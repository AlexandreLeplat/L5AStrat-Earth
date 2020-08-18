using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Message
    {
        public long Id { get; set; }

        // Objet du message
        public string Subject { get; set; }

        // Contenu du message
        public string Body { get; set; }

        // Flag indiquant si le message est une notification du système de jeu
        public bool IsNotification { get; set; }

        // Flag indiquant si le message est lu
        public bool IsRead { get; set; }

        // Flag indiquant si le message est archivé
        public bool IsArchived { get; set; }

        // Date d'envoi du message
        public DateTime? SendDate { get; set; }

        // Joueur auquel le message est adressé
        public long PlayerId { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }

        // Expéditeur du message
        public long SenderId { get; set; }
        [JsonIgnore]
        public Player Sender { get; set; }
    }
}
