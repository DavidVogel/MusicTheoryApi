using MusicTheory.Domain;

namespace MusicTheory.Services;

/// <summary>
/// Interface for a service that provides functionality related to musical chords
/// </summary>
public interface IChordService
{
    /// <summary>Get the notes of a chord given its root and type</summary>
    Chord GetChord(Note root, ChordType chordType);
}
