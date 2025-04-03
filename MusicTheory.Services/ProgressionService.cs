using MusicTheory.Domain;

namespace MusicTheory.Services;

/// <summary>
/// Service for generating and manipulating musical chord progressions.
/// </summary>
public class ProgressionService : IProgressionService
{
    private readonly IScaleService _scaleService;

    // The ProgressionService can use ScaleService to get diatonic chords.
    /// <summary>
    /// Constructor for ProgressionService
    /// </summary>
    /// <param name="scaleService">ScaleService instance</param>
    public ProgressionService(IScaleService scaleService)
    {
        _scaleService = scaleService;
    }

    /// <summary>
    /// Get common chord progressions for the given key (root note and scale type)
    /// </summary>
    /// <param name="keyRoot">The root note of the key</param>
    /// <param name="scaleType">The type of scale (Major, Natural Minor, Harmonic Minor)</param>
    /// <returns>A list of common chord progressions</returns>
    public IEnumerable<ChordProgression> GetCommonProgressions(Note keyRoot, ScaleType scaleType)
    {
        // Get all diatonic chords in the key (I through vii)
        var diatonicChords = _scaleService.GetDiatonicChords(keyRoot, scaleType).ToList();

        var progressions = new List<ChordProgression>();

        if (scaleType == ScaleType.Major)
        {
            // Common progressions in major
            progressions.Add(new ChordProgression(
                "I-IV-V",
                new List<Chord> { diatonicChords[0], diatonicChords[3], diatonicChords[4] } // 1,4,5
            ));
            progressions.Add(new ChordProgression(
                "I-V-vi-IV",
                new List<Chord>
                    { diatonicChords[0], diatonicChords[4], diatonicChords[5], diatonicChords[3] } // 1,5,6,4
            ));
            progressions.Add(new ChordProgression(
                "ii-V-I",
                new List<Chord> { diatonicChords[1], diatonicChords[4], diatonicChords[0] } // 2,5,1
            ));
        }
        else
        {
            // Treat minor (natural minor) or harmonic minor similarly for progressions
            // Common progressions in minor
            progressions.Add(new ChordProgression(
                "i-iv-v",
                new List<Chord>
                    { diatonicChords[0], diatonicChords[3], diatonicChords[4] } // 1,4,5 (in natural minor v is minor)
            ));
            progressions.Add(new ChordProgression(
                "i-VI-III-VII",
                new List<Chord>
                    { diatonicChords[0], diatonicChords[5], diatonicChords[2], diatonicChords[6] } // 1,6,3,7
            ));
        }

        return progressions;
    }
}
