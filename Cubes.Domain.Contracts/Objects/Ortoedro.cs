namespace Cubes.Domain.Contracts.Objects
{
    public class Ortoedro
    {
        #region .: Constructor :.

        public Ortoedro()
        {
            Width = 0;
            Length = 0;
            Depth = 0;
        }

        public Ortoedro(decimal width, decimal length, decimal depth)
        {
            Width = width;
            Length = length;
            Depth = depth;
        }

        #endregion .: Constructor :.

        #region .: Properties :.

        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Depth { get; set; }

        #endregion .: Properties :.
    }
}