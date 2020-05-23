using Cubes.Domain.Contracts;
using Cubes.Domain.Contracts.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace Cubes.Domain.Implementation
{
    public class IntersectionCalculator : IIntersectionCalculator
    {
        #region .: Public Methods :.

        // Se podría cambiar esta firma por:     
        // public asinc Task<bool> FindParallelCubeIntersection(Cube firstCube, Cube secondCube)
        // lo que permitiría hacer  var result = await Task.WhenAll(task1, task2, task3); para liberar el thread mientras se estan realizando los cálculos
        // También vale este comentario para la siguiente función CalculateParallelCubeIntersectionFigure
        // Parece ser que esta firma solo es posible a partir de C# 8.0, y estamos trabajando con C# 7.3, he decidido no cambiar el 
        // lenguaje

        public bool FindParallelCubeIntersection(Cube firstCube, Cube secondCube)
        {
            // Habría que ver si es mejor ejecutar 3 tareas secuenciales, pero podemos ejecutar solo 1 o 2 de ellas.
            // O ejecutarlas siempre todas en paralelo, esperar que acaben todas, y buscar si alguna de ellas ha tenido como resultado false.
            // Dejo la primera opción en comentarios encima de la segunda solución escogida.
            // En este caso concreto los resultados son muy parecidos

            //bool result = true;

            //Task<bool> task1 = Task<bool>.Run(() => FindAxisIntersection(firstCube.Centre.Abscissa, firstCube.EdgeSize, secondCube.Centre.Abscissa, secondCube.EdgeSize));
            //if (!task1.Result) return false;

            //Task<bool> task2 = Task<bool>.Run(() => FindAxisIntersection(firstCube.Centre.Ordinate, firstCube.EdgeSize, secondCube.Centre.Ordinate, secondCube.EdgeSize));
            //if (!task2.Result) return false;

            //Task<bool> task3 = Task<bool>.Run(() => FindAxisIntersection(firstCube.Centre.Applicate, firstCube.EdgeSize, secondCube.Centre.Applicate, secondCube.EdgeSize));
            //if (!task3.Result) return false;

            // return result;

            var task1 = Task<bool>.Factory.StartNew(() => FindAxisIntersection(firstCube.Centre.Abscissa, firstCube.EdgeSize, secondCube.Centre.Abscissa, secondCube.EdgeSize));
            var task2 = Task<bool>.Factory.StartNew(() => FindAxisIntersection(firstCube.Centre.Ordinate, firstCube.EdgeSize, secondCube.Centre.Ordinate, secondCube.EdgeSize));
            var task3 = Task<bool>.Factory.StartNew(() => FindAxisIntersection(firstCube.Centre.Applicate, firstCube.EdgeSize, secondCube.Centre.Applicate, secondCube.EdgeSize));

            var result = Task.WhenAll(task1, task2, task3);

            return !result.Result.Contains(false);


        }

        public Ortoedro CalculateParallelCubeIntersectionFigure(Cube firstCube, Cube secondCube)
        {
            //******************************************************
            // WARNING - WARNING - WARNING - WARNING
            //******************************************************

            // Otra forma de ejecutar tareas en paralelo, esperar que acaben y obtener el resultado de las mismas, usando reflection
            // En principio, reflection es una operación cara en tiempo de ejecución, habría que valorar si vale la pena en este caso concreto.
            // Por otra parte el código es menos legible. Hay que tener claro porque usamos GetPropValue, y
            // respetar el orden de parámetros que necesita el constructor de la clase ortoedro.

            // Solo dejo este método así para demostrar el conocimiento de reflection, y que podría darse el caso de tener
            // que ejecutar una tarea para muchos casos (propiedades de la clase Point en este caso). Si encima
            // tenemos la dificultad añadida de tener que respetar el orden de los parámetros para obtener con
            // seguridad el resultado final, no creo que valga la pena hacerlo de esta manera.

            // El método mostrado en la función anterior es más sencillo, más corto y más claro.

            var dimensions = typeof(Point).GetProperties();
            var listResult = new List<(decimal, string)>();

            foreach (var item in dimensions)
            {
                var task = Task.Run(() => GetDimension((decimal)GetPropValue(firstCube.Centre, item.Name), firstCube.EdgeSize, (decimal)GetPropValue(secondCube.Centre, item.Name), secondCube.EdgeSize));
                
                // Esto tiene que evitarse! , task.Result es bloqueante para este thread.
                var tuple = (task.Result, item.Name);
                listResult.Add(tuple);
            }

            // Necesario para respetar el orden de los parámetros del constructor para la clase ortoedro

            var width = listResult.Where(x => x.Item2 == "Abscissa").FirstOrDefault().Item1;
            var length = listResult.Where(x => x.Item2 == "Ordinate").FirstOrDefault().Item1;
            var depth = listResult.Where(x => x.Item2 == "Applicate").FirstOrDefault().Item1;

            return new Ortoedro(width, length, depth);
        }

        #endregion .: Public Methods :.

        #region .: Private methods :.

        private decimal GetDimension(decimal coordinate1, decimal edgeSize1, decimal coordinate2, decimal edgeSize2)
        {
            return Math.Abs(Math.Min(coordinate1 + edgeSize1 / 2, coordinate2 + edgeSize2 / 2) - Math.Max(coordinate1 - edgeSize1 / 2, coordinate2 - edgeSize2 / 2));
        }

        private bool FindAxisIntersection(decimal coordinate1, decimal edgeSize1, decimal coordinate2, decimal edgeSize2)
        {
            Tuple<decimal, decimal> segment1 = new Tuple<decimal, decimal>(coordinate1 - edgeSize1 / 2, coordinate1 + edgeSize1 / 2);
            Tuple<decimal, decimal> segment2 = new Tuple<decimal, decimal>(coordinate2 - edgeSize2 / 2, coordinate2 + edgeSize2 / 2);

            return (segment1.Item1 <= segment2.Item1 && segment2.Item1 <= segment1.Item2) || (segment1.Item1 <= segment2.Item2 && segment2.Item2 <= segment1.Item2);
        }

        #endregion .: Private methods :.

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}