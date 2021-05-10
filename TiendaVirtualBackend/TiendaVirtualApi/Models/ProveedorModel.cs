using System.Collections.Generic;
using Entity;

namespace Models
{
  public class ProveedorModel
  {
    public class ProveedorInputModel
    {
      public string TipoDocumento { get; set; }
      public string Documento { get; set; }
      public string Nombre { get; set; }
      public List<Producto> Productos { get; set; }
      public ProveedorInputModel()
      {
        Productos = new List<Producto>();
      }
    }
    public class ProveedorViewModel : ProveedorInputModel
    {
      public ProveedorViewModel(Proveedor proveedor)
      {
        Documento = proveedor.Documento;
        IdProveedor = proveedor.Id;
        TipoDocumento=proveedor.TipoDocumento;
        Nombre = proveedor.Nombre;
        Productos = proveedor.Productos;
      }
      public int IdProveedor { get; set; }
    }
  }
}
