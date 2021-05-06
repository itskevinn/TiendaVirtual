using System.Collections.Generic;
using Entity;

namespace Models
{
  public class ProveedorModel
  {
    public class ProveedorInputModel
    {
      public string Nit { get; set; }
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
        Nit = proveedor.Nit;
        Nombre = proveedor.Nombre;
        Productos = proveedor.Productos;
      }
    }
  }
}
