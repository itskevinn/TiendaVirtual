using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Proveedor
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdProveedor { get; set; }
    [Required(ErrorMessage = "Se requiere el tipo de documento del proveedor")]
    public string TipoDocumento { get; set; } //NIT, CÉDULA, CÉDULA DE EXTRANJERÍA, PASAPORTE...
    [Required(ErrorMessage = "Se requiere el documento del proveedor")]
    public string Documento { get; set; }
    [Required(ErrorMessage = "Se requiere el nombre del proveedor")]
    public string Nombre { get; set; }
    [NotMapped]
    public List<Producto> Productos { get; set; }
  }
}