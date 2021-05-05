namespace Entity
{
  public class Producto
  {
    public string IdObjeto { get; set; }
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public decimal Descontado { get; set; }
    public decimal Descuento { get; set; }
    public string NitProveedor { get; set; }
    public void CalcularPrecioConDescuento()
    {
      CalcularDescontado();
      Precio = Precio - Descontado;
    }
    public void CalcularDescontado()
    {
      Descontado = Precio * (Descuento / 100);
    }
  }
}