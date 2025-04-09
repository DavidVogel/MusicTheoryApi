using MusicTheory.Domain;

namespace MusicTheory.Data;

/// <summary>
/// Interface for a repository that handles scale data.
/// </summary>
public interface IScaleRepository
{
    /// <summary>
    /// Save or update a custom scale (could be user-defined).
    /// </summary>
    /// <param name="scale"></param>
    void SaveScale(Scale scale);

    /// <summary>
    /// Retrieve a scale by its key (e.g., "C Major").
    /// </summary>
    /// <param name="scaleKey">The key for the scale, e.g., "C Major".</param>
    /// <returns>The Scale object if found; otherwise, null.</returns>
    Scale? GetScale(string scaleKey);

    /// <summary>
    /// Retrieve all scales in the repository.
    /// </summary>
    /// <returns>A list of all Scale objects.</returns>
    IEnumerable<Scale> GetAllScales();
}

/// <summary>
/// Interface for a repository that handles chord progression data.
/// </summary>
public interface IProgressionRepository
{
    /// <summary>
    /// Save or update a custom chord progression (could be user-defined).
    /// </summary>
    /// <param name="root">The root key for the progression, e.g., "C".</param>
    /// <param name="scaleType">The scale type, e.g., "Major", "Minor".</param>
    /// <param name="progression">The chord progression to save.</param>
    void SaveProgression(string root, ScaleType scaleType, ChordProgression progression);

    /// <summary>
    /// Retrieve all chord progressions for a given key (if stored).
    /// </summary>
    /// <param name="root">The root key for the progression, e.g., "C".</param>
    /// <param name="scaleType">The scale type, e.g., "Major", "Minor".</param>
    /// <returns>A list of ChordProgression objects.</returns>
    IEnumerable<ChordProgression> GetProgressionsForKey(string root, ScaleType scaleType);
}
