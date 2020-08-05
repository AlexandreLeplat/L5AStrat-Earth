using Entities.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        [JsonIgnore]
        public List<Player> Players { get; set; }
    }
}
