using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class LiderAvaluo : Entity<int>
  {
    public int IdUsuario { get; set; }
    [NotMapped]
    public Usuario Usuario { get; set; }
  }
}