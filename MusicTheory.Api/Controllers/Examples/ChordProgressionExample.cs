using NSwag.Examples;
using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Api.Controllers.Examples
{
    public class ChordProgressionsExample : IExampleProvider<IEnumerable<ChordProgression>>
    {
        private readonly IServiceProvider _provider;

        public ChordProgressionsExample(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<ChordProgression> GetExample()
        {
            using (var scope = _provider.CreateScope())
            {
                var progressionService = scope.ServiceProvider.GetRequiredService<IProgressionService>();
                var key = new Note(NoteName.C, Accidental.Natural);
                return progressionService.GetCommonProgressions(key, ScaleType.Major);
            }
        }
    }
}
