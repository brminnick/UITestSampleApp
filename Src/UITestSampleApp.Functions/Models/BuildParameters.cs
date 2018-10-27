using Newtonsoft.Json;

namespace UITestSampleApp.Functions
{
    public partial class BuildParameters
    {
        public BuildParameters(bool isDebug) => IsDebug = isDebug;

        [JsonProperty("debug")]
        public bool IsDebug { get; } = true;
    }
}
