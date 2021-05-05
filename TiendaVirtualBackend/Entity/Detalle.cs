namespace Entity
{
  public class Detalle
  {
    public string Id { get; set; }
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
    public decimal TotalDescontado { get; set; }
    public Producto Producto { get; set; }
    public void CalcularTotal()
    {
      Total = Producto.Precio * Cantidad;
    }
    public void CalcularTotalDescontado()
    {
      TotalDescontado = Producto.Descontado * Cantidad;
    }
  }
}