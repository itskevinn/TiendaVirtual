using Entity;

namespace Models
{
  public class ProductoModel
  {
    public class ProductoInputModel
    {
      public string Id { get; set; }
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public decimal Precio { get; set; }
      public int Cantidad { get; set; }
      public decimal Iva { get; set; }
      public decimal Descuento { get; set; }
      public string NitProveedor { get; set; }

    }

    public class ProductoViewModel : ProductoInputModel
    {
      public ProductoViewModel(Producto producto)
      {
        Id = producto.Id;
        Nombre = producto.Nombre;
        Descripcion = producto.Descripcion;
        Precio = producto.PrecioBase;
        Cantidad = producto.CantidadDisponible;
        NitProveedor = producto.NitProveedor;
        Iva = producto.Iva;
        Descuento = producto.Descuento;
      }
    }
  }
}