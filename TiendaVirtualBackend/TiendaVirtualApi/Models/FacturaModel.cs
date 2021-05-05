using Entity;

namespace Models
{
  public class FacturaModel
  {
    public class FacturaInputModel : Factura
    {
    }

    public class FacturaViewModel : FacturaInputModel
    {
      public FacturaViewModel(Factura factura)
      {
        IdFactura = factura.IdFactura;
        Detalles = factura.Detalles;
      }
    }
  }
}