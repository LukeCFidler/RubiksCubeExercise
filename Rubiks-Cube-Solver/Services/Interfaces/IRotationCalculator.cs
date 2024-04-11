namespace Rubiks_Cube_Solver.Services.Interfaces
{
    using Rubiks_Cube_Solver.Models.Enums;

    public interface IRotationCalculatorService
    {
        public int CalculateRotatedPosition(Directions direction, int index);

        public Dictionary<Faces, Faces> CalculateFaceChange(Faces face, Directions direction);

        public Dictionary<Faces, int[]> CalculateEffectedSideIndexes(Faces face);
    }
}