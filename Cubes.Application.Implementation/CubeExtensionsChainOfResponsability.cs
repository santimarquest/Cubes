using Cubes.Domain.Contracts;
using Cubes.Domain.Contracts.Objects;

namespace Cubes.Application.Implementation
{
    public static class CubeChainOfResponsability
    {
        public class CubeHandlerParams
        { 

            public CubeHandlerParams(IIntersectionCalculator intersectionCalculator, 
                                                       IVolumeCalculator volumeCalculator,
                                                      Cube firstCube, Cube secondCube)
            {
                this.intersectionCalculator = intersectionCalculator;
                this.volumeCalculator = volumeCalculator;
                this.firstCube = firstCube;
                this.secondCube = secondCube;
            }

            public bool intersection { get; set; }
            public IIntersectionCalculator intersectionCalculator { get; set; }
            public IVolumeCalculator volumeCalculator { get; set; }
            public Cube firstCube { get; set; }
            public Cube secondCube { get; set; }

            public decimal volume { get; set; }
        }

        public interface IHandler
        {
            IHandler SetNext(IHandler handler);

            CubeHandlerParams Handle(CubeHandlerParams cubeHandlerParams);
        }

        public abstract class AbstractHandler : IHandler
        {
            private IHandler _nextHandler;

            public IHandler SetNext(IHandler handler)
            {
                this._nextHandler = handler;
                return handler;
            }

            public virtual CubeHandlerParams Handle(CubeHandlerParams cubeHandlerParams)
            {
                if (this._nextHandler != null)
                {
                    return this._nextHandler.Handle(cubeHandlerParams);
                }
                else
                {
                    return cubeHandlerParams;
                }
            }
        }

       public class GetIntersection : AbstractHandler
        {
            public override CubeHandlerParams Handle(CubeHandlerParams cubeHandlerParams)
            {
                cubeHandlerParams.intersection =  cubeHandlerParams.intersectionCalculator.FindParallelCubeIntersection(cubeHandlerParams.firstCube, cubeHandlerParams.secondCube);
                return base.Handle(cubeHandlerParams);
            }
        }

        public class CalculateVolumeIntersection : AbstractHandler
        {

            public CalculateVolumeIntersection()
            {
                this.SetNext(null);
            }

            public override CubeHandlerParams Handle( CubeHandlerParams cubeHandlerParams)
            {
              cubeHandlerParams.volume = cubeHandlerParams.intersection
              ? CalculateVolumeIntersectionTrue(cubeHandlerParams.intersectionCalculator, cubeHandlerParams.volumeCalculator, cubeHandlerParams.firstCube, cubeHandlerParams.secondCube)
              : CalculateVolumeIntersectionFalse();

                return base.Handle(cubeHandlerParams);
            }
        }

        public static decimal CalculateVolumeIntersectionTrue(  IIntersectionCalculator intersectionCalculator,
                                                                                             IVolumeCalculator volumeCalculator,
                                                                                             Cube firstCube, Cube secondCube)
        {
            return (volumeCalculator.CalculateOrtoedroVolume(intersectionCalculator.CalculateParallelCubeIntersectionFigure(firstCube, secondCube)));
        }


        public static decimal CalculateVolumeIntersectionFalse()
        {
            return 0m;
        }
    }
}
