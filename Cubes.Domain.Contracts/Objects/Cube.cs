namespace Cubes.Domain.Contracts.Objects
{
    public class Cube : Ortoedro
    {
        #region .: Constructor :.

        public Cube(Point centre, decimal edgeSize)
        {
            Centre = centre;
            Width = Length = Depth = EdgeSize = edgeSize;
        }

        #endregion .: Constructor :.

        #region .: Properties :.

        public Point Centre { get; set; }
        public decimal EdgeSize { get; set; }

        #endregion .: Properties :.
    }
}