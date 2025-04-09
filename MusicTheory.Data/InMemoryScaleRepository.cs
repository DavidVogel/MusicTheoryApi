using MusicTheory.Domain;

namespace MusicTheory.Data;

/// <summary>
/// In-memory implementation of the IScaleRepository interface.
/// </summary>
public class InMemoryScaleRepository : IScaleRepository
{
    private readonly Dictionary<string, Scale> _scales = new();

    /// <summary>
    /// Retrieves a scale by its key.
    /// </summary>
    /// <param name="scaleKey">The key for the scale, e.g., "C Major".</param>
    /// <returns>The Scale object if found; otherwise, null.</returns>
    public Scale? GetScale(string scaleKey)
    {
        _scales.TryGetValue(scaleKey, out Scale? scale);
        return scale;
    }

    /// <summary>
    /// Retrieves all scales in the repository.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Scale> GetAllScales() => _scales.Values.ToList();

    /// <summary>
    /// Saves a scale to the repository.
    /// </summary>
    /// <param name="scale"></param>
    public void SaveScale(Scale scale)
    {
        string key = $"{scale.Root}{(scale.Root.Accidental == Accidental.Natural ? "" : "")} {scale.Type}";
        // Note.ToString() gives something like "C#" or "Db", etc. This forms keys like "C# Major".
        _scales[key] = scale;
    }
}

/// <summary>
/// In-memory implementation of the IProgressionRepository interface.
/// </summary>
public class InMemoryProgressionRepository : IProgressionRepository
{
    // Key for dictionary could be composite of key root and scale type, e.g., "A Minor"
    private readonly Dictionary<string, List<ChordProgression>> _progressions = new();

    /// <summary>
    /// Saves a chord progression to the repository.
    /// </summary>
    /// <param name="root">The root key for the progression, e.g., "C".</param>
    /// <param name="scaleType">The scale type, e.g., "Major", "Minor".</param>
    /// <param name="progression">The chord progression to save.</param>
    public void SaveProgression(string root, ScaleType scaleType, ChordProgression progression)
    {
        string dictKey = $"{root} {scaleType}";
        if (!_progressions.ContainsKey(dictKey))
        {
            _progressions[dictKey] = new List<ChordProgression>();
        }

        _progressions[dictKey].Add(progression);
    }

    /// <summary>
    /// Retrieves all chord progressions for a given key and scale type.
    /// </summary>
    /// <param name="root">The root key for the progression, e.g., "C".</param>
    /// <param name="scaleType">The scale type, e.g., "Major", "Minor".</param>
    /// <returns>A list of chord progressions for the specified key and scale type.</returns>
    public IEnumerable<ChordProgression> GetProgressionsForKey(string root, ScaleType scaleType)
    {
        string dictKey = $"{root} {scaleType}";
        return _progressions.ContainsKey(dictKey) ? _progressions[dictKey] : Enumerable.Empty<ChordProgression>();
    }
}
