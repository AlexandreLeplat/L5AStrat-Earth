using Newtonsoft.Json;

namespace API.Models
{
    [JsonObject]
    public class TokenInputModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
