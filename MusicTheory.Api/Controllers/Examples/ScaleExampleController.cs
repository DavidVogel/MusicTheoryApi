using MusicTheory.Domain;
using Microsoft.AspNetCore.Mvc;

// To get NSwag to generate a Scale schema

namespace MusicTheory.Api.Controllers.Examples;

/// <summary>
/// Example provider for the Scale class.
/// </summary>
[ApiController]
[Route("api/v{v:apiVersion}/examples")]
public class ScaleExampleController : ControllerBase
{
    /// <summary>
    /// Provides an example of a Scale object (G# Harmonic Minor).
    /// </summary>
    /// <returns></returns>
    [HttpGet("scale")]
    [ProducesResponseType(typeof(Scale), 200)]
    public ActionResult<Scale> GetScaleExample()
    {
        // Create a Scale using ScaleExample
        var rootNote = new Note(NoteName.G, Accidental.Sharp);
        var scaleType = ScaleType.HarmonicMinor;
        var scale = new Scale(rootNote, scaleType);
        return Ok(scale);
    }
}
