using System.Text.Json.Serialization;

namespace MagicalProduct.API.Payload.Response
{
    public class RoleResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}