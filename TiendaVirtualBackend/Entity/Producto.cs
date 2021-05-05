using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Producto
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string IdObjeto { get; set; }
    [Required(ErrorMessage = "Se requiere el id del producto")]
    public string Id { get; set; }
    [Required(ErrorMessage = "Se requiere el nombre del producto")]
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    [Required(ErrorMessage = "Se requiere el nombre el precio del producto")]
    public decimal PrecioBase { get; set; }
    [Required(ErrorMessage = "Se requiere el IVA del producto")]
    public decimal Iva { get; set; }
    public decimal Descuento { get; set; }
    [Required(ErrorMessage = "Se requiere el nit del proveedor")]
    public string NitProveedor { get; set; }
    [Required(ErrorMessage = "Se requiere la cantidad disponible del producto")]
    public int CantidadDisponible { get; set; }

  }
}