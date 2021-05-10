using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Usuario
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int IdUsuario { get; set; }
    [Required(ErrorMessage = "Ingrese un usuario")]
    public string NombreUsuario { get; set; }
    [Required(ErrorMessage = "Ingrese una contraseña")]
    public string Contrasena { get; set; }
    public int IdRol { get; set; }
    [Required(ErrorMessage = "Seleccione un rol")]
    [NotMapped]
    public Rol Rol { get; set; }
    public int IdPersona { get; set; }
    [NotMapped]
    public Persona Persona { get; set; }
  }
}
