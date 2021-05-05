using Entity;

namespace Models
{
  public class ProveedorModel
  {
    public class ProveedorInputModel : Proveedor
    {

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
