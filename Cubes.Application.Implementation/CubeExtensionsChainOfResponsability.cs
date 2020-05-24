using Cubes.Domain.Contracts;
using Cubes.Domain.Contracts.Objects;
using Cubes.Domain.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubes.Application.Implementation
{
    public static class CubeChainOfResponsability
    {
        public class CubeHandlerParams
        { 

            public CubeHandlerParams(IIntersectionCalculator intersectionCalculator, Cube firstCube, Cube secondCube)
            {
                this.intersectionCalculator = intersectionCalculator;
                this.firstCube = firstCube;
                this.secondCube = secondCube;
            }

            public CubeHandlerParams(bool intersection, IIntersectionCalculator intersectionCalculator, IVolumeCalculator volumeCalculator, Cube firstCube, Cube secondCube)
            {
                this.intersection = intersection;
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

            CubeHandlerParams Handle(CubeHandlerParams myObject);
        }

        // The default chaining behavior can be implemented inside a base handler
        // class.
        public abstract class AbstractHandler : IHandler
        {
            private IHandler _nextHandler;

            public IHandler SetNext(IHandler handler)
            {
                this._nextHandler = handler;

                // Returning a handler from here will let us link handlers in a
                // convenient way like this:
                // monkey.SetNext(squirrel).SetNext(dog);
                return handler;
            }

            public virtual CubeHandlerParams Handle(CubeHandlerParams myObject)
            {
                if (this._nextHandler != null)
                {
                    return this._nextHandler.Handle(myObject);
                }
                else
                {
                    return myObject;
                }
            }
        }

       public class GetIntersection : AbstractHandler
        {
            private CalculateVolumeIntersection calculateVolumeIntersection;

            public GetIntersection ()
            {
                
            }

            public GetIntersection(CalculateVolumeIntersection calculateVolumeIntersection)
            {
                this.SetNext(calculateVolumeIntersection);
            }

            public override CubeHandlerParams Handle(CubeHandlerParams myObject)
            {
                myObject.intersection =  myObject.intersectionCalculator.FindParallelCubeIntersection(myObject.firstCube, myObject.secondCube);
                if (myObject.intersection)
                {
                    myObject.volumeCalculator = new VolumeCalculator();
                }
                return base.Handle(myObject);
            }
        }

        public class CalculateVolumeIntersection : AbstractHandler
        {
            private object p;

            public CalculateVolumeIntersection()
            {
                this.SetNext(null);
            }

            public override CubeHandlerParams Handle( CubeHandlerParams myObject)
            {
              myObject.volume = myObject.intersection
              ? CalculateVolumeIntersectionTrue(myObject.intersectionCalculator, myObject.volumeCalculator, myObject.firstCube, myObject.secondCube)
              : CalculateVolumeIntersectionFalse();

                return base.Handle(myObject);
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

        //public static decimal CalculateVolumeIntersection(bool intersection,
        //                                                                                 IIntersectionCalculator intersectionCalculator,
        //                                                                                 IVolumeCalculator volumeCalculator,
        //                                                                                  Cube firstCube, Cube secondCube)
        //{
        //    return (intersection)
        //        ? CalculateVolumeIntersectionTrue(intersectionCalculator, volumeCalculator, firstCube, secondCube)
        //        : CalculateVolumeIntersectionFalse();
        //}
    }
}
