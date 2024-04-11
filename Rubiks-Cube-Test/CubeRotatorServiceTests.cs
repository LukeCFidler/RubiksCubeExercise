namespace Rubiks_Cube_Test
{
    using Moq;
    using Rubiks_Cube_Solver.Models.Enums;
    using Rubiks_Cube_Solver.Models;
    using Rubiks_Cube_Solver.Services;
    using Rubiks_Cube_Solver.Services.Interfaces;
    using Newtonsoft.Json;

    [TestClass]
    public class CubeRotatorServiceTests
    {
        private Mock<ICubeValidatorService> mockCubeValidatorService;
        private Mock<IRotationCalculatorService> mockRotationCalculatorService;

        private CubeRotatorService cubeRotatorService;

        public CubeRotatorServiceTests()
        {
            mockCubeValidatorService = new Mock<ICubeValidatorService>();
            mockRotationCalculatorService = new Mock<IRotationCalculatorService>();

            cubeRotatorService = new CubeRotatorService(mockCubeValidatorService.Object, mockRotationCalculatorService.Object);
        }

        [TestMethod]
        public void ValidParams_Success()
        {
            // Arrange 
            var cube = new Dictionary<Faces, List<Colours>>();

            for (int i = 0; i < CubeConstants.FacesPerCube; i++)
            {
                var enumIndex = i + 1;

                cube.Add(((Faces)enumIndex), Enumerable.Repeat((Colours)enumIndex, CubeConstants.TilesPerFace).ToList());
            }

            var face = Faces.Front;
            var direction = Directions.Clockwise;

            mockRotationCalculatorService.Setup(mock => mock.CalculateFaceChange(face, direction)).Returns(new Dictionary<Faces, Faces>()
            {
                { Faces.Top, Faces.Right },
                { Faces.Right, Faces.Bottom },
                { Faces.Bottom, Faces.Left },
                { Faces.Left, Faces.Top }
            });

            mockRotationCalculatorService.Setup(mock => mock.CalculateEffectedSideIndexes(face)).Returns(new Dictionary<Faces, int[]>()
            {
                { Faces.Top, [6, 7, 8] },
                { Faces.Bottom, [0, 1, 2] },
                { Faces.Left, [2, 5, 8] },
                { Faces.Right, [0, 3, 6] }
            });

            mockRotationCalculatorService.Setup(mock => mock.CalculateRotatedPosition(direction, It.IsAny<int>())).Returns(1);

            // Act
            cubeRotatorService.Rotate(cube, face, direction);

            // Assert
            mockCubeValidatorService.Verify(mock =>
                mock.Validate(It.Is<Dictionary<Faces, List<Colours>>>(createdCube =>
                    JsonConvert.SerializeObject(cube).Equals(JsonConvert.SerializeObject(createdCube)))), Times.Once);

            mockRotationCalculatorService.Verify(mock => mock.CalculateFaceChange(face, direction), Times.Once);
            mockRotationCalculatorService.Verify(mock => mock.CalculateEffectedSideIndexes(face), Times.Once);
            mockRotationCalculatorService.Verify(mock => mock.CalculateRotatedPosition(direction, It.IsInRange(0, 8, Range.Inclusive)), Times.Exactly(CubeConstants.TilesPerFace));
        }

        [TestMethod]
        public void InvalidFace_ThrowsArgumentNullException()
        {
            // Arrange 
            // Act
            try
            {
                cubeRotatorService.Rotate(new Dictionary<Faces, List<Colours>>(), 0, Directions.AntiClockwise);
            }
            catch (ArgumentNullException)
            {
                // Assert
                mockCubeValidatorService.Verify(mock => mock.Validate(It.IsAny<Dictionary<Faces, List<Colours>>>()), Times.Once);
            }
            catch
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void InvalidDirection_ThrowsArgumentNullException()
        {
            // Arrange
            try
            {
                // Act
                cubeRotatorService.Rotate(new Dictionary<Faces, List<Colours>>(), Faces.Bottom, 0);
            }
            catch (ArgumentNullException)
            {
                // Assert
                mockCubeValidatorService.Verify(mock => mock.Validate(It.IsAny<Dictionary<Faces, List<Colours>>>()), Times.Once);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}