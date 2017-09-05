using System;

#if MOBILE
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
#endif

namespace UITestSampleApp.Shared
{
    public class EntityData
    {
        public EntityData() => Id = Guid.NewGuid().ToString();

#if MOBILE
        [JsonProperty(PropertyName = "id")]
#endif  
        public string Id { get; set; }

#if MOBILE
        [CreatedAt]
#endif
        public DateTimeOffset CreatedAt { get; set; }

#if MOBILE
		[UpdatedAt]
#endif
        public DateTimeOffset UpdatedAt { get; set; }

#if MOBILE
		[Version]
#endif
        public string AzureVersion { get; set; }

#if MOBILE
        [Deleted]
#endif
        public bool IsDeleted { get; set; }
    }
}
