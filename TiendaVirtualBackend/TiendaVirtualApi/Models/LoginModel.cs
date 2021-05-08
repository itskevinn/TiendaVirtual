using System.ComponentModel.DataAnnotations;
using Entity;

namespace Models
{
  public class LoginModel
  {
    public class LoginInputModel
    {
      [Required(ErrorMessage = "Se requiere el usuario")]
      public string Usuario { get; set; }
      [Required(ErrorMessage = "Se requiere la contraseña")]
      public string Contrasena { get; set; }
    }
    public class LoginViewModel : LoginInputModel
    {
      public LoginViewModel(Usuario usuario)
      {
        Usuario = usuario._Usuario;
        Contrasena = usuario.Contrasena;
        Rol = usuario.Rol;
        IdUsuario = usuario.IdUsuario;
      }
      public string IdUsuario { get; set; }
      public string Rol { get; set; }
    }
  }
}