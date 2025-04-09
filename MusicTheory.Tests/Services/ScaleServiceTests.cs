using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Tests.Services;

/// <summary>
/// Unit tests for the ScaleService class.
/// </summary>
public class ScaleServiceTests
{
    private readonly ScaleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScaleServiceTests"/> class.
    /// </summary>
    public ScaleServiceTests()
    {
        _service = new ScaleService();
    }

    /// <summary>
    /// Tests that GetScaleNotes returns the correct notes for a C major scale.
    /// </summary>
    [Fact]
    public void GetScaleNotes_CMajorScale_ReturnsCorrectNotes()
    {
        // Arrange
        var root = new Note(NoteName.C);
        var scaleType = ScaleType.Major;
        var expectedNotes = new[] { "C", "D", "E", "F", "G", "A", "B", "C" };

        // Act
        var result = _service.GetScaleNotes(root, scaleType).ToArray();

        // Assert
        Assert.Equal(expectedNotes.Length, result.Length);
        for (int i = 0; i < expectedNotes.Length; i++)
        {
            Assert.Equal(expectedNotes[i], result[i]);
        }
    }

    /// <summary>
    /// Tests that GetScaleNotes returns the correct notes for an A minor scale.
    /// </summary>
    [Fact]
    public void GetScaleNotes_AMinorScale_ReturnsCorrectNotes()
    {
        // Arrange
        var root = new Note(NoteName.A);
        var scaleType = ScaleType.Minor;
        var expectedNotes = new[] { "A", "B", "C", "D", "E", "F", "G", "A" };

        // Act
        var result = _service.GetScaleNotes(root, scaleType).ToArray();

        // Assert
        Assert.Equal(expectedNotes.Length, result.Length);
        for (int i = 0; i < expectedNotes.Length; i++)
        {
            Assert.Equal(expectedNotes[i], result[i]);
        }
    }

    /// <summary>
    /// Tests that GetScaleNotes returns the correct notes for a scale with accidentals.
    /// </summary>
    [Fact]
    public void GetScaleNotes_FSharpMajorScale_ReturnsCorrectNotes()
    {
        // Arrange
        var root = new Note(NoteName.F, Accidental.Sharp);
        var scaleType = ScaleType.Major;
        var expectedNotes = new[] { "F#", "G#", "A#", "B", "C#", "D#", "E#", "F#" };

        // Act
        var result = _service.GetScaleNotes(root, scaleType).ToArray();

        // Assert
        Assert.Equal(expectedNotes.Length, result.Length);
        for (int i = 0; i < expectedNotes.Length; i++)
        {
            Assert.Equal(expectedNotes[i], result[i]);
        }
    }

    /// <summary>
    /// Tests that GetDiatonicChords returns the correct chords for a C major scale.
    /// </summary>
    [Fact]
    public void GetDiatonicChords_CMajorScale_ReturnsCorrectChords()
    {
        // Arrange
        var root = new Note(NoteName.C);
        var scaleType = ScaleType.Major;

        // Expected chord types for the C major scale
        var expectedRoots = new[] { NoteName.C, NoteName.D, NoteName.E, NoteName.F, NoteName.G, NoteName.A, NoteName.B };
        var expectedTypes = new[] {
            ChordType.Major, ChordType.Minor, ChordType.Minor,
            ChordType.Major, ChordType.Major, ChordType.Minor,
            ChordType.Diminished
        };

        // Act
        var chords = _service.GetDiatonicChords(root, scaleType).ToArray();

        // Assert
        Assert.Equal(7, chords.Length);
        for (int i = 0; i < 7; i++)
        {
            Assert.Equal(expectedRoots[i], chords[i].Root.Name);
            Assert.Equal(expectedTypes[i], chords[i].Type);
        }
    }

    /// <summary>
    /// Tests that GetDiatonicChords returns the correct chords for an A minor scale.
    /// </summary>
    [Fact]
    public void GetDiatonicChords_AMinorScale_ReturnsCorrectChords()
    {
        // Arrange
        var root = new Note(NoteName.A);
        var scaleType = ScaleType.Minor;

        // Expected chord types for the A minor scale
        var expectedRoots = new[] { NoteName.A, NoteName.B, NoteName.C, NoteName.D, NoteName.E, NoteName.F, NoteName.G };
        var expectedTypes = new[] {
            ChordType.Minor, ChordType.Diminished, ChordType.Major,
            ChordType.Minor, ChordType.Minor, ChordType.Major,
            ChordType.Major
        };

        // Act
        var chords = _service.GetDiatonicChords(root, scaleType).ToArray();

        // Assert
        Assert.Equal(7, chords.Length);
        for (int i = 0; i < 7; i++)
        {
            Assert.Equal(expectedRoots[i], chords[i].Root.Name);
            Assert.Equal(expectedTypes[i], chords[i].Type);
        }
    }

    /// <summary>
    /// Tests that GetDiatonicChords returns the correct chords for a scale with accidentals.
    /// </summary>
    [Fact]
    public void GetDiatonicChords_EFlatMajorScale_ReturnsCorrectChords()
    {
        // Arrange
        var root = new Note(NoteName.E, Accidental.Flat);
        var scaleType = ScaleType.Major;

        // Expected chord roots and types for Eb major scale
        var expectedRoots = new[] { NoteName.E, NoteName.F, NoteName.G, NoteName.A, NoteName.B, NoteName.C, NoteName.D };
        var expectedAccidentals = new[] {
            Accidental.Flat, Accidental.Natural, Accidental.Natural,
            Accidental.Flat, Accidental.Flat, Accidental.Natural,
            Accidental.Natural
        };
        var expectedTypes = new[] {
            ChordType.Major, ChordType.Minor, ChordType.Minor,
            ChordType.Major, ChordType.Major, ChordType.Minor,
            ChordType.Diminished
        };

        // Act
        var chords = _service.GetDiatonicChords(root, scaleType).ToArray();

        // Assert
        Assert.Equal(7, chords.Length);
        for (int i = 0; i < 7; i++)
        {
            Assert.Equal(expectedRoots[i], chords[i].Root.Name);
            Assert.Equal(expectedAccidentals[i], chords[i].Root.Accidental);
            Assert.Equal(expectedTypes[i], chords[i].Type);
        }
    }
}
