using Cubes.Application.Contracts;
using Cubes.Application.Implementation;
using Cubes.Domain.Contracts;
using Cubes.Domain.Contracts.Objects;
using Cubes.Domain.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;
using System;
using Xunit.Sdk;

namespace Cubes.Application.UnitTest
{
    [TestClass]
    public class CubesIntersectionUnitTest
    {
        private static MockFactory _mockFactory;
        private static Mock<IIntersectionCalculator> _intersectionCalculatorMock;
        private static Mock<IVolumeCalculator> _volumeCalculatorMock;

        [TestInitialize]
        public void Testinitialize()
        {
            _mockFactory = new MockFactory();
            _intersectionCalculatorMock = _mockFactory.CreateMock<IIntersectionCalculator>();
            _volumeCalculatorMock = _mockFactory.CreateMock<IVolumeCalculator>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _mockFactory.ClearExpectations();
        }

        #region .: Tests :.

        [DataTestMethod]
        // No conozco muy bien MSTest, no he encontrado una forma mejor de pasar parámetros a esta prueba
        [DataRow(2d, 2d, 2d, 2d, 3d, 2d, 2d, 2d, 4d)]
        [DataRow(2d, 2d, 2d, 1d, 2d, 2d, 3d, 1d, 0d)]
        // Con lado negativo de los cubos, asignamos un valor por defecto igual a 1, también se podría haber lanzado una exception
        [DataRow(2d, 2d, 2d, -1d, 3d, 2d, 2d, -1d, 0d)]
        [DataRow(2d, 2d, 2d, 2d, 4d, 2d, 2d, 2d, 0d)]
        public void CubesIntersectionCollideTest(double x1, double y1, double z1, double edge1,
                                                                       double x2, double y2, double z2, double edge2,
                                                                       double expectedvolume
                                                                       )
        {
            // En los parámetros no se puede poner el sufijo m de decimal
            decimal cx1, cy1, cz1, ce1, cx2, cy2, cz2, ce2;
            FromDoubleToDecimal(x1, y1, z1, edge1, x2, y2, z2, edge2, out cx1, out cy1, out cz1, out ce1, out cx2, out cy2, out cz2, out ce2);

            var volumeIntersection = (decimal)expectedvolume;

            #region .: Configure mocks :.

            _intersectionCalculatorMock.Expects.AtLeastOne.Method(m => m.FindParallelCubeIntersection(null, null)).WithAnyArguments().WillReturn(false);
            _intersectionCalculatorMock.Expects.AtLeastOne.Method(m => m.CalculateParallelCubeIntersectionFigure(null, null)).WithAnyArguments().WillReturn(new Ortoedro());
            _volumeCalculatorMock.Expects.AtLeastOne.Method(m => m.CalculateOrtoedroVolume(null)).WithAnyArguments().WillReturn(1);

            #endregion .: Configure mocks :.

            // No estoy seguro de porque no funciona con el objeto Mock
            // ICubesIntersection cubesIntersection = new CubesIntersection(_intersectionCalculatorMock.MockObject, _volumeCalculatorMock.MockObject);
            ICubesIntersection cubesIntersection = new CubesIntersection(new IntersectionCalculator(), new VolumeCalculator());
            Cube firstcube = CubeBuilder.CreateCube().CenteredAt(cx1, cy1, cz1).WithEdgeLength(ce1).Build();
            Cube secondCube = CubeBuilder.CreateCube().CenteredAt(cx2, cy2, cz2).WithEdgeLength(ce2).Build();

            Tuple<bool, decimal> result = cubesIntersection.GetCubesIntersection(firstcube, secondCube);
            Assert.IsTrue(result.Item1, "Intersection should have been found.");
            Assert.IsTrue(result.Item2 == volumeIntersection, "Wrong volume.");
        }

        [DataTestMethod]
        [DataRow(2d, 2d, 2d, 2d, 10d, 10d, 10d, 2d)]
        public void CubesIntersectionDoNotCollideTest(double x1, double y1, double z1, double edge1,
                                                                                 double x2, double y2, double z2, double edge2)
        {
            // En los parámetros no se puede poner el sufijo m de decimal
            decimal cx1, cy1, cz1, ce1, cx2, cy2, cz2, ce2;
            FromDoubleToDecimal(x1, y1, z1, edge1, x2, y2, z2, edge2, out cx1, out cy1, out cz1, out ce1, out cx2, out cy2, out cz2, out ce2);


            #region .: Configure mocks :.

            _intersectionCalculatorMock.Expects.AtLeastOne.Method(m => m.FindParallelCubeIntersection(null, null)).WithAnyArguments().WillReturn(false);
            _intersectionCalculatorMock.Expects.AtLeastOne.Method(m => m.CalculateParallelCubeIntersectionFigure(null, null)).WithAnyArguments().WillReturn(new Ortoedro());
            _volumeCalculatorMock.Expects.AtLeastOne.Method(m => m.CalculateOrtoedroVolume(null)).WithAnyArguments().WillReturn(1);

            #endregion .: Configure mocks :.

            ICubesIntersection cubesIntersection = new CubesIntersection(_intersectionCalculatorMock.MockObject, _volumeCalculatorMock.MockObject);
            Cube firstcube = CubeBuilder.CreateCube().CenteredAt(cx1, cy1, cz1).WithEdgeLength(ce1).Build();
            Cube secondCube = CubeBuilder.CreateCube().CenteredAt(cx2, cy2, cz2).WithEdgeLength(ce2).Build();

            Tuple<bool, decimal> result = cubesIntersection.GetCubesIntersection(firstcube, secondCube);
            Assert.IsFalse(result.Item1, "Intersection should not have been found.");
            Assert.IsTrue(result.Item2 == 0, "Wrong volume.");
        }

        private static void FromDoubleToDecimal(double x1, double y1, double z1, double edge1, double x2, double y2, double z2, double edge2, out decimal cx1, out decimal cy1, out decimal cz1, out decimal ce1, out decimal cx2, out decimal cy2, out decimal cz2, out decimal ce2)
        {
            cx1 = (decimal)x1;
            cy1 = (decimal)y1;
            cz1 = (decimal)z1;
            ce1 = (decimal)edge1;
            cx2 = (decimal)x2;
            cy2 = (decimal)y2;
            cz2 = (decimal)z2;
            ce2 = (decimal)edge2;
        }


        #endregion .: Tests :.
    }
}