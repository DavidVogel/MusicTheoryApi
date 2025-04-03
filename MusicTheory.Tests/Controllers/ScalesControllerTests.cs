using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MusicTheory.Api.Controllers;
using MusicTheory.Domain; // Namespace where controllers reside
using MusicTheory.Services;             // Namespace for IScaleService interface
using MusicTheory.Api.Models;               // If needed for model classes (Domain)

namespace MusicTheory.Tests.Controllers;

public class ScalesControllerTests
{
    // Example: Test that GetScaleNotes returns the expected notes and calls service once
    [Fact]
    public void GetScaleNotes_ValidScale_ReturnsNotesList()
    {
        // Arrange: create a mock IScaleService and set up expected behavior
        var mockService = new Mock<IScaleService>();

        // string testKey = "C";
        // string testScaleType = "major";

        // Create a Note instance. Adapt the constructor as needed.
        var testNote = new Note(NoteName.C);
        var testScaleType = ScaleType.Major;

        var expectedNotes = new List<string> { "C", "D", "E", "F", "G", "A", "B" };
        mockService.Setup(s => s.GetScaleNotes(testNote, testScaleType))
        // mockService.Setup(s => s.GetScaleNotes(testKey, testScaleType))
            .Returns(expectedNotes);

        // Inject the mock service into a ScalesController instance
        var controller = new ScalesController(mockService.Object);

        // Act: call the controller action
        // var result = controller.GetScaleNotes(testKey, testScaleType);
        // var result = controller.GetScaleNotes("C", "major");
        ActionResult<IEnumerable<string>> result = controller.GetScaleNotes("C", "major");

        // Assert: The result should be an OkObjectResult containing the expected notes
        var actionResult = Assert.IsType<ActionResult<IEnumerable<string>>>(result);
        // var okResult = Assert.IsType<OkObjectResult>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        var actualNotes = Assert.IsAssignableFrom<IEnumerable<string>>(okResult.Value);
        Assert.Equal(expectedNotes, actualNotes);

        // Verify: Ensure the service method was called exactly once with correct parameters
        // mockService.Verify(s => s.GetScaleNotes(testKey, testScaleType), Times.Once());
        mockService.Verify(s => s.GetScaleNotes(testNote, testScaleType), Times.Once());
    }
}
