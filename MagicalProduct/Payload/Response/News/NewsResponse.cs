using System.Text.Json.Serialization;

namespace MagicalProduct.API.Payload.Response
{
    public class NewsResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}