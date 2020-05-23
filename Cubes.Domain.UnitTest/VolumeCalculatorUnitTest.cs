using Cubes.Domain.Contracts;
using Cubes.Domain.Contracts.Objects;
using Cubes.Domain.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cubes.Domain.UnitTest
{
    [TestClass]
    public class VolumeCalculatorUnitTest
    {
        #region .: Tests :.

        [DataTestMethod]
        [DataRow(2d,2d,2d)]
        [DataRow(2d, 0.5d, 1d)]
        [DataRow(3d, 2d, 1d)]
        [DataRow(0d, 2d, 2d)]
        public void OrtoedroVolumeTest(double width, double length, double depth)
        {
            var decimalWidth = (decimal)width;
            var decimalLength = (decimal)length;
            var decimalDepth = (decimal)depth;

            IVolumeCalculator volumeCalculator = new VolumeCalculator();
            Ortoedro ortoedro = new Ortoedro()
            {
                Width = decimalWidth,
                Length = decimalLength,
                Depth = decimalDepth
            };

            decimal volume = volumeCalculator.CalculateOrtoedroVolume(ortoedro);

            Assert.IsTrue(volume == decimalWidth * decimalLength * decimalDepth, $"Expected volume 8 but received {volume}.");
        }

        #endregion .: Tests :.
    }
}