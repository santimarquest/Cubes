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
            var cx1 = (decimal)x1;
            var cy1 = (decimal)y1;
            var cz1 = (decimal)z1;
            var ce1 = (decimal)edge1;
            var cx2 = (decimal)x2;
            var cy2 = (decimal)y2;
            var cz2 = (decimal)z2;
            var ce2 = (decimal)edge2;
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
            var cx1 = (decimal)x1;
            var cy1 = (decimal)y1;
            var cz1 = (decimal)z1;
            var ce1 = (decimal)edge1;
            var cx2 = (decimal)x2;
            var cy2 = (decimal)y2;
            var cz2 = (decimal)z2;
            var ce2 = (decimal)edge2;

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

        #endregion .: Tests :.
    }
}