using System.ComponentModel.DataAnnotations;

namespace MusicTheory.Domain;

/// <summary>
/// Represents a musical scale with a root note and a scale type
/// </summary>
public class Scale
{
    /// <summary>
    /// The root note of the scale (e.g., "C", "D-Sharp", "G-Flat", etc.)
    /// </summary>
    [Required]
    public Note Root { get; set; }

    /// <summary>
    /// The type of scale ("Major", "Minor", or "HarmonicMinor")
    /// </summary>
    [Required]
    public ScaleType Type { get; set; }

    /// <summary>
    /// The notes of the scale generated from the root and type
    /// </summary>
    [Required]
    public List<Note> Notes { get; private set; }

    // Interval patterns for each scale type (semitones between successive scale notes).
    private static readonly Dictionary<ScaleType, int[]> ScaleIntervals = new()
    {
        { ScaleType.Major, new[] { 2, 2, 1, 2, 2, 2, 1 } }, // W-W-H-W-W-W-H
        { ScaleType.Minor, new[] { 2, 1, 2, 2, 1, 2, 2 } }, // Natural minor: W-H-W-W-H-W-W
        { ScaleType.HarmonicMinor, new[] { 2, 1, 2, 2, 1, 3, 1 } } // Harmonic minor: W-H-W-W-H-A2-H
    };

    /// <summary>
    /// Constructor to create a scale with a given root note and type
    /// </summary>
    /// <param name="root">The root note of the scale</param>
    /// <param name="type">The type of scale</param>
    public Scale(Note root, ScaleType type)
    {
        Root = root;
        Type = type;
        Notes = GenerateScaleNotes(root, type);
    }


    /// <summary>
    /// Generate the notes of the scale based on the root and scale type.
    /// Ensures correct enharmonic spelling for each degree.
    /// </summary>
    private List<Note> GenerateScaleNotes(Note root, ScaleType type)
    {
        var notes = new List<Note> { root };
        if (!ScaleIntervals.ContainsKey(type))
            return notes;

        int[] intervals = ScaleIntervals[type];
        int currentPitch = root.PitchClass;
        int letterIndex = (int)root.Name;
        // Generate notes by applying each interval in sequence
        foreach (int interval in intervals)
        {
            currentPitch = (currentPitch + interval) % 12;
            letterIndex = (letterIndex + 1) % 7; // advance to next letter in alphabet
            NoteName nextLetter = (NoteName)letterIndex;
            // Determine accidental for nextLetter to match target pitch
            int basePitch = Note.NaturalSemitone[nextLetter];
            int diff = currentPitch - basePitch;
            if (diff > 6) diff -= 12;
            if (diff < -5) diff += 12;
            var accidental = (Accidental)diff;
            notes.Add(new Note(nextLetter, accidental));
        }

        return notes;
    }
}
