using MusicTheory.Domain;

namespace MusicTheory.Services;

/// <summary>
/// Interface for a service that provides functionality related to musical scales
/// </summary>
public interface IScaleService
{
    /// <summary>Get the notes of the specified scale</summary>
    IEnumerable<string> GetScaleNotes(Note root, ScaleType scaleType);

    /// <summary>Get the diatonic triad chords of the specified scale</summary>
    IEnumerable<Chord> GetDiatonicChords(Note root, ScaleType scaleType);
}
