using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Rol : Entity<int>
  {
    public string Nombre { get; set; }

  }
}