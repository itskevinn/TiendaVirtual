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
      public string IdInteresado { get; set; }
      public string Tipo { get; set; }
    }

    public class FacturaViewModel : FacturaInputModel
    {
      public FacturaViewModel(Factura factura)
      {
        IdFactura = factura.IdFactura;
        Detalles = factura.ObtenerDetalles();
        IdInteresado = factura.IdInteresado;
        Total = factura.Total;
        IvaTotal = factura.IvaTotal;
        DescuentoTotal = factura.DescuentoTotal;
        SubTotal = factura.SubTotal;
        Tipo = factura.Tipo;
      }
      public decimal SubTotal { get; set; }
      public decimal Total { get; set; }
      public decimal IvaTotal { get; set; }
      public decimal DescuentoTotal { get; set; }
    }
  }
}