using MusicTheory.Api.Utils;
using NSwag.Examples;

// To get NSwag to generate a Scale schema

namespace MusicTheory.Api.Controllers.Examples;

/// <summary>
/// Example provider for ScaleNotesResponse.
/// </summary>
public class ScaleNotesExample : IExampleProvider<ScaleNotesResponse>
{
    /// <summary>
    /// Provides an example of a ScaleNotesResponse object.
    /// </summary>
    /// <returns>A ScaleNotesResponse object with a list of notes.</returns>
    public ScaleNotesResponse GetExample()
    {
        return new ScaleNotesResponse
        {
            Notes = new List<string> { "C", "D", "E", "F", "G", "A", "B" }
        };
    }
}
