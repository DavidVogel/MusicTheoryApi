using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Tests.Services;

/// <summary>
/// Unit tests for the ChordService class.
/// </summary>
public class ChordServiceTests
{
    private readonly ChordService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChordServiceTests"/> class.
    /// </summary>
    public ChordServiceTests()
    {
        _service = new ChordService();
    }

    /// <summary>
    /// Tests that GetChord returns a chord with the correct root note and type.
    /// </summary>
    [Fact]
    public void GetChord_ReturnsChordWithCorrectRootAndType()
    {
        // Arrange
        var rootNote = new Note(NoteName.C);
        var chordType = ChordType.Major;

        // Act
        var chord = _service.GetChord(rootNote, chordType);

        // Assert
        Assert.Equal(rootNote, chord.Root);
        Assert.Equal(chordType, chord.Type);
    }

    /// <summary>
    /// Tests that GetChord returns a major chord with the correct notes.
    /// </summary>
    [Fact]
    public void GetChord_MajorChord_HasCorrectNotes()
    {
        // Arrange
        var rootNote = new Note(NoteName.C);
        var chordType = ChordType.Major;

        // Act
        var chord = _service.GetChord(rootNote, chordType);

        // Assert
        Assert.Equal(3, chord.Notes.Count);
        Assert.Equal(NoteName.C, chord.Notes[0].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[0].Accidental);
        Assert.Equal(NoteName.E, chord.Notes[1].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[1].Accidental);
        Assert.Equal(NoteName.G, chord.Notes[2].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[2].Accidental);
    }

    /// <summary>
    /// Tests that GetChord returns a minor chord with the correct notes.
    /// </summary>
    [Fact]
    public void GetChord_MinorChord_HasCorrectNotes()
    {
        // Arrange
        var rootNote = new Note(NoteName.A);
        var chordType = ChordType.Minor;

        // Act
        var chord = _service.GetChord(rootNote, chordType);

        // Assert
        Assert.Equal(3, chord.Notes.Count);
        Assert.Equal(NoteName.A, chord.Notes[0].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[0].Accidental);
        Assert.Equal(NoteName.C, chord.Notes[1].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[1].Accidental);
        Assert.Equal(NoteName.E, chord.Notes[2].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[2].Accidental);
    }

    /// <summary>
    /// Tests that GetChord returns a diminished chord with the correct notes.
    /// </summary>
    [Fact]
    public void GetChord_DiminishedChord_HasCorrectNotes()
    {
        // Arrange
        var rootNote = new Note(NoteName.B);
        var chordType = ChordType.Diminished;

        // Act
        var chord = _service.GetChord(rootNote, chordType);

        // Assert
        Assert.Equal(3, chord.Notes.Count);
        Assert.Equal(NoteName.B, chord.Notes[0].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[0].Accidental);
        Assert.Equal(NoteName.D, chord.Notes[1].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[1].Accidental);
        Assert.Equal(NoteName.F, chord.Notes[2].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[2].Accidental);
    }

    /// <summary>
    /// Tests that GetChord returns an augmented chord with the correct notes.
    /// </summary>
    [Fact]
    public void GetChord_AugmentedChord_HasCorrectNotes()
    {
        // Arrange
        var rootNote = new Note(NoteName.F);
        var chordType = ChordType.Augmented;

        // Act
        var chord = _service.GetChord(rootNote, chordType);

        // Assert
        Assert.Equal(3, chord.Notes.Count);
        Assert.Equal(NoteName.F, chord.Notes[0].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[0].Accidental);
        Assert.Equal(NoteName.A, chord.Notes[1].Name);
        Assert.Equal(Accidental.Natural, chord.Notes[1].Accidental);
        Assert.Equal(NoteName.C, chord.Notes[2].Name);
        Assert.Equal(Accidental.Sharp, chord.Notes[2].Accidental);
    }

    /// <summary>
    /// Tests that GetChord handles accidentals in the root correctly.
    /// </summary>
    [Fact]
    public void GetChord_RootWithAccidental_GeneratesCorrectNotes()
    {
        // Arrange
        var rootNote = new Note(NoteName.F, Accidental.Sharp);
        var chordType = ChordType.Major;

        // Act
        var chord = _service.GetChord(rootNote, chordType);

        // Assert
        Assert.Equal(3, chord.Notes.Count);
        Assert.Equal(NoteName.F, chord.Notes[0].Name);
        Assert.Equal(Accidental.Sharp, chord.Notes[0].Accidental);
        Assert.Equal(NoteName.A, chord.Notes[1].Name);
        Assert.Equal(Accidental.Sharp, chord.Notes[1].Accidental);
        Assert.Equal(NoteName.C, chord.Notes[2].Name);
        Assert.Equal(Accidental.Sharp, chord.Notes[2].Accidental);
    }
}
