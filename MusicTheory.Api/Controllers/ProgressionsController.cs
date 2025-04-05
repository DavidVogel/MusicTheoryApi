using MusicTheory.Domain;
using MusicTheory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace MusicTheory.Api.Controllers;

/// <summary>Controller for managing chord progression requests</summary>
[ApiVersion(0)]
[Authorize]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class ProgressionsController : ControllerBase
{
    private readonly IProgressionService _progressionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressionsController"/> class
    /// </summary>
    /// <param name="progressionService">The service for managing chord progressions</param>
    public ProgressionsController(IProgressionService progressionService)
    {
        _progressionService = progressionService;
    }

    // GET: api/v0/progressions/{root}/{scaleType}/common
    /// <summary>Get common chord progressions for the specified key</summary>
    /// <param name="root">The root note of the key (e.g. "E")</param>
    /// <param name="scaleType">The scale type of the key (e.g. "minor")</param>
    [MapToApiVersion(0)]
    [HttpGet("{root}/{scaleType}/common")]
    [ProducesResponseType(typeof(IEnumerable<ChordProgression>), 200)]
    public ActionResult<IEnumerable<ChordProgression>> GetCommonProgressions(string root, string scaleType)
    {
        Note keyRoot;
        ScaleType type;
        try
        {
            keyRoot = Note.Parse(root);
            type = Enum.Parse<ScaleType>(scaleType, ignoreCase: true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var progressions = _progressionService.GetCommonProgressions(keyRoot, type);
        return Ok(progressions);
    }
}
