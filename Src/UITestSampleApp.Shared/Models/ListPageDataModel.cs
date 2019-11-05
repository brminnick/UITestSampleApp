using System;
using Newtonsoft.Json;

namespace UITestSampleApp.Shared
{
    public class ListPageDataModel
    {
        public ListPageDataModel(int detail, string text) : this(detail,text,Guid.NewGuid().ToString())
        {

        }

        [JsonConstructor]
        public ListPageDataModel(int detail, string text, string id) =>
            (Detail, Text, Id) = (detail, text, id);

        [JsonIgnore]
        public const string CollectionId = "uitestsampleapp-collection";

        [JsonIgnore]
        public const string DatabaseId = "uitestsampleapp-database";

        public string Id { get; }
        public int Detail { get; }
        public string Text { get; }
    }
}
