using Cubes.Domain.Contracts.Objects;

namespace Cubes.Domain.Contracts
{
    public interface IVolumeCalculator
    {
        decimal NoVolume(Ortoedro ortoedro);
        decimal CalculateOrtoedroVolume(Ortoedro ortoedro);
    }
}