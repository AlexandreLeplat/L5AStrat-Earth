using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Order
    {
        public long Id { get; set; }

        // Paramètres de l'action (clef = label de l'orderInput, valeur = valeur du paramètre en string)
        [NotMapped]
        public Dictionary<string, string> Parameters
        {
            get { return _jsonParameters == null ? null : JsonSerializer.Deserialize<Dictionary<string, string>>(_jsonParameters); }
            set { _jsonParameters = JsonSerializer.Serialize(value); }
        }
        [JsonIgnore]
        public string _jsonParameters { get; set; }

        public OrderStatus Status { get; set; }
        public string Comment { get; set; }

        // Type d'action
        public long ActionTypeId { get; set; }
        [JsonIgnore]
        public ActionType ActionType { get; set; }

        // Feuille d'ordre à laquelle l'ordre appartient
        public long OrdersSheetId { get; set; }
        [JsonIgnore]
        public OrdersSheet OrdersSheet { get; set; }
    }
}
