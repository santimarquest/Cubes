using Cubes.Domain.Contracts.Objects;
using System;

namespace Cubes.Application.Contracts
{
    public interface ICubesIntersection
    {
        Tuple<bool, decimal> GetCubesIntersection(Cube firstCube, Cube secondCube);
    }
}