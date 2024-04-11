namespace Rubiks_Cube_Solver.Services
{
    using Rubiks_Cube_Solver.Models;
    using Rubiks_Cube_Solver.Models.Enums;
    using Rubiks_Cube_Solver.Services.Interfaces;

    public class CubeValidatorService : ICubeValidatorService
    {
        public void Validate(Dictionary<Faces, List<Colours>> cube)
        {
            if (cube == null)
            {
                throw new ArgumentNullException(nameof(cube));
            }
            else if (cube.Keys.Count != CubeConstants.FacesPerCube)
            {
                throw new ArgumentException($"{CubeConstants.FacesPerCube} faces must be provided.");
            }
            else if (cube.Any(cubeTile => cubeTile.Value.Count != CubeConstants.TilesPerFace))
            {
                throw new ArgumentException($"{CubeConstants.TilesPerFace} colours for each face must be provided.");
            }
        }
    }
}