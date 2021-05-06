using System.Collections.Generic;
using Entity;

namespace Models
{
  public class FacturaModel
  {
    public class FacturaInputModel
    {
      public int IdFactura { get; set; }
      public List<Detalle> Detalles { get; set; }
      public string IdUsuario { get; set; }
    }

    public class FacturaViewModel : FacturaInputModel
    {
      public FacturaViewModel(Factura factura)
      {
        IdFactura = factura.IdFactura;
        Detalles = factura.Detalles;
        IdUsuario = factura.IdUsuario;
      }
    }
  }
}