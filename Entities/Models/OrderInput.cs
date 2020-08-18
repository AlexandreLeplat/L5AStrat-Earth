using Entities.Enums;

namespace Entities.Models
{
    public class OrderInput
    {
        public string Label { get; set; }
        public OrderInputType Type { get; set; }
        public string Description { get; set; }
    }
}
