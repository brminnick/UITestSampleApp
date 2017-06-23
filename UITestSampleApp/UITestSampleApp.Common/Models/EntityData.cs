﻿using System;

using Microsoft.WindowsAzure.MobileServices;

using Newtonsoft.Json;

namespace UITestSampleApp.Common
{
    public class EntityData
    {
        public EntityData()
        {
            Id = Guid.NewGuid().ToString();
        }

		[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

		[CreatedAt]
		public DateTimeOffset CreatedAt { get; set; }

		[UpdatedAt]
		public DateTimeOffset UpdatedAt { get; set; }

		[Version]
		public string AzureVersion { get; set; }

		[Deleted]
		public bool IsDeleted { get; set; }
	}
}
