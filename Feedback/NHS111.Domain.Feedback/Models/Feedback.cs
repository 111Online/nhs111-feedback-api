using System;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;

namespace NHS111.Domain.Feedback.Models
{
    public class Feedback : TableEntity
    {
        public Feedback()
        {
            var now = DateTime.UtcNow;
            PartitionKey = string.Format("{0:yyyy-MM}", now);
            RowKey = string.Format("{0:dd HH-mm-ss-fff}-{1}", now, Guid.NewGuid());
        }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "jSonData")]
        public string JSonData { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "dateAdded")]
        public DateTime DateAdded { get; set; }

        [JsonProperty(PropertyName = "emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "pageId")]
        public string PageId { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public int? Rating { get; set; }
    }
}
