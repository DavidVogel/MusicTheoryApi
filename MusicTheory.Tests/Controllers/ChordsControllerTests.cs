using Moq;
using Microsoft.AspNetCore.Mvc;
using MusicTheory.Api.Controllers;
using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Tests.Controllers;

/// <summary>
/// Unit tests for the ChordsController class.
/// </summary>
public class ChordsControllerTests
{
    /// <summary>
    /// Tests the GetChord method with a valid chord request.
    /// </summary>
    [Fact]
    public void GetChord_ValidChord_ReturnsChord()
    {
        // Arrange
        var mockService = new Mock<IChordService>();
        var testNote = new Note(NoteName.B);
        var testChordType = ChordType.Augmented;
        var expectedChord = new Chord(testNote, testChordType);

        mockService.Setup(s => s.GetChord(testNote, testChordType))
            .Returns(expectedChord);

        var controller = new ChordsController(mockService.Object);

        // Act
        ActionResult<Chord> result = controller.GetChord("B", "Augmented");

        // Assert
        var actionResult = Assert.IsType<ActionResult<Chord>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        var actualChord = Assert.IsType<Chord>(okResult.Value);
        Assert.Equal(expectedChord, actualChord);

        mockService.Verify(s => s.GetChord(testNote, testChordType), Times.Once());
    }

    /// <summary>
    /// Tests the GetChord method with an invalid root note.
    /// </summary>
    [Fact]
    public void GetChord_InvalidRoot_ReturnsBadRequest()
    {
        // Arrange
        var mockService = new Mock<IChordService>();
        var controller = new ChordsController(mockService.Object);

        // Act
        ActionResult<Chord> result = controller.GetChord("H", "Major"); // H is not a valid note name

        // Assert
        var actionResult = Assert.IsType<ActionResult<Chord>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        Assert.Contains("Invalid note letter", badRequestResult.Value.ToString());
        mockService.Verify(s => s.GetChord(It.IsAny<Note>(), It.IsAny<ChordType>()), Times.Never());
    }

    /// <summary>
    /// Tests the GetChord method with an invalid chord type.
    /// </summary>
    [Fact]
    public void GetChord_InvalidChordType_ReturnsBadRequest()
    {
        // Arrange
        var mockService = new Mock<IChordService>();
        var controller = new ChordsController(mockService.Object);

        // Act
        ActionResult<Chord> result = controller.GetChord("C", "SuperMajor"); // SuperMajor is not a valid chord type

        // Assert
        var actionResult = Assert.IsType<ActionResult<Chord>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        mockService.Verify(s => s.GetChord(It.IsAny<Note>(), It.IsAny<ChordType>()), Times.Never());
    }
}
