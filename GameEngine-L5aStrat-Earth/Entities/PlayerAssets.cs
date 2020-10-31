using System.Text.Json.Serialization;

namespace L5aStrat_Earth.Entities
{
    public class PlayerAssets
    {
        [JsonPropertyName("Caractéristiques")]
        public PlayerAttributes Attributes { get; set; }

        [JsonPropertyName("Ressources")]
        public PlayerResources Resources { get; set; }
    }

    public class PlayerAttributes
    {
        [JsonPropertyName("Gloire")]
        public string _jsonGlory { get; set; }
        [JsonIgnore]
        public int Glory
        {
            get { return _jsonGlory == null ? 0 : int.Parse(_jsonGlory); }
            set { _jsonGlory = value.ToString(); }
        }

        [JsonPropertyName("Infamie")]
        public string _jsonInfamy { get; set; }
        [JsonIgnore]
        public int Infamy
        {
            get { return _jsonInfamy == null ? 0 : int.Parse(_jsonInfamy); }
            set { _jsonInfamy = value.ToString(); }
        }
    }

    public class PlayerResources
    {
        [JsonPropertyName("Stratégie")]
        public string _jsonStrategy { get; set; }
        [JsonIgnore]
        public int Strategy
        {
            get { return _jsonStrategy == null ? 0 : int.Parse(_jsonStrategy); }
            set { _jsonStrategy = value.ToString(); }
        }

        [JsonPropertyName("Influence")]
        public string _jsonInfluence { get; set; }
        [JsonIgnore]
        public int Influence
        {
            get { return _jsonInfluence == null ? 0 : int.Parse(_jsonInfluence); }
            set { _jsonInfluence = value.ToString(); }
        }
    }
}
