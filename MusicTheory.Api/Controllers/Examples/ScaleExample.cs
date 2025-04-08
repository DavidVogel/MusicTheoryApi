// File: MusicTheory.Api/Controllers/Examples/ScaleExample.cs
using MusicTheory.Domain;
using NSwag.Examples;

namespace MusicTheory.Api.Controllers.Examples
{
    /// <summary>
    /// Example provider for the Scale class.
    /// </summary>
    public class ScaleExample : IExampleProvider<Scale>
    {
        public Scale GetExample()
        {
            // Create a D flat note.
            var rootNote = new Note(NoteName.G, Accidental.Sharp);
            // Define the scale type.
            var scaleType = ScaleType.HarmonicMinor;
            // Create and return the scale.
            return new Scale(rootNote, scaleType);
        }
    }
}
