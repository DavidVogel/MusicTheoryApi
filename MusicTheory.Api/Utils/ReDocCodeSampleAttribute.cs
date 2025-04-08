using NSwag.Annotations;

namespace MusicTheory.Utils;

/// <summary>
/// Attribute to specify a code sample for ReDoc documentation.
/// </summary>
public class ReDocCodeSampleAttribute : OpenApiOperationProcessorAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReDocCodeSampleAttribute"/> class.
    /// </summary>
    /// <param name="language">The programming language of the code sample.</param>
    /// <param name="source">The source code or file path of the code sample.</param>
    public ReDocCodeSampleAttribute(string language, string source)
        : base(typeof(ReDocCodeSampleAppender), language, source)
    {
    }
}
