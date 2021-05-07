using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Interesado
  {
    [Key]
    public string Id { get; set; }
    public string IdInteresado { get; set; }
    [NotMapped]
    public Usuario Usuario { get; set; }
    public string IdUsuario { get; set; }
    public List<Factura> Facturas { get; set; }
  }
}