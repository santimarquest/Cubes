using Cubes.Domain.Contracts.Objects;

namespace Cubes.Domain.Contracts
{
    public interface IVolumeCalculator
    {
        decimal CalculateOrtoedroVolume(Ortoedro ortoedro);
    }
}