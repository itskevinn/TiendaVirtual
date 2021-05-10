using Entity;

namespace Models
{
  public class UsuarioModel
  {
    public class UsuarioInputModel
    {
      public int IdRol { get; set; }
      public string Contrasena { get; set; }
      public string NombreUsuario { get; set; }
    }
    public class UsuarioViewModel : UsuarioInputModel
    {
      public UsuarioViewModel(Usuario usuario)
      {
        IdUsuario = usuario.IdUsuario;
        NombreUsuario = usuario.NombreUsuario;
        Rol = usuario.Rol;
      }
      public int IdUsuario { get; set; }
      public Rol Rol { get; set; }
    }
  }
}
