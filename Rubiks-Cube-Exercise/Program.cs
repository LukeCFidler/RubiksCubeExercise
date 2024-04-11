using Rubiks_Cube_Solver.Models;
using Rubiks_Cube_Solver.Models.Enums;
using Rubiks_Cube_Solver.Services;
using System.Drawing;

internal class Program
{
    private static void Main()
    {
        var cube = new Dictionary<Faces, List<Colours>>();

        for (int i = 0; i < CubeConstants.FacesPerCube; i++)
        {
            var enumIndex = i + 1;

            cube.Add(((Faces)enumIndex), Enumerable.Repeat((Colours)enumIndex, CubeConstants.TilesPerFace).ToList());
        }

        var cubeRotatorService = new CubeRotatorService(new CubeValidatorService(), new RotationCalculatorService());
        var cubeWriteService = new CubeWriterService();

        Console.WriteLine(cubeWriteService.Write(cube));
        cubeRotatorService.Rotate(cube, Faces.Front, Directions.Clockwise);
        Console.WriteLine($"Rotating {Faces.Front} {Directions.Clockwise} - {Environment.NewLine}{cubeWriteService.Write(cube)}");
        cubeRotatorService.Rotate(cube, Faces.Right, Directions.AntiClockwise);
        Console.WriteLine($"Rotating {Faces.Right} {Directions.AntiClockwise} - {Environment.NewLine}{cubeWriteService.Write(cube)}");
        cubeRotatorService.Rotate(cube, Faces.Top, Directions.Clockwise);
        Console.WriteLine($"Rotating {Faces.Top} {Directions.Clockwise} - {Environment.NewLine}{cubeWriteService.Write(cube)}");
        cubeRotatorService.Rotate(cube, Faces.Back, Directions.AntiClockwise);
        Console.WriteLine($"Rotating {Faces.Back} {Directions.AntiClockwise} - {Environment.NewLine}{cubeWriteService.Write(cube)}");
        cubeRotatorService.Rotate(cube, Faces.Left, Directions.Clockwise);
        Console.WriteLine($"Rotating {Faces.Left} {Directions.Clockwise} - {Environment.NewLine}{cubeWriteService.Write(cube)}");
        cubeRotatorService.Rotate(cube, Faces.Bottom, Directions.AntiClockwise);
        Console.WriteLine($"Rotating {Faces.Bottom} {Directions.AntiClockwise} - {Environment.NewLine}{cubeWriteService.Write(cube)}");
        Console.ReadLine();
    }
}