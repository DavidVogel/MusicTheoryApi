using NSwag.Examples;
using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Api.Controllers.Examples
{
    public class ChordServiceExample : IExampleProvider<IEnumerable<Chord>>
    {
        private readonly IServiceProvider _provider;

        public ChordServiceExample(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<Chord> GetExample()
        {
            using var scope = _provider.CreateScope();
            var scaleService = scope.ServiceProvider.GetRequiredService<IScaleService>();

            // Use sample values for demonstration, for example note C Sharp and Harmonic Minor scale.
            var sampleRoot = new Note(NoteName.C, Accidental.Sharp);
            var sampleScaleType = ScaleType.HarmonicMinor;

            return scaleService.GetDiatonicChords(sampleRoot, sampleScaleType);
        }
    }
}
