using System.Text.Json.Serialization;

namespace MagicalProduct.API.Payload.Response
{
    public class UserResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("gender")]
        public bool? Gender { get; set; }

        [JsonPropertyName("status")]
        public bool? Status { get; set; }

        [JsonPropertyName("role-id")]
        public int? RoleId { get; set; }

        [JsonPropertyName("role-name")]
        public string RoleName { get; set; }
    }
}