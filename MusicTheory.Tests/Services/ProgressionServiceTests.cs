using Moq;
using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Tests.Services;

/// <summary>
/// Unit tests for the ProgressionService class.
/// </summary>
public class ProgressionServiceTests
{
    private readonly Mock<IScaleService> _mockScaleService;
    private readonly ProgressionService _service;
    private readonly Note _cMajorRoot;
    private readonly Note _aMinorRoot;
    private readonly List<Chord> _majorDiatonicChords;
    private readonly List<Chord> _minorDiatonicChords;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressionServiceTests"/> class.
    /// </summary>
    public ProgressionServiceTests()
    {
        _mockScaleService = new Mock<IScaleService>();
        _service = new ProgressionService(_mockScaleService.Object);

        // Setup test data
        _cMajorRoot = new Note(NoteName.C);
        _aMinorRoot = new Note(NoteName.A);

        // Create mock diatonic chords for C Major
        _majorDiatonicChords = new List<Chord>
        {
            new Chord(new Note(NoteName.C), ChordType.Major),       // I
            new Chord(new Note(NoteName.D), ChordType.Minor),       // ii
            new Chord(new Note(NoteName.E), ChordType.Minor),       // iii
            new Chord(new Note(NoteName.F), ChordType.Major),       // IV
            new Chord(new Note(NoteName.G), ChordType.Major),       // V
            new Chord(new Note(NoteName.A), ChordType.Minor),       // vi
            new Chord(new Note(NoteName.B), ChordType.Diminished)   // vii°
        };

        // Create mock diatonic chords for A Minor
        _minorDiatonicChords = new List<Chord>
        {
            new Chord(new Note(NoteName.A), ChordType.Minor),       // i
            new Chord(new Note(NoteName.B), ChordType.Diminished),  // ii°
            new Chord(new Note(NoteName.C), ChordType.Major),       // III
            new Chord(new Note(NoteName.D), ChordType.Minor),       // iv
            new Chord(new Note(NoteName.E), ChordType.Minor),       // v
            new Chord(new Note(NoteName.F), ChordType.Major),       // VI
            new Chord(new Note(NoteName.G), ChordType.Major)        // VII
        };
    }

    /// <summary>
    /// Tests that GetCommonProgressions calls ScaleService to get diatonic chords.
    /// </summary>
    [Fact]
    public void GetCommonProgressions_CallsScaleServiceForDiatonicChords()
    {
        // Arrange
        _mockScaleService.Setup(s => s.GetDiatonicChords(_cMajorRoot, ScaleType.Major))
            .Returns(_majorDiatonicChords);

        // Act
        var result = _service.GetCommonProgressions(_cMajorRoot, ScaleType.Major);

        // Assert
        _mockScaleService.Verify(s => s.GetDiatonicChords(_cMajorRoot, ScaleType.Major), Times.Once());
    }

    /// <summary>
    /// Tests that GetCommonProgressions returns the expected major progressions.
    /// </summary>
    [Fact]
    public void GetCommonProgressions_MajorScale_ReturnsExpectedProgressions()
    {
        // Arrange
        _mockScaleService.Setup(s => s.GetDiatonicChords(_cMajorRoot, ScaleType.Major))
            .Returns(_majorDiatonicChords);

        // Act
        var progressions = _service.GetCommonProgressions(_cMajorRoot, ScaleType.Major).ToList();

        // Assert
        Assert.Equal(3, progressions.Count);

        // Check progression patterns
        Assert.Equal("I-IV-V", progressions[0].Pattern);
        Assert.Equal("I-V-vi-IV", progressions[1].Pattern);
        Assert.Equal("ii-V-I", progressions[2].Pattern);

        // Check chords in first progression (I-IV-V)
        Assert.Equal(3, progressions[0].Chords.Count);
        Assert.Equal(_majorDiatonicChords[0], progressions[0].Chords[0]); // I
        Assert.Equal(_majorDiatonicChords[3], progressions[0].Chords[1]); // IV
        Assert.Equal(_majorDiatonicChords[4], progressions[0].Chords[2]); // V

        // Check chords in second progression (I-V-vi-IV)
        Assert.Equal(4, progressions[1].Chords.Count);
        Assert.Equal(_majorDiatonicChords[0], progressions[1].Chords[0]); // I
        Assert.Equal(_majorDiatonicChords[4], progressions[1].Chords[1]); // V
        Assert.Equal(_majorDiatonicChords[5], progressions[1].Chords[2]); // vi
        Assert.Equal(_majorDiatonicChords[3], progressions[1].Chords[3]); // IV

        // Check chords in third progression (ii-V-I)
        Assert.Equal(3, progressions[2].Chords.Count);
        Assert.Equal(_majorDiatonicChords[1], progressions[2].Chords[0]); // ii
        Assert.Equal(_majorDiatonicChords[4], progressions[2].Chords[1]); // V
        Assert.Equal(_majorDiatonicChords[0], progressions[2].Chords[2]); // I
    }

    /// <summary>
    /// Tests that GetCommonProgressions returns the expected minor progressions.
    /// </summary>
    [Fact]
    public void GetCommonProgressions_MinorScale_ReturnsExpectedProgressions()
    {
        // Arrange
        _mockScaleService.Setup(s => s.GetDiatonicChords(_aMinorRoot, ScaleType.Minor))
            .Returns(_minorDiatonicChords);

        // Act
        var progressions = _service.GetCommonProgressions(_aMinorRoot, ScaleType.Minor).ToList();

        // Assert
        Assert.Equal(2, progressions.Count);

        // Check progression patterns
        Assert.Equal("i-iv-v", progressions[0].Pattern);
        Assert.Equal("i-VI-III-VII", progressions[1].Pattern);

        // Check chords in first progression (i-iv-v)
        Assert.Equal(3, progressions[0].Chords.Count);
        Assert.Equal(_minorDiatonicChords[0], progressions[0].Chords[0]); // i
        Assert.Equal(_minorDiatonicChords[3], progressions[0].Chords[1]); // iv
        Assert.Equal(_minorDiatonicChords[4], progressions[0].Chords[2]); // v

        // Check chords in second progression (i-VI-III-VII)
        Assert.Equal(4, progressions[1].Chords.Count);
        Assert.Equal(_minorDiatonicChords[0], progressions[1].Chords[0]); // i
        Assert.Equal(_minorDiatonicChords[5], progressions[1].Chords[1]); // VI
        Assert.Equal(_minorDiatonicChords[2], progressions[1].Chords[2]); // III
        Assert.Equal(_minorDiatonicChords[6], progressions[1].Chords[3]); // VII
    }

    /// <summary>
    /// Tests that GetCommonProgressions handles harmonic minor scales.
    /// </summary>
    [Fact]
    public void GetCommonProgressions_HarmonicMinorScale_HandledSameAsMinor()
    {
        // Arrange
        var harmonicMinorRoot = new Note(NoteName.E);
        _mockScaleService.Setup(s => s.GetDiatonicChords(harmonicMinorRoot, ScaleType.HarmonicMinor))
            .Returns(_minorDiatonicChords); // Reuse minor chords for simplicity in test

        // Act
        var progressions = _service.GetCommonProgressions(harmonicMinorRoot, ScaleType.HarmonicMinor).ToList();

        // Assert
        Assert.Equal(2, progressions.Count);
        Assert.Equal("i-iv-v", progressions[0].Pattern);
        Assert.Equal("i-VI-III-VII", progressions[1].Pattern);
    }
}
