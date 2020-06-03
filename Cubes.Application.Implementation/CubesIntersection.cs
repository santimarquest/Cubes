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
            // Definimos los pasos de un patrón Chain of Responsability, en este casos tenemos unca cade na de solo dos pasos.
            var getIntersection = new CubeChainOfResponsability.GetIntersection();
            var calculateVolumeIntersection = new CubeChainOfResponsability.CalculateVolumeIntersection();
              
            // Definimos el orden de ejecución de los pasos en la cadena
            getIntersection.SetNext(calculateVolumeIntersection);

            // Lanzamos la ejecución del primer paso. Si no es el último paso, cada paso llama al siguiente.
            var result = getIntersection.Handle(new CubeChainOfResponsability.CubeHandlerParams(_intersectionCalculator, _volumeCalculator,  firstCube, secondCube));
            return new Tuple<bool, decimal>(result.intersection, result.volume);
        }

        #endregion .: Public Methods :.
    }
}