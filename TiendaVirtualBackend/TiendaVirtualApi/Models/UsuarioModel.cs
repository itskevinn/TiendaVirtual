using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;

namespace Models
{
  public class UsuarioModel
  {
    public class UsuarioInputModel : Usuario
    {

    }
    public class UsuarioViewModel : UsuarioInputModel
    {
      public UsuarioViewModel(Usuario usuario)
      {
        Id = usuario.Id;
        _Usuario = usuario._Usuario;
        Rol = usuario.Rol;
      }
    }
  }
}
