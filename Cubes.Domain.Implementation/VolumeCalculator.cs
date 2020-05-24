using Cubes.Domain.Contracts;
using Cubes.Domain.Contracts.Objects;

namespace Cubes.Domain.Implementation
{
    public class VolumeCalculator : IVolumeCalculator
    {
        #region .: Public Methods :.

        public decimal CalculateOrtoedroVolume(Ortoedro ortoedro)
        {
            return ortoedro.Width * ortoedro.Length * ortoedro.Depth;
        }

        public decimal NoVolume(Ortoedro ortoedro)
        {
            return 0m;
        }

        #endregion .: Public Methods :.
    }
}