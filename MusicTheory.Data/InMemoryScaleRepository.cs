using MusicTheory.Domain;

namespace MusicTheory.Data;

// A simple in-memory implementation of IScaleRepository.
public class InMemoryScaleRepository : IScaleRepository
{
    // Dictionary key example: "C Major" -> Scale object
    private readonly Dictionary<string, Scale> _scales = new();

    public Scale? GetScale(string scaleKey)
    {
        _scales.TryGetValue(scaleKey, out Scale? scale);
        return scale;
    }

    public IEnumerable<Scale> GetAllScales() => _scales.Values.ToList();

    public void SaveScale(Scale scale)
    {
        string key = $"{scale.Root}{(scale.Root.Accidental == Accidental.Natural ? "" : "")} {scale.Type}";
        // Note.ToString() gives something like "C#" or "Db", etc. This forms keys like "C# Major".
        _scales[key] = scale;
    }
}

// A simple in-memory implementation of IProgressionRepository.
public class InMemoryProgressionRepository : IProgressionRepository
{
    // Key for dictionary could be composite of key root and scale type, e.g., "A Minor"
    private readonly Dictionary<string, List<ChordProgression>> _progressions = new();

    public void SaveProgression(string key, ScaleType scaleType, ChordProgression progression)
    {
        string dictKey = $"{key} {scaleType}";
        if (!_progressions.ContainsKey(dictKey))
        {
            _progressions[dictKey] = new List<ChordProgression>();
        }

        _progressions[dictKey].Add(progression);
    }

    public IEnumerable<ChordProgression> GetProgressionsForKey(string key, ScaleType scaleType)
    {
        string dictKey = $"{key} {scaleType}";
        return _progressions.ContainsKey(dictKey) ? _progressions[dictKey] : Enumerable.Empty<ChordProgression>();
    }
}
