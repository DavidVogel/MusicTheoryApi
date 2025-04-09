using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace MusicTheory.Api.Utils;

/// <summary>
/// Processes the operation description to provide example scale notes.
/// </summary>
public class ScaleNotesExampleOperationProcessor : IOperationProcessor
{
    /// <summary>
    /// Processes the operation description to provide example scale notes.
    /// </summary>
    /// <param name="context">The context of the operation being processed.</param>
    /// <returns><c>true</c> if the operation was processed successfully; otherwise, <c>false</c>.</returns>
    public bool Process(OperationProcessorContext context)
    {
        if (context.MethodInfo.Name == "GetScaleNotes")
        {
            if (context.OperationDescription.Operation.Responses.TryGetValue("200", out var response))
            {
                // G-Sharp Harmonic Minor Scale
                response.Examples = new List<string>
                {
                    "G#",
                    "A#",
                    "B",
                    "C#",
                    "D#",
                    "E",
                    "F##",
                    "G#"
                };
            }
        }

        return true;
    }
}
