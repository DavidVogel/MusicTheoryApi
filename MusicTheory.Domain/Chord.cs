using System.ComponentModel.DataAnnotations;

namespace MusicTheory.Domain;

/// <summary>
/// Represents a chord (triad only) with a root note and chord type
/// </summary>
public class Chord
{
    /// <summary>
    /// The root note of the chord (e.g., "C", "D-Sharp", "E-Flat", etc.)
    /// </summary>
    [Required]
    public Note Root { get; set; }

    /// <summary>
    ///  The type of chord ("Major", "Minor", "Diminished", or "Augmented")
    /// </summary>
    [Required]
    public ChordType Type { get; set; }

    /// <summary>
    /// The notes of the chord (triad) generated from the root and type
    /// </summary>
    [Required]
    public List<Note> Notes { get; private set; }

    // Interval patterns for chord types (from root to other chord tones).
    // For triads: two intervals (root->third, third->fifth).
    private static readonly Dictionary<ChordType, int[]> ChordIntervals = new()
    {
        { ChordType.Major, new[] { 4, 3 } }, // root->Maj3, then ->min3 (total 7 semitones to fifth)
        { ChordType.Minor, new[] { 3, 4 } }, // root->min3, then ->Maj3
        { ChordType.Diminished, new[] { 3, 3 } }, // two minor 3rds (diminished 5th total 6 semitones)
        { ChordType.Augmented, new[] { 4, 4 } } // two major 3rds (augmented 5th total 8 semitones)
    };

    /// <summary>
    /// Constructor to create a chord with a given root note and type
    /// </summary>
    /// <param name="root">The root note of the chord</param>
    /// <param name="type">The type of chord</param>
    public Chord(Note root, ChordType type)
    {
        Root = root;
        Type = type;
        Notes = GenerateChordNotes(root, type);
    }

    /// <summary>
    /// Generate the notes of the chord (triad) based on root and type.
    /// Ensures each chord tone is spelled with the correct letter.
    /// </summary>
    private List<Note> GenerateChordNotes(Note root, ChordType type)
    {
        var notes = new List<Note> { root };
        if (!ChordIntervals.ContainsKey(type)) return notes;
        int[] intervals = ChordIntervals[type];
        int rootPitch = root.PitchClass;
        int rootLetterIndex = (int)root.Name;
        int sumInterval = 0;
        for (int i = 0; i < intervals.Length; i++)
        {
            sumInterval += intervals[i];
            int targetPitch = (rootPitch + sumInterval) % 12;
            // For triads, each chord tone is a third above the previous,
            // so advance the letter by 2 for each interval step.
            int targetLetterIndex = (rootLetterIndex + 2 * (i + 1)) % 7;
            NoteName targetLetter = (NoteName)targetLetterIndex;
            int basePitch = Note.NaturalSemitone[targetLetter];
            int diff = targetPitch - basePitch;
            if (diff > 6) diff -= 12;
            if (diff < -5) diff += 12;
            var accidental = (Accidental)diff;
            notes.Add(new Note(targetLetter, accidental));
        }

        return notes;
    }

    /// <summary>
    /// Override ToString() to provide a string representation of the chord
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{Root}{Type}";
}
