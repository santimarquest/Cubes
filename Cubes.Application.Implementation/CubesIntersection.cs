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
            var calculateVolumeIntersection = new CubeChainOfResponsability.CalculateVolumeIntersection();
            var getIntersection = new CubeChainOfResponsability.GetIntersection(calculateVolumeIntersection);

            //var myObject = getIntersection.Handle(new CubeExtensionsChainOfResponsability.MyClass(_intersectionCalculator, firstCube, secondCube));
            //var result = calculateVolumeIntersection.Handle(myObject);

            var result = getIntersection.Handle(new CubeChainOfResponsability.CubeHandlerParams(_intersectionCalculator, firstCube, secondCube));
            // var result = calculateVolumeIntersection.Handle(myObject);

            //var intersection = CubeExtensionsChainOfResponsability.GetIntersection(_intersectionCalculator, firstCube, secondCube);
            //return (intersection, CubeExtensionsChainOfResponsability.CalculateVolumeIntersection(intersection, _intersectionCalculator, _volumeCalculator, firstCube, secondCube)).ToTuple();

            return new Tuple<bool, decimal>(result.intersection, result.volume);
        }

        #endregion .: Public Methods :.
    }
}