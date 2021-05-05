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
    public FacturaService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      detalleService = new DetalleService(tiendaVirtualContext);
    }
    public GuardarFacturaResponse Guardar(Factura factura)
    {
      try
      {
        Factura facturaBuscada = context.Facturas.Find(factura.IdFactura);
        if (facturaBuscada == null)
        {
          foreach (Detalle detalle in factura.Detalles)
          {
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
      return context.Facturas.Include((f) => f.Detalles).ToList();
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