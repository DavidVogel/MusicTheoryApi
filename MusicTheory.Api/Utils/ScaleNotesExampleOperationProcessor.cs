// File: MusicTheory.Api/Utils/ScaleNotesExampleOperationProcessor.cs
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System.Collections.Generic;

namespace MusicTheory.Api.Utils
{
    public class ScaleNotesExampleOperationProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            if (context.MethodInfo.Name == "GetScaleNotes")
            {
                if (context.OperationDescription.Operation.Responses.TryGetValue("200", out var response))
                {
                    response.Examples = new List<string>
                    {
                        "Db",
                        "Eb",
                        "Fb",
                        "Gb",
                        "Ab",
                        "Bbb",
                        "C",
                        "Db"
                    };
                }
            }
            return true;
        }
    }
}
