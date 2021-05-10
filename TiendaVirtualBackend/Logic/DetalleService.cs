using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;

namespace Logic
{
  public class DetalleService
  {
    private TiendaVirtualContext context;
    private ProductoService productoService;
    public DetalleService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      productoService = new ProductoService(tiendaVirtualContext);
    }
    public GuardarDetalleResponse Guardar(Detalle detalle)
    {
      try
      {
        Producto productoBuscado = productoService.ConsultarPorId(detalle.IdProducto);
        if (productoBuscado == null)
        {
          return new GuardarDetalleResponse("No se encontró el producto", true);
        }
        if (detalle.Tipo.ToLower() == "aumento")
        {
          if (productoService.AumentarCantidad(productoBuscado, detalle.Cantidad).Error)
          {
            var mensajeModificacion = productoService.AumentarCantidad(productoBuscado, detalle.Cantidad).Mensaje;
            return new GuardarDetalleResponse(detalle, mensajeModificacion, true);
          };
        }
        if (detalle.Tipo.ToLower() == "resta")
        {
          if (productoService.ReducirCantidad(productoBuscado, detalle.Cantidad).Error)
          {
            var mensajeModificacion = productoService.ReducirCantidad(productoBuscado, detalle.Cantidad).Mensaje;
            return new GuardarDetalleResponse(detalle, mensajeModificacion, true);
          };
        }
        detalle.CalcularTotal();
        context.Detalles.Add(detalle);
        context.SaveChanges();
        return new GuardarDetalleResponse(detalle, "Detalle guardado con éxito", false);

      }
      catch (Exception e)
      {
        return new GuardarDetalleResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
      }
    }
    public List<Detalle> Consultar()
    {
      return context.Detalles.ToList();
    }
    public Detalle Consultar(string id)
    {
      return context.Detalles.Find(id);
    }
    public List<Detalle> ConsultarPorProducto(string id)
    {
      if (context.Productos.Where((p) => p.Codigo == id).Count() == 1)
      {
        return context.Detalles.Where((d) => d.IdProducto == id).ToList();
      }
      return null;
    }
    public List<Detalle> ConsultarPorFactura(int id)
    {
      return context.Detalles.Where((d) => d.IdFactura == id).ToList();
    }
    public class GuardarDetalleResponse
    {
      public Detalle Detalle { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarDetalleResponse(Detalle detalle, string mensaje, bool error)
      {
        Detalle = detalle;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarDetalleResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}