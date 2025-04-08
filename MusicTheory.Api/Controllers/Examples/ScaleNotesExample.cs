// To get NSwag to generate a Scale schema

using MusicTheory.Api.Utils;
using MusicTheory.Domain;
using NSwag.Examples;

namespace MusicTheory.Api.Controllers.Examples;

/// <summary>
/// Example provider for ScaleNotesResponse.
/// </summary>
public class ScaleNotesExample : IExampleProvider<ScaleNotesResponse>
{
    public ScaleNotesResponse GetExample()
    {
        return new ScaleNotesResponse
        {
            Notes = new List<string> { "C", "D", "E", "F", "G", "A", "B" }
        };
    }
}
