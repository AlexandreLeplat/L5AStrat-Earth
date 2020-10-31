using System.Collections.Generic;

namespace HostApp.Models
{
    public class OrderInputModel
    {
        public int Rank { get; set; }
        public long ActionTypeId { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}
