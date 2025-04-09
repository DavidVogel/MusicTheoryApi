using NSwag.Examples;
using MusicTheory.Domain;
using MusicTheory.Services;

namespace MusicTheory.Api.Controllers.Examples;

/// <summary>
/// Example provider for the ChordService class.
/// </summary>
public class ChordServiceExample : IExampleProvider<IEnumerable<Chord>>
{
    private readonly IServiceProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChordServiceExample"/> class.
    /// </summary>
    /// <param name="provider">Service provider for dependency injection</param>
    public ChordServiceExample(IServiceProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Provides an example of a list of Chord objects.
    /// </summary>
    /// <returns>A list of Chord objects</returns>
    public IEnumerable<Chord> GetExample()
    {
        using var scope = _provider.CreateScope();
        var scaleService = scope.ServiceProvider.GetRequiredService<IScaleService>();

        var sampleRoot = new Note(NoteName.C, Accidental.Sharp);
        var sampleScaleType = ScaleType.HarmonicMinor;

        return scaleService.GetDiatonicChords(sampleRoot, sampleScaleType);
    }
}
