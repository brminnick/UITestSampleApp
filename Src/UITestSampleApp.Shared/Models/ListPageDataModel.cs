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

        public ListPageDataModel(int detail, string text) : this(detail, text, Guid.NewGuid().ToString())
        {

        }

        [JsonConstructor]
        public ListPageDataModel(int detail, string text, string id) =>
            (Detail, Text, Id) = (detail, text, id);

        public string Id { get; }
        public int Detail { get; }
        public string Text { get; }
    }
}
