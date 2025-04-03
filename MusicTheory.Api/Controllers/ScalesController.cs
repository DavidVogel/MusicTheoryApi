using MusicTheory.Domain;
using MusicTheory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MusicTheory.Api.Controllers;

/// <summary>Controller for handling scale-related requests</summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ScalesController : ControllerBase
{
    private readonly IScaleService _scaleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScalesController"/> class
    /// </summary>
    /// <param name="scaleService">The service for managing scales</param>
    public ScalesController(IScaleService scaleService)
    {
        _scaleService = scaleService;
    }

    // GET: api/scales/{root}/{scaleType}/notes
    /// <summary>Get notes in a given scale</summary>
    /// <param name="root">The root note of the scale (e.g. "C" or "G#")</param>
    /// <param name="scaleType">The type of scale (e.g. "major", "minor", "harmonicMinor")</param>
    [HttpGet("{root}/{scaleType}/notes")]
    [ProducesResponseType(typeof(IEnumerable<string>), 200)]
    public ActionResult<IEnumerable<string>> GetScaleNotes(string root, string scaleType)
    {
        // Parse inputs
        Note rootNote;
        ScaleType type;
        try
        {
            rootNote = Note.Parse(root);
            type = Enum.Parse<ScaleType>(scaleType, ignoreCase: true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var notes = _scaleService.GetScaleNotes(rootNote, type);
        return Ok(notes);
    }

    // GET: api/scales/{root}/{scaleType}/chords
    /// <summary>Get diatonic chords in a given scale</summary>
    /// <param name="root">The root note of the scale (e.g. "C")</param>
    /// <param name="scaleType">The type of scale (e.g. "major")</param>
    [HttpGet("{root}/{scaleType}/chords")]
    [ProducesResponseType(typeof(IEnumerable<Chord>), 200)]
    public ActionResult<IEnumerable<Chord>> GetScaleChords(string root, string scaleType)
    {
        Note rootNote;
        ScaleType type;
        try
        {
            rootNote = Note.Parse(root);
            type = Enum.Parse<ScaleType>(scaleType, ignoreCase: true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var chords = _scaleService.GetDiatonicChords(rootNote, type);
        return Ok(chords);
    }
}
