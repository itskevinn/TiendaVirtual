using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Factura : Entity<int>
  {
    public Factura()
    {
      Detalles = new List<Detalle>();
    }
    public string Tipo { get; set; }
    [NotMapped]
    private List<Detalle> Detalles { get; set; }
    public decimal DescuentoTotal { get; set; }
    public decimal IvaTotal { get; set; }
    public decimal Total { get; set; }
    public decimal SubTotal { get; set; }
    public int IdInteresado { get; set; }
    public void AgregarDetalle(Detalle detalle)
    {
      Detalle Detalle = new Detalle
      {
        Tipo = this.Tipo == "venta" ? "resta" : "aumento",
        Cantidad = detalle.Cantidad,
        Descuento = detalle.Descuento,
        Id = detalle.Id,
        IdFactura = detalle.IdFactura,
        IdProducto = detalle.IdProducto,
        PrecioBase = detalle.PrecioBase,
        Total = detalle.Total,
      };
      Detalle.ColocarProducto(detalle.ObtenerProducto());
      Detalle.CalcularSubTotal();
      Detalle.CalcularDescontado();
      Detalle.CalcularValorIva();
      Detalle.CalcularTotal();
      Detalles.Add(Detalle);
    }
    public List<Detalle> ObtenerDetalles()
    {
      return Detalles;
    }
    public void CalcularTotales()
    {
      CalcularSubTotal();
      CalcularDescuentoTotal();
      CalcularIvaTotal();
      Total = Detalles.Sum((d) => d.Total);
    }
    public void CalcularDescuentoTotal()
    {
      DescuentoTotal = Detalles.Sum((d) => d.ValorDescontado);
    }
    public void CalcularIvaTotal()
    {
      IvaTotal = Detalles.Sum((d) => d.ValorIva);
    }
    public void CalcularSubTotal()
    {
      SubTotal = Detalles.Sum((d) => d.SubTotal);
    }
  }
}
