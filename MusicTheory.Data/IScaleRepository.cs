using MusicTheory.Domain;

namespace MusicTheory.Data;

public interface IScaleRepository
{
    // Save or update a custom scale (could be user-defined).
    void SaveScale(Scale scale);

    // Retrieve a scale by key (e.g., "C Major").
    Scale? GetScale(string scaleKey);

    // List all stored scales.
    IEnumerable<Scale> GetAllScales();
}

public interface IProgressionRepository
{
    // Save a custom chord progression.
    void SaveProgression(string key, ScaleType scaleType, ChordProgression progression);

    // Get all progressions for a given key (if stored).
    IEnumerable<ChordProgression> GetProgressionsForKey(string key, ScaleType scaleType);
}
