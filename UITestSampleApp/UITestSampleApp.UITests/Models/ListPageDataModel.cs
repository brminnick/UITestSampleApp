﻿using System;

namespace UITestSampleApp.UITests
{
    [Serializable]
    public class ListPageDataModel
    {
		public string Id { get; set; }
        public string DetailProperty { get; set; }
        public string TextProperty { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string AzureVersion { get; set; }
        public bool IsDeleted { get; set; }
    }
}
