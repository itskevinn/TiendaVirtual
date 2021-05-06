using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Logic
{
  public class FacturaService
  {
    private TiendaVirtualContext context;
    private readonly DetalleService detalleService;
    private readonly ProductoService productoService;
    public FacturaService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      detalleService = new DetalleService(tiendaVirtualContext);
      productoService = new ProductoService(tiendaVirtualContext);
    }
    public GuardarFacturaResponse Guardar(Factura factura)
    {
      try
      {
        Factura facturaBuscada = context.Facturas.Find(factura.IdFactura);
        Usuario usuario = context.Usuarios.Find(factura.IdUsuario);
        if (facturaBuscada == null)
        {
          if (usuario == null)
          {
            return new GuardarFacturaResponse("Usuario no encontrado", true);
          }
          foreach (Detalle detalle in factura.Detalles)
          {
            if (detalleService.Guardar(detalle).Error)
            {
              return new GuardarFacturaResponse(detalleService.Guardar(detalle).Mensaje, true);
            }
            detalleService.Guardar(detalle);
          }
          factura.CalcularTotales();
          context.Facturas.Add(factura);
          context.SaveChanges();
          return new GuardarFacturaResponse(factura, "Factura guardada con éxito", false);
        }
        return new GuardarFacturaResponse("Factura duplicada, por favor, rectifique la información", true);
      }
      catch (System.Exception e)
      {
        return new GuardarFacturaResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Factura> Consultar()
    {
      List<Factura> facturas = context.Facturas.ToList();
      facturas.ForEach((f) => f.Detalles = detalleService.ConsultarPorFactura(f.IdFactura));
      return facturas;
    }
    public List<Factura> ConsultarPorUsuario(string idUsuario)
    {
      return context.Facturas.Where((f) => f.IdUsuario == idUsuario).ToList();
    }
    public Factura Consultar(string id)
    {
      return context.Facturas.Find(id);
    }
    public EditarFacturaResponse Editar(string id, Factura facturaActualizado)
    {
      try
      {
        var facturaAActualizar = context.Facturas.Find(id);
        if (facturaAActualizar != null)
        {
          facturaAActualizar.Detalles = facturaActualizado.Detalles;
          facturaAActualizar.CalcularTotales();
          context.Facturas.Update(facturaAActualizar);
          context.SaveChanges();
          return new EditarFacturaResponse(facturaAActualizar, "Factura editada correctamente", false);
        }
        else
        {
          return new EditarFacturaResponse("Factura no encontrada", true);
        }
      }
      catch (Exception e)
      {
        return new EditarFacturaResponse($"Ocurrió un error al editar la factura {e.Message}", true);
      }
    }
    public EliminarFacturaResponse Eliminar(string id)
    {
      try
      {
        var facturaAEliminar = context.Facturas.Find(id);
        if (facturaAEliminar != null)
        {
          context.Facturas.Remove(facturaAEliminar);
          context.SaveChanges();
          return new EliminarFacturaResponse(facturaAEliminar, "Factura eliminada correctamente");
        }
        return new EliminarFacturaResponse("No se encontró la factura");
      }
      catch (Exception e)
      {
        return new EliminarFacturaResponse("Ocurrió un error al eliminar la factura" + e.Message);
      }
    }
    public class EliminarFacturaResponse
    {
      public Factura Factura { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EliminarFacturaResponse(Factura factura, string mensaje)
      {
        Mensaje = mensaje;
        Factura = factura;
        Error = false;
      }
      public EliminarFacturaResponse(string mensaje)
      {
        Mensaje = mensaje;
        Error = true;
      }
    }
    public class EditarFacturaResponse
    {
      public Factura Factura { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarFacturaResponse(Factura factura, string mensaje, bool error)
      {
        Factura = factura;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarFacturaResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarFacturaResponse
    {
      public Factura Factura { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarFacturaResponse(Factura factura, string mensaje, bool error)
      {
        Factura = factura;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarFacturaResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}