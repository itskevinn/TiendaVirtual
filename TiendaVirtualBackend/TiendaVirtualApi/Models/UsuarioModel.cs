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
        IdUsuario = usuario.IdUsuario;
        _Usuario = usuario._Usuario;
        Facturas = usuario.Facturas;
        Rol = usuario.Rol;
      }
    }
  }
}
