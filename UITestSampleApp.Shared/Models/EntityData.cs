using System;

#if NETSTANDARD1_4
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
#endif

namespace UITestSampleApp.Shared
{
    [Serializable]
    public class EntityData
    {
        public EntityData()
        {
            Id = Guid.NewGuid().ToString();
        }
#if NETSTANDARD1_4
		[JsonProperty(PropertyName = "id")]
#endif
        public string Id { get; set; }
#if NETSTANDARD1_4
		[CreatedAt]
#endif
		public DateTimeOffset CreatedAt { get; set; }
#if NETSTANDARD1_4
		[UpdatedAt]
#endif
		public DateTimeOffset UpdatedAt { get; set; }
#if NETSTANDARD1_4
		[Version]
#endif
		public string AzureVersion { get; set; }
#if NETSTANDARD1_4
		[Deleted]
#endif
		public bool IsDeleted { get; set; }
	}
}
