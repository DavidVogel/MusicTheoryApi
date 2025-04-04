namespace MusicTheory.Domain;

/// <summary>
/// Represents a musical note (with letter name and accidental)
/// </summary>
public class Note : IEquatable<Note>
{
    /// <summary>
    /// The letter name of the note (A-G)
    /// </summary>
    public NoteName Name { get; set; }

    /// <summary>
    /// The accidental of the note (e.g., sharp, flat, natural)
    /// </summary>
    public Accidental Accidental { get; set; }

    // Natural note base semitone values relative to C = 0.
    internal static readonly Dictionary<NoteName, int> NaturalSemitone = new()
    {
        { NoteName.C, 0 }, { NoteName.D, 2 }, { NoteName.E, 4 },
        { NoteName.F, 5 }, { NoteName.G, 7 }, { NoteName.A, 9 },
        { NoteName.B, 11 }
    };

    /// <summary>
    /// Constructor to create a note with a given name and accidental
    /// </summary>
    /// <param name="name">The letter name of the note (A-G)</param>
    /// <param name="accidental">The accidental of the note (e.g., sharp, flat)</param>
    public Note(NoteName name, Accidental accidental = Accidental.Natural)
    {
        Name = name;
        Accidental = accidental;
    }

    /// <summary>The pitch class (0-11) of this note (ignoring octave)</summary>
    public int PitchClass
        => (NaturalSemitone[Name] + (int)Accidental + 12) % 12;

    /// <summary>Returns a string like "C", "F#", "Eb", "E##" for the note</summary>
    public override string ToString()
    {
        string letter = Name.ToString(); // "A", "B", ...
        string accidentalStr = Accidental switch
        {
            Accidental.DoubleFlat => "bb",
            Accidental.Flat => "b",
            Accidental.Natural => "",
            Accidental.Sharp => "#",
            Accidental.DoubleSharp => "##",
            _ => ""
        };
        return letter + accidentalStr;
    }

    /// <summary>
    /// Determines if this note is enharmonically equivalent to another
    /// </summary>
    public bool Equals(Note? other)
        => other != null && PitchClass == other.PitchClass;

    /// <summary>
    /// Override Equals to compare notes by their pitch class (ignoring octave).
    /// </summary>
    /// <param name="obj">The object to compare with</param>
    /// <returns>True if the notes are enharmonically equivalent</returns>
    public override bool Equals(object? obj) => obj is Note other && Equals(other);

    /// <summary>
    /// Override GetHashCode to Hash by pitch class for enharmonic equivalence
    /// </summary>
    /// <returns>The hash code for the note</returns>
    public override int GetHashCode() => PitchClass;

    /// <summary>
    /// Returns an alternate enharmonic spelling of this note (if one exists).
    /// </summary>
    public Note GetEnharmonicEquivalent()
    {
        // Calculate an alternate by shifting the letter and adjusting the accidental.
        // If current note is natural or sharp, try spelling as the previous letter raised.
        // If current note is flat, try spelling as the next letter lowered.
        int currentLetterIndex = (int)Name;
        int targetPitch = PitchClass;
        if (Accidental >= Accidental.Natural)
        {
            // For natural or sharp notes, use previous letter with flats (e.g., C# -> Db, C -> B#).
            int prevLetterIndex = (currentLetterIndex - 1 + 7) % 7;
            NoteName prevLetter = (NoteName)prevLetterIndex;
            int basePitch = NaturalSemitone[prevLetter];
            int diff = targetPitch - basePitch;
            if (diff > 6) diff -= 12;
            if (diff < -5) diff += 12;
            // diff now in range [-5,6]; we expect -2 <= diff <= 2 normally.
            return new Note(prevLetter, (Accidental)diff);
        }
        else
        {
            // For flat notes, use next letter with sharps (e.g., Db -> C#).
            int nextLetterIndex = (currentLetterIndex + 1) % 7;
            NoteName nextLetter = (NoteName)nextLetterIndex;
            int basePitch = NaturalSemitone[nextLetter];
            int diff = targetPitch - basePitch;
            if (diff > 6) diff -= 12;
            if (diff < -5) diff += 12;
            return new Note(nextLetter, (Accidental)diff);
        }
    }

    /// <summary>
    /// Parse a note string (like "C", "F#", "Eb") into a Note object.
    /// </summary>
    public static Note Parse(string noteStr)
    {
        if (string.IsNullOrWhiteSpace(noteStr))
        {
            throw new ArgumentException("Note string is empty");
        }

        noteStr = noteStr.Trim();

        // Get the note letter from the first character.
        char letterChar = char.ToUpper(noteStr[0]);

        if (letterChar < 'A' || letterChar > 'G')
        {
            throw new ArgumentException($"Invalid note letter: {letterChar}");
        }

        NoteName letter = (NoteName)("ABCDEFG".IndexOf(letterChar));

        // Get and normalize accidental string (everything after the letter).
        string accidentalPart = noteStr.Length > 1 ? noteStr[1..].Trim().ToLowerInvariant() : "";

        // Normalize common accidental formats.
        if (accidentalPart.Contains("sharp"))
        {
            accidentalPart = "#";
        }
        else if (accidentalPart.Contains("flat"))
        {
            accidentalPart = "b";
        }

        // Remove any hyphens or spaces (for inputs like "C-Sharp" or "b-flat").
        accidentalPart = accidentalPart.Replace("-", "").Replace(" ", "");

        Accidental accidental = Accidental.Natural;

        if (!string.IsNullOrEmpty(accidentalPart))
        {
            accidental = accidentalPart switch
            {
                "b" => Accidental.Flat,
                "bb" => Accidental.DoubleFlat,
                "#" => Accidental.Sharp,
                "##" => Accidental.DoubleSharp,
                _ => throw new ArgumentException($"Invalid accidental: {accidentalPart}")
            };
        }

        return new Note(letter, accidental);
    }
}
