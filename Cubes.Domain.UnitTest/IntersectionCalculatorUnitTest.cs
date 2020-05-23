using Cubes.Domain.Contracts;
using Cubes.Domain.Contracts.Objects;
using Cubes.Domain.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cubes.Domain.UnitTest
{
    [TestClass]
    public class IntersectionCalculatorUnitTest
    {
        #region .: Tests :.

        [DataTestMethod]
        [DataRow(0d, 0d, 0d, 2d, 0d, 0d, 0d, 2d)]
        public void FindParallelCubeIntersectionSameCubeTest(double x1, double y1, double z1, double edge1,
                                                                                            double x2, double y2, double z2, double edge2)
        {
            // En los parámetros no se puede poner el sufijo m de decimal
            decimal cx1, cy1, cz1, ce1, cx2, cy2, cz2, ce2;
            FromDoubleToDecimal(x1, y1, z1, edge1, x2, y2, z2, edge2, out cx1, out cy1, out cz1, out ce1, out cx2, out cy2, out cz2, out ce2);

            IIntersectionCalculator intersectionCalculator = new IntersectionCalculator();

            Cube firstCube = CubeBuilder.CreateCube()
               .CenteredAt(cx1, cy1, cz1)
               .WithEdgeLength(ce1)
               .Build();

            Cube secondCube = CubeBuilder.CreateCube()
              .CenteredAt(cx2, cy2, cz2)
              .WithEdgeLength(ce2)
              .Build();

            Assert.IsTrue(intersectionCalculator.FindParallelCubeIntersection(firstCube, secondCube), "An intersection should have been found.");
        }

        [DataTestMethod]
        [DataRow(0d, 0d, 0d, 2d, 3d, 3d, 3d, 2d)]
        public void FindParallelCubeIntersectionNoCollidingTes(double x1, double y1, double z1, double edge1,
                                                                                            double x2, double y2, double z2, double edge2)
        {
            // En los parámetros no se puede poner el sufijo m de decimal
            decimal cx1, cy1, cz1, ce1, cx2, cy2, cz2, ce2;
            FromDoubleToDecimal(x1, y1, z1, edge1, x2, y2, z2, edge2, out cx1, out cy1, out cz1, out ce1, out cx2, out cy2, out cz2, out ce2);

            IIntersectionCalculator intersectionCalculator = new IntersectionCalculator();
            Cube firstCube = CubeBuilder.CreateCube()
            .CenteredAt(cx1, cy1, cz1)
            .WithEdgeLength(ce1)
            .Build();

            Cube secondCube = CubeBuilder.CreateCube()
             .CenteredAt(cx2, cy2, cz2)
             .WithEdgeLength(ce2)
             .Build();

            Assert.IsFalse(intersectionCalculator.FindParallelCubeIntersection(firstCube, secondCube), "An intersection have been found but cubes do not collide.");
        }

        [DataTestMethod]
        [DataRow(0d, 0d, 0d, 2d, 0d, 1d, 0d, 2d, 2d, 1d, 2d)]
        public void CalculateParallelCubeIntersectionFigureTest(double x1, double y1, double z1, double edge1,
                                                                                            double x2, double y2, double z2, double edge2,
                                                                                            double rwidth, double rlength, double rdepth)
        {
            // En los parámetros no se puede poner el sufijo m de decimal
            decimal cx1, cy1, cz1, ce1, cx2, cy2, cz2, ce2;  
            FromDoubleToDecimal(x1, y1, z1, edge1, x2, y2, z2, edge2, out cx1, out cy1, out cz1, out ce1, out cx2, out cy2, out cz2, out ce2);
            
            var width = (decimal)rwidth;
            var length = (decimal)rlength;
            var depth = (decimal)rdepth;

            IIntersectionCalculator intersectionCalculator = new IntersectionCalculator();
            Cube firstCube = CubeBuilder.CreateCube()
             .CenteredAt(cx1, cy1, cz1)
             .WithEdgeLength(ce1)
             .Build();

            Cube secondCube = CubeBuilder.CreateCube()
                .CenteredAt(cx2, cy2, cz2)
                .WithEdgeLength(ce2)
                .Build();

            Ortoedro result = intersectionCalculator.CalculateParallelCubeIntersectionFigure(firstCube, secondCube);
            Assert.IsTrue(result.Width == width && result.Length == length && result.Depth == depth, "The resultant ortoedro dimensions are not the expected.");
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