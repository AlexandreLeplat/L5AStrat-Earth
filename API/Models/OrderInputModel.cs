using System.Collections.Generic;

namespace API.Models
{
    public class OrderInputModel
    {
        public long Id { get; set; }
        public int Rank { get; set; }
        public long ActionTypeId { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
