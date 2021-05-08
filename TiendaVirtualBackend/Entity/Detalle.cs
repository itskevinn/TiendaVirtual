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
    public string Tipo { get; set; }
    public decimal Total { get; set; }
    public decimal Descuento { get; set; }
    public decimal ValorDescontado { get; set; }
    public decimal ValorIva { get; set; }
    public decimal PrecioBase { get; set; }
    public decimal SubTotal { get; set; }
    [NotMapped]
    public Producto Producto { get; set; }
    [Required(ErrorMessage = "Se requiere el id del producto a facturar")]
    public string IdProducto { get; set; }
    public int IdFactura { get; set; }

    public void CalcularSubTotal()
    {
      if (PrecioBase != 0)
      {
        SubTotal = PrecioBase * Cantidad;
        return;
      }
      SubTotal = Producto.PrecioBase * Cantidad;
    }
    public void CalcularDescontado()
    {
      if (Descuento != 0 && PrecioBase != 0)
      {
        ValorDescontado = PrecioBase * (Descuento / 100);
        ValorDescontado = ValorDescontado * Cantidad;
        return;
      }
      ValorDescontado = Producto.PrecioBase * (Producto.Descuento / 100);
      ValorDescontado = ValorDescontado * Cantidad;
    }
    public void CalcularValorIva()
    {
      if (PrecioBase != 0)
      {
        ValorIva = PrecioBase * (Producto.Iva / 100);
        ValorIva = ValorIva * Cantidad;
        return;
      }
      ValorIva = Producto.PrecioBase * (Producto.Iva / 100);
      ValorIva = ValorIva * Cantidad;
    }
    public void CalcularTotal()
    {
      CalcularSubTotal();
      CalcularDescontado();
      CalcularValorIva();
      Total = SubTotal - ValorDescontado + ValorIva;
    }

  }
}