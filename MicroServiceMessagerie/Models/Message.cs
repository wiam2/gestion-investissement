using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceMessagerie.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Emeteur { get; set; }
        public string Recepteur { get; set; }
        public string Contenu { get; set; }
        public DateTime Date { get; set; }
        public Conversation conversation { get; set; }
    }
}
