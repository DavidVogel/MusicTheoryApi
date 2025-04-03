using MusicTheory.Domain;
using MusicTheory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicTheory.Api.Controllers;

/// <summary>Controller for handling chord-related requests</summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
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

    // GET: api/chords/{root}/{chordType}
    /// <summary>Gets the notes of a chord specified by root and type</summary>
    /// <param name="root">The root note of the chord (e.g. "D")</param>
    /// <param name="chordType">The chord type (e.g. "minor", "Major")</param>
    [HttpGet("{root}/{chordType}")]
    [ProducesResponseType(typeof(Chord), 200)]
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
