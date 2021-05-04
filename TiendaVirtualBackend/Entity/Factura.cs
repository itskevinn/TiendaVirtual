using System.Collections.Generic;

namespace Entity
{
  public class Factura
  {
    public string Id { get; set; }
    public List<Detalle> Detalles { get; set; }
    public decimal SubTotal
    {
      get; set;
    }
    public decimal Descuento { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }
    public void CalcularTotal()
    {
      Total = SubTotal - Descuento;
      Total = SubTotal + CalcularIva();
    }
    private decimal CalcularIva()
    {
      return Total * Iva;
    }
    public void CalcularSubTotal()
    {
      foreach (Detalle detalle in Detalles)
        SubTotal += detalle.Total;
    }
  }
}