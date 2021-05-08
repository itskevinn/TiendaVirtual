using Entity;

namespace Models
{
  public class DetalleModel
  {
    public class DetalleInputModel
    {
      public decimal PrecioBase { get; set; }
      public decimal Descuento { get; set; }
      public int Cantidad { get; set; }
      public string IdProducto { get; set; }
      public int IdFactura { get; set; }
    }

    public class DetalleViewModel : DetalleInputModel
    {
      public DetalleViewModel(Detalle detalle)
      {
        Cantidad = detalle.Cantidad;
        IdProducto = detalle.IdProducto;
        /*IdFactura = detalle.IdFactura;*/
        PrecioBase = detalle.PrecioBase;
        Descuento = detalle.Descuento;
        SubTotal = detalle.SubTotal;
        ValorDescontado = detalle.ValorDescontado;
        ValorIva = detalle.ValorIva;
        Total = detalle.Total;
      }
      public decimal Total { get; set; }
      public decimal ValorIva { get; set; }
      public decimal SubTotal { get; set; }
      public decimal ValorDescontado { get; set; }
    }
  }
}