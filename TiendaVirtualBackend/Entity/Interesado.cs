using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Interesado : Entity<int>
  {
    [NotMapped]
    public Usuario Usuario { get; set; }
    public int IdUsuario { get; set; }
    public List<Factura> Facturas { get; set; }
  }
}