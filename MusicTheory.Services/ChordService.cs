using MusicTheory.Domain;

namespace MusicTheory.Services;

/// <summary>
/// Service for generating and manipulating musical chords.
/// </summary>
public class ChordService : IChordService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChordService"/> class.
    /// </summary>
    /// <param name="root">The root note of the chord.</param>
    /// <param name="chordType">The type of the chord (e.g., major, minor).</param>
    /// <returns>A new instance of the <see cref="ChordService"/> class.</returns>
    public Chord GetChord(Note root, ChordType chordType)
    {
        // Generate the chord using the domain model
        Chord chord = new Chord(root, chordType);
        return chord;
    }
}
