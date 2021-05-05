using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
  public class Factura
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int IdFactura { get; set; }
    [NotMapped]
    public List<Detalle> Detalles { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DescuentoTotal { get; set; }
    public decimal IvaTotal { get; set; }
    public decimal Total { get; set; }
    public void CalcularTotales()
    {
      CalcularDescuentoTotal();
      CalcularIvaTotal();
      Total = Detalles.Sum((d) => d.Total);
    }
    public void CalcularDescuentoTotal()
    {
      DescuentoTotal = Detalles.Sum((d) => d.ValorDescontado);
    }
    public void CalcularIvaTotal()
    {
      IvaTotal = Detalles.Sum((d) => d.ValorIva);
    }
  }
}
