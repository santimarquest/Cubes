using Cubes.Application.Contracts;
using Cubes.Domain.Contracts.Objects;
using Cubes.Presentation.DTO;
using System;

namespace Cubes.Presentation
{
    public class App : IApp
    {
        #region .: Properties :.

        private readonly ICubesIntersection _cubesIntersection;

        #endregion .: Properties :.

        #region .: Constructor :.

        public App(ICubesIntersection cubesIntersection)
        {
            _cubesIntersection = cubesIntersection;
        }

        #endregion .: Constructor :.

        #region .: Public Methods :.

        public void Run()
        {
            string leave = "n";
            Console.Out.WriteLine("Write \"exit\" at any time to quit application.");
            while (!leave.Trim().ToLower().Equals("y"))
            {
                InputDTO inputDTO = new InputDTO(); 
                SetCubesInformationFromConsoleInput(inputDTO);
                Tuple<bool, decimal> result = ProcessInput(inputDTO);

                if (result.Item1)
                {
                    Console.Out.WriteLine($"The cubes collide and their intersection volume is {result.Item2}.");
                }
                else
                {
                    Console.Out.WriteLine("The cubes do not collide.");
                }
                Console.Out.WriteLine("Do you want to leave? y/n");
                leave = Console.In.ReadLine();
            }

            Console.Out.WriteLine("Leaving program...");
        }

        #endregion .: Public Methods :.

        #region .: Private Methods :.

        private void SetCubesInformationFromConsoleInput(InputDTO inputDTO)
        {
            string input;

            Console.Out.WriteLine("Set coordinates of the centre for the first cube.");
            Console.Out.WriteLine("Enter X:");
            input = Console.In.ReadLine();
            inputDTO.Abscisse1 = ParseInput(input);

            Console.Out.WriteLine("Enter Y:");
            input = Console.In.ReadLine();
            inputDTO.Ordinate1 = ParseInput(input);

            Console.Out.WriteLine("Enter Z:");
            input = Console.In.ReadLine();
            inputDTO.Applicate1 = ParseInput(input);

            Console.Out.WriteLine("Enter the edge size:");
            input = Console.In.ReadLine();
            inputDTO.EdgeSize1 = Math.Abs(ParseInput(input));

            Console.Out.WriteLine("Set coordinates of the centre for the second cube.");
            Console.Out.WriteLine("Enter X:");
            input = Console.In.ReadLine();
            inputDTO.Abscisse2 = ParseInput(input);

            Console.Out.WriteLine("Enter Y:");
            input = Console.In.ReadLine();
            inputDTO.Ordinate2 = ParseInput(input);

            Console.Out.WriteLine("Enter Z:");
            input = Console.In.ReadLine();
            inputDTO.Applicate2 = ParseInput(input);

            Console.Out.WriteLine("Enter the edge size:");
            input = Console.In.ReadLine();
            inputDTO.EdgeSize2 = Math.Abs(ParseInput(input));
        }

        private decimal ParseInput(string input)
        {
            decimal parsedInput;

            if (input.Trim().ToLower().Equals("exit"))
            {
                Console.Out.WriteLine("Leaving program...");
                Environment.Exit(0);
            }

            while (!ValidateInput(input, out parsedInput))
            {
                Console.Out.WriteLine("Introduced value is not valid, reenter it: ");
                input = Console.In.ReadLine();
                if (input.Trim().ToLower().Equals("exit"))
                {
                    Console.Out.WriteLine("Leaving program...");
                    Environment.Exit(0);
                }
            }

            return parsedInput;
        }

        private bool ValidateInput(string input, out decimal parsedInput)
        {
            return decimal.TryParse(input.Replace('.', ','), out parsedInput);
        }

        private Tuple<bool, decimal> ProcessInput(InputDTO inputDTO)
        {
            Cube firstCube = CubeBuilder.CreateCube()
                .CenteredAt(inputDTO.Abscisse1, inputDTO.Ordinate1, inputDTO.Applicate1)
                .WithEdgeLength(inputDTO.EdgeSize1)
                .Build();

            Cube secondCube = CubeBuilder.CreateCube()
               .CenteredAt(inputDTO.Abscisse2, inputDTO.Ordinate2, inputDTO.Applicate2)
               .WithEdgeLength(inputDTO.EdgeSize2)
               .Build();

            return _cubesIntersection.GetCubesIntersection(firstCube, secondCube);
        }

        #endregion .: Private Methods :.
    }
}