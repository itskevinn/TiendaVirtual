using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Detalle
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string IdDetalle { get; set; }
    [Required(ErrorMessage = "Se requiere la cantidad del producto")]
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
    public decimal ValorDescontado { get; set; }
    public decimal ValorConDescuento { get; set; }
    public decimal ValorIva { get; set; }
    public decimal SubTotal { get; set; }
    [NotMapped]
    public Producto Producto { get; set; }
    [Required(ErrorMessage = "Se requiere el id del producto a facturar")]
    public string IdProducto { get; set; }

    public void CalcularSubTotal()
    {
      SubTotal = Producto.PrecioBase * Cantidad;
    }
    public void CalcularDescontado()
    {
      ValorDescontado = SubTotal * (Producto.Descuento / 100);
    }
    public void CalcularValorConDescuento()
    {
      ValorConDescuento = SubTotal - ValorDescontado;
    }
    public void CalcularValorIva()
    {
      ValorIva = ValorConDescuento * (Producto.Iva / 100);
    }
    public void CalcularTotal()
    {
      CalcularSubTotal();
      CalcularDescontado();
      CalcularValorConDescuento();
      CalcularValorIva();
      Total = SubTotal - ValorDescontado + ValorIva;
    }

  }
}