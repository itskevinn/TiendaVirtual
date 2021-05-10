using System.Collections.Generic;
using Entity;

namespace Models
{
  public class InteresadoModel
  {
    public class InteresadoInputModel
    {
      public Usuario Usuario { get; set; }
    }

    public class InteresadoViewModel : InteresadoInputModel
    {
      public InteresadoViewModel(Interesado interesado)
      {
        IdInteresado = interesado.IdInteresado;
        Facturas = interesado.Facturas;
        Usuario = interesado.Usuario;
      }
      public List<Factura> Facturas { get; set; }
      public int IdInteresado { get; set; }
    }
  }
}