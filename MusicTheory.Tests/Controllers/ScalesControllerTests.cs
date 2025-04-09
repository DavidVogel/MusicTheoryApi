using Moq;
using Microsoft.AspNetCore.Mvc;
using MusicTheory.Api.Controllers;
using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Tests.Controllers;

/// <summary>
/// Unit tests for the ScalesController class.
/// </summary>
public class ScalesControllerTests
{
    /// <summary>
    /// Tests the GetScaleNotes method with a valid scale.
    /// </summary>
    [Fact]
    public void GetScaleNotes_ValidScale_ReturnsNotesList()
    {
        var mockService = new Mock<IScaleService>();
        var testNote = new Note(NoteName.C);
        var testScaleType = ScaleType.Major;
        var expectedNotes = new List<string> { "C", "D", "E", "F", "G", "A", "B" };
        mockService.Setup(s => s.GetScaleNotes(testNote, testScaleType))
            .Returns(expectedNotes);

        var controller = new ScalesController(mockService.Object);

        ActionResult<IEnumerable<string>> result = controller.GetScaleNotes("C", "major");

        var actionResult = Assert.IsType<ActionResult<IEnumerable<string>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        var actualNotes = Assert.IsAssignableFrom<IEnumerable<string>>(okResult.Value);
        Assert.Equal(expectedNotes, actualNotes);

        mockService.Verify(s => s.GetScaleNotes(testNote, testScaleType), Times.Once());
    }
}
