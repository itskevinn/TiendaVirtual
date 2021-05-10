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
        Usuario = usuario.NombreUsuario;
        Contrasena = usuario.Contrasena;
        Rol = usuario.Rol;
        IdUsuario = usuario.IdUsuario;
        Persona = usuario.Persona;
        IdPersona = usuario.IdPersona;
      }
      public int IdPersona { get; set; }
      public Persona Persona { get; set; }
      public int IdUsuario { get; set; }
      public Rol Rol { get; set; }
    }
  }
}