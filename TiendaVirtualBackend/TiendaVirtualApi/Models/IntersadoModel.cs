using System.Collections.Generic;
using Entity;

namespace Models
{
  public class InteresadoModel
  {
    public class InteresadoInputModel
    {
      public string IdInteresado { get; set; }
      public Usuario Usuario { get; set; }
      public string IdUsuario { get; set; }
      public List<Factura> Facturas { get; set; }
    }

    public class InteresadoViewModel : InteresadoInputModel
    {
      public InteresadoViewModel(Interesado interesado)
      {
        IdInteresado = interesado.IdInteresado;
        Facturas = interesado.Facturas;
        IdUsuario = interesado.IdInteresado;
        Usuario = interesado.Usuario;
      }
    }
  }
}