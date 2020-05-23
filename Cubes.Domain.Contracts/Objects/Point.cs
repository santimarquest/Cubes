namespace Cubes.Domain.Contracts.Objects
{
    public class Point
    {
        #region .: Constructor :.

        public Point()
        {
            Abscissa = 0;
            Ordinate = 0;
            Applicate = 0;
        }

        public Point(decimal abscissa, decimal ordinate, decimal applicate)
        {
            Abscissa = abscissa;
            Ordinate = ordinate;
            Applicate = applicate;
        }

        #endregion .: Constructor :.

        #region .: Properties :.

        public decimal Abscissa { get; set; }
        public decimal Ordinate { get; set; }
        public decimal Applicate { get; set; }

        #endregion .: Properties :.
    }
}