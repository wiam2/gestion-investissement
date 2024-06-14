using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceMessagerie.Models
{
    public class Conversation
    {
      
        [Key]
        public string? idconversation { get; set; }
        [NotMapped]
        public string? Emeteur { get; set; }

        [NotMapped]
        public string? Recepteur { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();
       

    }
}
