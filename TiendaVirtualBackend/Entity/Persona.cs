using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Persona : Entity<int>
  {
    public string Nombre { get; set; }
    public string Apellido { get; set; }
  }
}