using Entity;

namespace Models
{
  public class DetalleModel
  {
    public class DetalleInputModel
    {
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
        IdFactura = detalle.IdFactura;
      }
    }
  }
}