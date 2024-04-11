namespace Rubiks_Cube_Solver.Services.Interfaces
{
    using Rubiks_Cube_Solver.Models.Enums;

    public interface ICubeValidatorService
    {
        public void Validate(Dictionary<Faces, List<Colours>> cube);
    }
}