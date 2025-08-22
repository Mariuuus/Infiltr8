using System.Text.Json.Serialization;

namespace server.Models
{
    public class JsonUser
    {
        [JsonRequired]
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonRequired]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }    
}
