using Entities.Enums;

namespace Entities.Models
{
    public class OrderInput
    {
        public string Label { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Parameter { get; set; }
    }
}
