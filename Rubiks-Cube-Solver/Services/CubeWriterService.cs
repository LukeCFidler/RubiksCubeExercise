namespace Rubiks_Cube_Solver.Services
{
    using Rubiks_Cube_Solver.Models.Enums;
    using System.Text;

    public class CubeWriterService
    {
        public string Write(Dictionary<Faces, List<Colours>> cube)
        {
            var outputBuilder = new StringBuilder();
            var i = 1;

            foreach (var key in cube.Keys)
            {
                outputBuilder.AppendLine($"{key} - ");

                foreach (var colour in cube[key])
                {
                    outputBuilder.Append($" | {colour} |");

                    if (i % 3 == 0 && i > 0)
                    {
                        outputBuilder.AppendLine();
                    }

                    i++;
                }
            }

            return outputBuilder.ToString();
        }
    }
}