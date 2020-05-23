using Cubes.Application.Contracts;
using Cubes.Domain.Contracts;
using Cubes.Domain.Contracts.Objects;
using System;

namespace Cubes.Application.Implementation
{
    public class CubesIntersection : ICubesIntersection
    {
        #region .: Properties :.

        private readonly IIntersectionCalculator _intersectionCalculator;
        private readonly IVolumeCalculator _volumeCalculator;

        #endregion .: Properties :.

        #region .: Constructor :.

        public CubesIntersection(IIntersectionCalculator intersectionCalculator, IVolumeCalculator volumeCalculator)
        {
            _intersectionCalculator = intersectionCalculator;
            _volumeCalculator = volumeCalculator;
        }

        #endregion .: Constructor :.

        #region .: Public Methods :.

        public Tuple<bool, decimal> GetCubesIntersection(Cube firstCube, Cube secondCube)
        {
            decimal intersectionVolume = 0;
            bool existsIntersection = _intersectionCalculator.FindParallelCubeIntersection(firstCube, secondCube);

            if (existsIntersection)
            {
                intersectionVolume = _volumeCalculator.CalculateOrtoedroVolume(_intersectionCalculator.CalculateParallelCubeIntersectionFigure(firstCube, secondCube));
            }

            return new Tuple<bool, decimal>(existsIntersection, intersectionVolume);
        }

        #endregion .: Public Methods :.
    }
}