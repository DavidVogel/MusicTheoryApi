using Newtonsoft.Json;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace MusicTheory.Utils;

/// <summary>
/// Appends a code sample to the operation description for ReDoc documentation.
/// </summary>
public class ReDocCodeSampleAppender : IOperationProcessor
{
    private readonly string _language;
    private readonly string _source;
    private const string ExtensionKey = "x-code-samples";

    /// <summary>
    /// Initializes a new instance of the <see cref="ReDocCodeSampleAppender"/> class.
    /// </summary>
    /// <param name="language">The programming language of the code sample.</param>
    /// <param name="source">The source code or file path of the code sample.</param>
    public ReDocCodeSampleAppender(string language, string source)
    {
        _language = language;
        // If the source starts with a file prefix, load its contents at runtime.
        if (source.StartsWith("file://"))
        {
            var filePath = source.Substring("file://".Length);
            _source = File.ReadAllText(filePath);
        }
        else
        {
            _source = source;
        }
    }

    /// <summary>
    /// Processes the operation description to append a code sample.
    /// </summary>
    /// <param name="context">The context of the operation being processed.</param>
    /// <returns><c>true</c> if the operation was processed successfully; otherwise, <c>false</c>.</returns>
    public bool Process(OperationProcessorContext context)
    {
        if (context.OperationDescription.Operation.ExtensionData == null)
            context.OperationDescription.Operation.ExtensionData = new Dictionary<string, object?>();

        var data = context.OperationDescription.Operation.ExtensionData;
        if (!data.ContainsKey(ExtensionKey))
            data[ExtensionKey] = new List<ReDocCodeSample>();

        var samples = (List<ReDocCodeSample>?)data[ExtensionKey];
        samples?.Add(new ReDocCodeSample
        {
            Language = _language,
            Source = _source,
        });

        return true;
    }
}

internal class ReDocCodeSample
{
    /// <summary>
    /// The programming language of the code sample.
    /// </summary>
    [JsonProperty("lang")]
    public string Language { get; set; } = "cURL";

    /// <summary>
    /// The source code or file path of the code sample.
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; } = string.Empty;
}
