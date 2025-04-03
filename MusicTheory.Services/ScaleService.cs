using MusicTheory.Domain;

namespace MusicTheory.Services;

/// <summary>
/// Service for generating and manipulating musical scales
/// </summary>
public class ScaleService : IScaleService
{
    /// <summary>
    /// Get the notes of the specified scale
    /// </summary>
    /// <param name="root">The root note of the scale</param>
    /// <param name="scaleType">The type of scale (e.g., Major, Minor)</param>
    /// <returns>A list of note names in the scale</returns>
    public IEnumerable<string> GetScaleNotes(Note root, ScaleType scaleType)
    {
        // Generate the scale using the domain model
        Scale scale = new Scale(root, scaleType);
        // Return note names as strings (e.g. ["G#", "A#", "B", ...]).
        return scale.Notes.Select(n => n.ToString());
    }

    /// <summary>
    /// Get the diatonic triad chords of the specified scale
    /// </summary>
    /// <param name="root">The root note of the scale</param>
    /// <param name="scaleType">The type of scale (e.g., Major, Minor)</param>
    /// <returns>A list of diatonic chords in the scale</returns>
    public IEnumerable<Chord> GetDiatonicChords(Note root, ScaleType scaleType)
    {
        // First, get the scale notes
        Scale scale = new Scale(root, scaleType);
        List<Note> scaleNotes = scale.Notes;
        var chords = new List<Chord>();

        // The scale.Notes list contains 8 notes (root through octave).
        // We use only degrees 1-7 to form diatonic triads.
        int degreeCount = scaleNotes.Count - 1; // ignore the repeated octave note
        for (int i = 0; i < degreeCount; i++)
        {
            // Root of chord = scale degree i+1 (0-indexed in list)
            Note chordRoot = scaleNotes[i];
            // Third = two scale degrees ahead (wrap around using mod)
            Note third = scaleNotes[(i + 2) % degreeCount];
            // Fifth = four scale degrees ahead (wrap around)
            Note fifth = scaleNotes[(i + 4) % degreeCount];

            // Determine chord type by interval pattern from root
            int interval1 = (third.PitchClass - chordRoot.PitchClass + 12) % 12;
            int interval2 = (fifth.PitchClass - chordRoot.PitchClass + 12) % 12;
            ChordType chordType = ChordType.Major;
            if (interval1 == 3 && interval2 == 7) chordType = ChordType.Minor;
            if (interval1 == 3 && interval2 == 6) chordType = ChordType.Diminished;
            if (interval1 == 4 && interval2 == 8) chordType = ChordType.Augmented;
            if (interval1 == 4 && interval2 == 7) chordType = ChordType.Major;
            // Create the chord (this will internally generate the same notes, with proper spelling)
            chords.Add(new Chord(chordRoot, chordType));
        }

        return chords;
    }
}
