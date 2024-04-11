namespace Rubiks_Cube_Solver.Services
{
    using Rubiks_Cube_Solver.Models;
    using Rubiks_Cube_Solver.Models.Enums;
    using Rubiks_Cube_Solver.Services.Interfaces;
    using System;
    using System.Collections.Generic;

    public class CubeRotatorService
    {
        private readonly ICubeValidatorService _cubeValidatorService;
        private readonly IRotationCalculatorService _rotationCalculatorService;

        public CubeRotatorService(ICubeValidatorService cubeValidatorService, IRotationCalculatorService rotationCalculatorService)
        {
            _cubeValidatorService = cubeValidatorService ?? throw new ArgumentNullException(nameof(cubeValidatorService));
            _rotationCalculatorService = rotationCalculatorService ?? throw new ArgumentNullException(nameof(rotationCalculatorService));
        }

        public void Rotate(Dictionary<Faces, List<Colours>> cube, Faces faceRotated, Directions directionRotated)
        {
            _cubeValidatorService.Validate(cube);

            if (faceRotated == default)
            {
                throw new ArgumentNullException(nameof(faceRotated));
            }
            else if (directionRotated == default)
            {
                throw new ArgumentNullException(nameof(directionRotated));
            }

            RotateChosenFace(cube, faceRotated, directionRotated);
            RotateEffectedSides(cube, faceRotated, directionRotated);
        }

        private void RotateChosenFace(Dictionary<Faces, List<Colours>> cube, Faces faceRotated, Directions directionRotated)
        {
            var tempColourValues = new List<Colours>();
            var chosenFace = cube[faceRotated];

            for (int i = 0; i < chosenFace.Count; i++)
            {
                var faceIndex = i % CubeConstants.TilesPerFace;
                var rotatedPosition = _rotationCalculatorService.CalculateRotatedPosition(directionRotated, faceIndex);

                tempColourValues.Add(chosenFace[rotatedPosition]);
            }

            cube[faceRotated] = tempColourValues;
        }

        private void RotateEffectedSides(Dictionary<Faces, List<Colours>> cube, Faces faceRotated, Directions directionRotated)
        {
            var effectedIndexCollection = _rotationCalculatorService.CalculateEffectedSideIndexes(faceRotated);
            var newFaces = _rotationCalculatorService.CalculateFaceChange(faceRotated, directionRotated);

            var tempCube = new Dictionary<Faces, List<Colours>>();

            foreach (var face in effectedIndexCollection.Keys)
            {
                var newFace = newFaces[face];

                tempCube.Add(newFace, cube[newFace]);

                var effectedIndexes = effectedIndexCollection[face];

                for (var i = 0; i < effectedIndexes.Length; i++)
                {
                    var effectedIndex = effectedIndexes[i];

                    if (faceRotated != Faces.Front && faceRotated != Faces.Back)
                    {
                        tempCube[newFace][_rotationCalculatorService.CalculateRotatedPosition(directionRotated, effectedIndex)] = cube[face][effectedIndex];
                    }
                    else
                    {
                        tempCube[newFace][effectedIndex] = cube[face][effectedIndex];
                    }


                }
            }

            foreach (var face in tempCube.Keys)
            {
                cube[face] = tempCube[face];
            }
        }
    }
}