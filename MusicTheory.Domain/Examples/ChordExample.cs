using NSwag.Examples;

namespace MusicTheory.Domain.Examples;

/// <summary>
/// Example provider for the Chord class
/// </summary>
public class ChordExample : IExampleProvider<Chord>
{
    /// <summary>
    /// Provides an example of a Chord object (B Augmented)
    /// </summary>
    /// <returns>A Chord object representing a B Augmented chord</returns>
    public Chord GetExample()
    {
        // Create a B natural note
        var rootNote = new Note(NoteName.B, Accidental.Natural);

        // Create a B Augmented chord - the notes will be automatically generated
        // in the chord constructor according to augmented chord formula
        return new Chord(rootNote, ChordType.Augmented);
    }
}
