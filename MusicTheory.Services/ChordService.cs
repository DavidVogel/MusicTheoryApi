using MusicTheory.Domain;

namespace MusicTheory.Services;

/// <summary>
/// Service for generating and manipulating musical chords.
/// </summary>
public class ChordService : IChordService
{
    public Chord GetChord(Note root, ChordType chordType)
    {
        // Generate the chord using the domain model
        Chord chord = new Chord(root, chordType);
        return chord;
    }
}
