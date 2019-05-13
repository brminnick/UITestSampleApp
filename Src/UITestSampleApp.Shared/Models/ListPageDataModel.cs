using System;
using Newtonsoft.Json;

namespace UITestSampleApp.Shared
{
    public class ListPageDataModel
    {
        [JsonIgnore]
        public const string CollectionId = "uitestsampleapp-collection";

        [JsonIgnore]
        public const string DatabaseId = "uitestsampleapp-database";

        [JsonProperty("id")]
        public string Id { get; } = Guid.NewGuid().ToString();

        public int Detail { get; set; }
        public string Text { get; set; }
    }
}
