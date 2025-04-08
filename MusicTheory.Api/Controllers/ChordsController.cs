using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MusicTheory.Domain;
using MusicTheory.Services;
using MusicTheory.Utils;


namespace MusicTheory.Api.Controllers;

/// <summary>Controller for handling chord-related requests</summary>
[ApiVersion(0)]
[Authorize]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class ChordsController : ControllerBase
{
    private readonly IChordService _chordService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChordsController"/> class.
    /// </summary>
    /// <param name="chordService"></param>
    public ChordsController(IChordService chordService)
    {
        _chordService = chordService;
    }

    // GET: api/v0/chords/{root}/{chordType}
    /// <summary>Get the notes of a chord specified by root and type</summary>
    /// <param name="root">The root note of the chord (e.g. "D", "C-Sharp", "B-Flat")</param>
    /// <param name="chordType">The chord type ("Minor", "Major", "Diminished", or "Augmented")</param>
    /// <returns>If successful, returns a <see cref="Chord"/> object with the root, type, and notes of the chord</returns>
    /// <response code="200">Chord returned successfully</response>
    /// <response code="400">Bad Request</response>
    [MapToApiVersion(0)]
    [HttpGet("{root}/{chordType}")]
    [ProducesResponseType(typeof(Chord), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ReDocCodeSample("cURL", "file://Controllers/Examples/GET_chord_example.curl.txt")]
    public ActionResult<Chord> GetChord(string root, string chordType)
    {
        Note rootNote;
        ChordType type;
        try
        {
            rootNote = Note.Parse(root);
            type = Enum.Parse<ChordType>(chordType, ignoreCase: true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var chord = _chordService.GetChord(rootNote, type);
        return Ok(chord);
    }
}
