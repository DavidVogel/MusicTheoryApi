using System.ComponentModel.DataAnnotations;

namespace MusicTheory.Domain
{
    /// <summary>
    /// Represents a chord progression in a given key
    /// </summary>
    public class ChordProgression
    {
        /// <summary>
        /// The pattern of the chord progression, e.g., "I-IV-V" or "i-iv-v".
        /// </summary>
        [Required]
        public string Pattern { get; set; }         // e.g., "I-IV-V" or "i-iv-v"


        /// <summary>
        /// The chords in the progression, represented as a list of Chord objects
        /// </summary>
        [Required]
        public List<Chord> Chords { get; set; }     // The actual chords in the progression

        /// <summary>
        /// Constructor to create a chord progression with a given pattern and list of chords
        /// </summary>
        /// <param name="pattern">The pattern of the chord progression</param>
        /// <param name="chords">The list of chords in the progression</param>
        public ChordProgression(string pattern, List<Chord> chords)
        {
            Pattern = pattern;
            Chords = chords;
        }
    }
}
