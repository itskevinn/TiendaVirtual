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
    public string IdUsuario { get; set; }
    [Required(ErrorMessage = "Ingrese un usuario")]
    public string _Usuario { get; set; }
    [Required(ErrorMessage = "Ingrese una contraseña")]
    public string Contrasena { get; set; }
    [Required(ErrorMessage = "Seleccione un rol")]
    public string Rol { get; set; }
    [NotMapped]
    public List<Factura> Facturas { get; set; }

  }
}
