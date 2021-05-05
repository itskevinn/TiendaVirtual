using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Proveedor
  {
    [Key]
    public string Nit { get; set; }
    [Required(ErrorMessage = "Se requiere el nombre del proveedor")]
    public string Nombre { get; set; }
    [NotMapped]
    public List<Producto> Productos { get; set; }
  }
}