using MusicTheory.Domain;
using NSwag.Examples;

namespace MusicTheory.Api.Controllers.Examples;

/// <summary>
/// Example provider for the Scale class.
/// </summary>
public class ScaleExample : IExampleProvider<Scale>
{
    /// <summary>
    /// Provides an example of a Scale object (G# Harmonic Minor).
    /// </summary>
    /// <returns>A Scale object representing a G# Harmonic Minor scale</returns>
    public Scale GetExample()
    {
        var rootNote = new Note(NoteName.G, Accidental.Sharp);
        var scaleType = ScaleType.HarmonicMinor;
        return new Scale(rootNote, scaleType);
    }
}
