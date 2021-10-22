using System.Text.Json.Serialization;

namespace UITestSampleApp.Functions
{
    public class BuildParameters
    {
        public BuildParameters(bool isDebug) => IsDebug = isDebug;

        [JsonPropertyName("debug")]
        public bool IsDebug { get; } = true;
    }
}
