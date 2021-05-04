using System;
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
    public DbSet<Producto> Producto { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }

  }
}
