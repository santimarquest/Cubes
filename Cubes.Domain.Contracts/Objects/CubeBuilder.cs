namespace Cubes.Domain.Contracts.Objects
{
    public class CubeBuilder
    {
        private Point center;
        private decimal edgeLength;

        public static CubeBuilder CreateCube()
        {
            return new CubeBuilder();
        }

        public CubeBuilder CenteredAt(decimal x, decimal y, decimal z)
        {
            center = new Point(x, y, z);
            return this;
        }

        public CubeBuilder WithEdgeLength(decimal length)
        {
            // El patrón builder nos permite hacer este tipo de validaciones en el momento
            // de construir objetos complejos. Aquí también se podría lanzar una exception.
            // Aunque la verdad es que el tratamiento de errores en un builder depende de cada caso, y da para un libro entero.
            // http://codinghelmet.com/articles/advances-in-applying-the-builder-design-pattern

            if (length <= 0)
            {
                length = 1;
            }
            edgeLength = length;
            return this;
        }

        public Cube Build()
        {
            return new Cube(center, edgeLength);
        }
    }
}