namespace Rubiks_Cube_Solver.Services
{
    using Rubiks_Cube_Solver.Models.Enums;
    using Rubiks_Cube_Solver.Services.Interfaces;

    public class RotationCalculatorService : IRotationCalculatorService
    {
        public int CalculateRotatedPosition(Directions direction, int index)
        {
            return direction switch
            {
                Directions.Clockwise => index switch
                {
                    0 => 2,
                    1 => 5,
                    2 => 8,
                    3 => 1,
                    4 => 4,
                    5 => 7,
                    6 => 0,
                    7 => 3,
                    8 => 6,
                    _ => throw new ArgumentOutOfRangeException(nameof(index)),
                },
                Directions.AntiClockwise => index switch
                {
                    0 => 6,
                    1 => 3,
                    2 => 0,
                    3 => 7,
                    4 => 4,
                    5 => 1,
                    6 => 8,
                    7 => 5,
                    8 => 2,
                    _ => throw new ArgumentOutOfRangeException(nameof(index)),
                },
                _ => throw new ArgumentOutOfRangeException(nameof(direction)),
            };
        }

        public Dictionary<Faces, Faces> CalculateFaceChange(Faces face, Directions direction)
        {
            if ((face == Faces.Front || face == Faces.Back) && direction == Directions.Clockwise)
            {
                return new Dictionary<Faces, Faces>()
                {
                    { Faces.Top, Faces.Right },
                    { Faces.Right, Faces.Bottom },
                    { Faces.Bottom, Faces.Left },
                    { Faces.Left, Faces.Top }
                };
            }
            else if ((face == Faces.Front || face == Faces.Back) && direction == Directions.AntiClockwise)
            {
                return new Dictionary<Faces, Faces>()
                {
                    { Faces.Top, Faces.Left },
                    { Faces.Left, Faces.Bottom },
                    { Faces.Bottom, Faces.Right },
                    { Faces.Right, Faces.Top }
                };
            }
            else if ((face == Faces.Left || face == Faces.Right) && direction == Directions.Clockwise)
            {
                return new Dictionary<Faces, Faces>()
                {
                    { Faces.Top, Faces.Back },
                    { Faces.Back, Faces.Bottom },
                    { Faces.Bottom, Faces.Front },
                    { Faces.Front, Faces.Top }
                };
            }
            else if ((face == Faces.Left || face == Faces.Right) && direction == Directions.AntiClockwise)
            {
                return new Dictionary<Faces, Faces>()
                {
                    { Faces.Top, Faces.Front },
                    { Faces.Front, Faces.Bottom },
                    { Faces.Bottom, Faces.Back },
                    { Faces.Back, Faces.Top }
                };
            }
            else if ((face == Faces.Top || face == Faces.Bottom) && direction == Directions.Clockwise)
            {
                return new Dictionary<Faces, Faces>()
                {
                    { Faces.Front, Faces.Left },
                    { Faces.Left, Faces.Back },
                    { Faces.Back, Faces.Right },
                    { Faces.Right, Faces.Front }
                };
            }
            else if ((face == Faces.Top || face == Faces.Bottom) && direction == Directions.AntiClockwise)
            {
                return new Dictionary<Faces, Faces>()
                {
                    { Faces.Front, Faces.Right },
                    { Faces.Right, Faces.Back },
                    { Faces.Back, Faces.Left },
                    { Faces.Left, Faces.Front }
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Invalid {nameof(face)} or {nameof(direction)} provided");
            }
        }

        public Dictionary<Faces, int[]> CalculateEffectedSideIndexes(Faces face)
        {
            return (face) switch
            {
                Faces.Front => new Dictionary<Faces, int[]>()
                {
                    { Faces.Top, [6, 7, 8] },
                    { Faces.Bottom, [0, 1, 2] },
                    { Faces.Left, [2, 5, 8] },
                    { Faces.Right, [0, 3, 6] }
                },
                Faces.Back => new Dictionary<Faces, int[]>()
                {
                    { Faces.Top, [0, 1, 2] },
                    { Faces.Bottom, [6, 7, 8] },
                    { Faces.Left, [0, 3, 6] },
                    { Faces.Right, [2, 5, 8] }
                },
                Faces.Left => new Dictionary<Faces, int[]>()
                {
                    { Faces.Top, [0, 3, 6] },
                    { Faces.Bottom, [0, 3, 6] },
                    { Faces.Front, [0, 3, 6] },
                    { Faces.Back, [0, 3, 6] }
                },
                Faces.Right => new Dictionary<Faces, int[]>()
                {
                    { Faces.Top, [2, 5, 8] },
                    { Faces.Bottom, [2, 5, 8] },
                    { Faces.Front, [2, 5, 8] },
                    { Faces.Back, [2, 5, 8] }
                },
                Faces.Top => new Dictionary<Faces, int[]>()
                {
                    { Faces.Front, [0, 1, 2] },
                    { Faces.Back, [0, 1, 2] },
                    { Faces.Left, [0, 1, 2] },
                    { Faces.Right, [0, 1, 2] }
                },
                Faces.Bottom => new Dictionary<Faces, int[]>()
                {
                    { Faces.Front, [6, 7, 8] },
                    { Faces.Back, [6, 7, 8] },
                    { Faces.Left, [6, 7, 8] },
                    { Faces.Right, [6, 7, 8] }
                },
                _ => throw new ArgumentOutOfRangeException(nameof(face))
            };
        }
    }
}