using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
  public class Usuario
  {
    [Key]
    public string IdUsuario { get; set; }
    [Required(ErrorMessage = "Ingrese un usuario")]
    public string _Usuario { get; set; }
    [Required(ErrorMessage = "Ingrese una contraseña")]
    public string Contrasena { get; set; }
    [Required(ErrorMessage = "Seleccione un rol")]
    public string Rol { get; set; }
  }
}
