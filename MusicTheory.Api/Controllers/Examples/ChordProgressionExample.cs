using NSwag.Examples;
using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Api.Controllers.Examples;

/// <summary>
/// Example provider for the ChordProgression class.
/// </summary>
public class ChordProgressionsExample : IExampleProvider<IEnumerable<ChordProgression>>
{
    private readonly IServiceProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChordProgressionsExample"/> class.
    /// </summary>
    /// <param name="provider">Service provider for dependency injection</param>
    public ChordProgressionsExample(IServiceProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Provides an example of a list of ChordProgression objects.
    /// </summary>
    /// <returns></returns>
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
