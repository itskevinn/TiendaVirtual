using System.Collections.Generic;

namespace Entity
{
  public class Factura
  {
    public string Id { get; set; }
    public List<Detalle> Detalles { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DescuentoTotal { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }
    public void CalcularTotal()
    {
      CalcularSubTotal();
      Total = SubTotal - DescuentoTotal;
      Total = SubTotal + CalcularIva();
    }
    public void CalcularDescuentoTotal()
    {
      foreach (Detalle detalle in Detalles)
      {
        DescuentoTotal += detalle.TotalDescontado;
      }
    }
    private decimal CalcularIva()
    {
      return Total * (Iva / 100);
    }
    public void CalcularSubTotal()
    {
      foreach (Detalle detalle in Detalles)
        SubTotal += detalle.Total;
    }
  }
}