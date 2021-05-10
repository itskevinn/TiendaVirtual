using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class LiderAvaluo
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdLiderAvaluo { get; set; }
    public int IdUsuario { get; set; }
    [NotMapped]
    public Usuario Usuario { get; set; }
  }
}