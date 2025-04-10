namespace MusicTheory.Domain;

/// <summary>Natural note names A through G</summary>
public enum NoteName
{
    A = 0,
    B = 1,
    C = 2,
    D = 3,
    E = 4,
    F = 5,
    G = 6
}

/// <summary>Accidental offsets applied to a note</summary>
public enum Accidental
{
    QuadrupleFlat = -4,
    TripleFlat = -3,
    DoubleFlat = -2,
    Flat = -1,
    Natural = 0,
    Sharp = 1,
    DoubleSharp = 2,
    TripleSharp = 3,
    QuadrupleSharp = 4
}

/// <summary>Musical interval in semitones</summary>
public enum Interval
{
    PerfectUnison = 0,
    MinorSecond = 1,
    MajorSecond = 2,
    MinorThird = 3,
    MajorThird = 4,
    PerfectFourth = 5,
    DiminishedFifth = 6, // Diminished Fifth (or Augmented Fourth)
    PerfectFifth = 7,
    MinorSixth = 8,
    MajorSixth = 9,
    MinorSeventh = 10,
    MajorSeventh = 11,
    PerfectOctave = 12
}

/// <summary>Types of musical scales</summary>
public enum ScaleType
{
    Major,
    Minor,
    HarmonicMinor /* (TODO: Extend with more types) */
}

/// <summary>Types of triad chords</summary>
public enum ChordType
{
    Major,
    Minor,
    Diminished,
    Augmented /* (TODO: Extend with more types) */
}
