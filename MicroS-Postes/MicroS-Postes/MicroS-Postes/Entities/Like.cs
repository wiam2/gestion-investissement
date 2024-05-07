using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MicroS_Postes.Entities
{
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLike { get; set; }
        public string IdLOwner { get; set; }
        public int IdPoste { get; set; }
    }
}
