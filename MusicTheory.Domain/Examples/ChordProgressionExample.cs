using NSwag.Examples;

namespace MusicTheory.Domain.Examples;

/// <summary>
/// Example provider for the ChordProgression class
/// </summary>
public class ChordProgressionExample : IExampleProvider<ChordProgression>
{
    /// <summary>
    /// Provides an example of a ChordProgression object (C major I-IV-V-I progression)
    /// </summary>
    /// <returns>A ChordProgression object representing a common progression in C major</returns>
    public ChordProgression GetExample()
    {
        // Create a C major key
        var cNote = new Note(NoteName.C, Accidental.Natural);

        // Create the four chords in the progression (I-IV-V-I)
        var cMajor = new Chord(cNote, ChordType.Major);                                // I chord
        var fMajor = new Chord(new Note(NoteName.F, Accidental.Natural), ChordType.Major);  // IV chord
        var gMajor = new Chord(new Note(NoteName.G, Accidental.Natural), ChordType.Major);  // V chord

        // Return a new chord progression with these chords
        return new ChordProgression("C Major I-IV-V-I", new List<Chord> { cMajor, fMajor, gMajor, cMajor });
    }
}

public class ChordProgressionsExample : IExampleProvider<IEnumerable<ChordProgression>>
{
    public IEnumerable<ChordProgression> GetExample()
    {
        // Use the existing example provider to generate one chord progression.
        var progressionExample = new ChordProgressionExample();
        var progression1 = progressionExample.GetExample();

        // Create a second example progression with different chords.
        var cNote = new Note(NoteName.C, Accidental.Natural);
        var fMajor = new Chord(new Note(NoteName.F, Accidental.Natural), ChordType.Major);
        var gMajor = new Chord(new Note(NoteName.G, Accidental.Natural), ChordType.Major);
        var progression2 = new ChordProgression("C Major I-IV-V progression", new List<Chord> {
            new Chord(cNote, ChordType.Major),
            fMajor,
            gMajor
        });

        return new List<ChordProgression> { progression1, progression2 };
    }
}
