using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Factura
  {
    public Factura()
    {
      Detalles = new List<Detalle>();
      Detalle = new Detalle();
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]

    public int IdFactura { get; set; }
    public string Tipo { get; set; }
    [NotMapped]
    private List<Detalle> Detalles { get; set; }
    public decimal DescuentoTotal { get; set; }
    private Detalle Detalle { get; set; }
    public decimal IvaTotal { get; set; }
    public decimal Total { get; set; }
    public decimal SubTotal { get; set; }
    public string IdInteresado { get; set; }
    public void AgregarDetalle(Detalle detalle)
    {
      Detalle = new Detalle
      {
        Tipo = this.Tipo == "venta" ? "resta" : "aumento",
        Cantidad = detalle.Cantidad,
        Descuento = detalle.Descuento,
        IdDetalle = detalle.IdDetalle,
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
