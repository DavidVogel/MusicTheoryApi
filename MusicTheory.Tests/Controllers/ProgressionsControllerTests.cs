using Moq;
using Microsoft.AspNetCore.Mvc;
using MusicTheory.Api.Controllers;
using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Tests.Controllers;

/// <summary>
/// Unit tests for the ProgressionsController class.
/// </summary>
public class ProgressionsControllerTests
{
    /// <summary>
    /// Tests the GetCommonProgressions method with valid parameters.
    /// </summary>
    [Fact]
    public void GetCommonProgressions_ValidParameters_ReturnsProgressions()
    {
        // Arrange
        var mockService = new Mock<IProgressionService>();
        var testRoot = new Note(NoteName.C);
        var testScaleType = ScaleType.Major;

        var expectedProgressions = new List<ChordProgression>
        {
            new ChordProgression(
                "I-IV-V",
                new List<Chord>
                {
                    new Chord(new Note(NoteName.C), ChordType.Major),
                    new Chord(new Note(NoteName.F), ChordType.Major),
                    new Chord(new Note(NoteName.G), ChordType.Major)
                }
            )
        };

        mockService.Setup(s => s.GetCommonProgressions(testRoot, testScaleType))
            .Returns(expectedProgressions);

        var controller = new ProgressionsController(mockService.Object);

        // Act
        ActionResult<IEnumerable<ChordProgression>> result = controller.GetCommonProgressions("C", "Major");

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<ChordProgression>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        var actualProgressions = Assert.IsAssignableFrom<IEnumerable<ChordProgression>>(okResult.Value);
        Assert.Equal(expectedProgressions, actualProgressions);

        mockService.Verify(s => s.GetCommonProgressions(testRoot, testScaleType), Times.Once());
    }

    /// <summary>
    /// Tests the GetCommonProgressions method with an invalid root note.
    /// </summary>
    [Fact]
    public void GetCommonProgressions_InvalidRoot_ReturnsBadRequest()
    {
        // Arrange
        var mockService = new Mock<IProgressionService>();
        var controller = new ProgressionsController(mockService.Object);

        // Act
        ActionResult<IEnumerable<ChordProgression>> result = controller.GetCommonProgressions("H", "Major"); // H is not a valid note name

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<ChordProgression>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        Assert.Contains("Invalid note letter", badRequestResult.Value.ToString());
        mockService.Verify(s => s.GetCommonProgressions(It.IsAny<Note>(), It.IsAny<ScaleType>()), Times.Never());
    }

    /// <summary>
    /// Tests the GetCommonProgressions method with an invalid scale type.
    /// </summary>
    [Fact]
    public void GetCommonProgressions_InvalidScaleType_ReturnsBadRequest()
    {
        // Arrange
        var mockService = new Mock<IProgressionService>();
        var controller = new ProgressionsController(mockService.Object);

        // Act
        ActionResult<IEnumerable<ChordProgression>> result = controller.GetCommonProgressions("C", "Mixolydian"); // Mixolydian is not a valid scale type

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<ChordProgression>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        mockService.Verify(s => s.GetCommonProgressions(It.IsAny<Note>(), It.IsAny<ScaleType>()), Times.Never());
    }
}
