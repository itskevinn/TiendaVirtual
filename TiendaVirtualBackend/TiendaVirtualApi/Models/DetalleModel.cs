using Entity;

namespace Models
{
  public class DetalleModel
  {
    public class DetalleInputModel : Detalle
    {
    }

    public class DetalleViewModel : DetalleInputModel
    {
      public DetalleViewModel(Detalle producto)
      {
        IdDetalle = producto.IdDetalle;
        Cantidad = producto.Cantidad;
        IdProducto = producto.IdProducto;
      }
    }
  }
}