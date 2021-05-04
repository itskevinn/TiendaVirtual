using System.Collections.Generic;

namespace TiendaVirtualBackend.Entity
{
  public class Proveedor
  {
    public string Nit { get; set; }
    public string Nombre { get; set; }
    public List<Producto> Productos { get; set; }
  }
}