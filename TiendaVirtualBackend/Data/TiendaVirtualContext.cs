using System;
using System.Linq;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data
{
  public class TiendaVirtualContext : DbContext
  {
    public TiendaVirtualContext(DbContextOptions options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Detalle> Detalles { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<Interesado> Interesados { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var tipoEntidades in modelBuilder.Model.GetEntityTypes()
      .SelectMany(x => x.GetProperties()).Where(x => x.ClrType == typeof(decimal) || x.ClrType == typeof(decimal?)))
      {
        tipoEntidades.SetPrecision(18);
        tipoEntidades.SetScale(6);
      }
    }
  }
}
