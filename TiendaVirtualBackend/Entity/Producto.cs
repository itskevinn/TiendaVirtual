using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Producto : Entity<int>
  {
    [Required(ErrorMessage = "Se requiere el código del producto")]
    public string Codigo { get; set; }
    [Required(ErrorMessage = "Se requiere el nombre del producto")]
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    [Required(ErrorMessage = "Se requiere el nombre el precio del producto")]
    public decimal PrecioBase { get; set; }
    [Required(ErrorMessage = "Se requiere el IVA del producto")]
    public decimal Iva { get; set; }
    public decimal Descuento { get; set; }
    [Required(ErrorMessage = "Se requiere el documento del proveedor")]
    public int idProveedor { get; set; }
    [Required(ErrorMessage = "Se requiere la cantidad disponible del producto")]
    public int CantidadDisponible { get; set; }

  }
}