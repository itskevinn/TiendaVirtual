using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Persona
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdPersona { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
  }
}