using NSwag.Annotations;

namespace MusicTheory.Domain.Examples;

public class ReDocCodeSampleAttribute : OpenApiOperationProcessorAttribute
{
    public ReDocCodeSampleAttribute(string language, string source)
        : base(typeof(ReDocCodeSampleAppender), language, source)
    {
    }
}
