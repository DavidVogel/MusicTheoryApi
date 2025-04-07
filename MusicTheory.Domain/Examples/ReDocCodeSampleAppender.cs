using Newtonsoft.Json;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace MusicTheory.Domain.Examples;

public class ReDocCodeSampleAppender : IOperationProcessor
{
    private readonly string _language;
    private readonly string _source;
    private const string ExtensionKey = "x-code-samples";

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

    public bool Process(OperationProcessorContext context)
    {
        if (context.OperationDescription.Operation.ExtensionData == null)
            context.OperationDescription.Operation.ExtensionData = new Dictionary<string, object>();

        var data = context.OperationDescription.Operation.ExtensionData;
        if (!data.ContainsKey(ExtensionKey))
            data[ExtensionKey] = new List<ReDocCodeSample>();

        var samples = (List<ReDocCodeSample>)data[ExtensionKey];
        samples.Add(new ReDocCodeSample
        {
            Language = _language,
            Source = _source,
        });

        return true;
    }
}

internal class ReDocCodeSample
{
    [JsonProperty("lang")]
    public string Language { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }
}
