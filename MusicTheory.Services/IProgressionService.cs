using MusicTheory.Domain;

namespace MusicTheory.Services;

/// <summary>
/// Interface for a service that provides functionality related to musical chord progressions
/// </summary>
public interface IProgressionService
{
    /// <summary>Get common chord progressions for the given key (root note and scale type)</summary>
    IEnumerable<ChordProgression> GetCommonProgressions(Note keyRoot, ScaleType scaleType);
}
