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
      public decimal PrecioBase { get; set; }
      public int CantidadDisponible { get; set; }
      public decimal Iva { get; set; }
      public decimal Descuento { get; set; }
      public string NitProveedor { get; set; }

    }

    public class ProductoViewModel : ProductoInputModel
    {
      public ProductoViewModel(Producto producto)
      {
        Id = producto.Codigo;
        Nombre = producto.Nombre;
        Descripcion = producto.Descripcion;
        PrecioBase = producto.PrecioBase;
        CantidadDisponible = producto.CantidadDisponible;
        NitProveedor = producto.DocumentoProveedor;
        Iva = producto.Iva;
        Descuento = producto.Descuento;
      }
    }
  }
}