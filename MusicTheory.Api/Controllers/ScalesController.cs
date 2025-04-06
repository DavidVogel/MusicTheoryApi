using MusicTheory.Domain;
using MusicTheory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace MusicTheory.Api.Controllers;

/// <summary>Controller for handling scale-related requests</summary>
[ApiVersion(0)]
[Authorize]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
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

    // GET: api/v0/scales/{root}/{scaleType}/notes
    /// <summary>Get notes in a given scale</summary>
    /// <param name="root">The root note of the scale (e.g. "C", "G-Sharp", "B-Flat")</param>
    /// <param name="scaleType">The type of scale (e.g. "Major", "Minor", "HarmonicMinor")</param>
    /// <response code="200">Returns a list of notes in the scale</response>
    /// <response code="400">Bad Request</response>
    [MapToApiVersion(0)]
    [HttpGet("{root}/{scaleType}/notes")]
    [ProducesResponseType(typeof(IEnumerable<string>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    // GET: api/v0/scales/{root}/{scaleType}/chords
    /// <summary>Get diatonic chords in a given scale</summary>
    /// <param name="root">The root note of the scale (e.g. "C", "F-Sharp", "A-Flat")</param>
    /// <param name="scaleType">The type of scale ("Major" or "Minor")</param>
    /// <response code="200">Returns a list of diatonic chords in the scale</response>
    /// <response code="400">Bad Request</response>
    [MapToApiVersion(0)]
    [HttpGet("{root}/{scaleType}/chords")]
    [ProducesResponseType(typeof(IEnumerable<Chord>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
