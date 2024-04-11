namespace Rubiks_Cube_Solver.Models
{
    using Rubiks_Cube_Solver.Models.Enums;

    public class CubeConstants
    {
        public static int FacesPerCube => Enum.GetNames(typeof(Faces)).Length;

        public const int TilesPerFace = 9;
    }
}
