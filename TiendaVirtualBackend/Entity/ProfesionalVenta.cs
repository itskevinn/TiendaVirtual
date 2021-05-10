using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class ProfesionalVenta
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdProfesionalVenta { get; set; }
    public int IdUsuario { get; set; }
    [NotMapped]
    public Usuario Usuario { get; set; }
    [NotMapped]
    private List<Factura> Facturas { get; set; }
    public List<Factura> ObtenerFacturas()
    {
      return Facturas;
    }
  }
}