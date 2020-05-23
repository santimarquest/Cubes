using Cubes.Domain.Contracts.Objects;

namespace Cubes.Domain.Contracts
{
    public interface IIntersectionCalculator
    {
        bool FindParallelCubeIntersection(Cube firstCube, Cube secondCube);

        Ortoedro CalculateParallelCubeIntersectionFigure(Cube firstCube, Cube secondCube);
    }
}