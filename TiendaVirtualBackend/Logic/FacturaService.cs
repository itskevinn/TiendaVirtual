using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;

namespace Logic
{
  public class FacturaService
  {
    private TiendaVirtualContext context;
    public FacturaService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
    }
    public GuardarFacturaResponse Guardar(Factura detalle)
    {
      try
      {
        Factura detalleBuscado = context.Facturas.Find(detalle.Id);
        if (detalleBuscado != null)
        {
          context.Facturas.Add(detalle);
          context.SaveChanges();
          return new GuardarFacturaResponse(detalle, "Factura guardado con éxito", false);
        }
        return new GuardarFacturaResponse("Factura duplicada, por favor, rectifique la información", true);
      }
      catch (System.Exception)
      {
        return new GuardarFacturaResponse("Ha ocurrido un error en el servidor. Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Factura> Consultar()
    {
      return context.Facturas.ToList();
    }
    public EditarFacturaResponse Editar(string id, Factura detalleActualizado)
    {
      try
      {
        var detalleAActualizar = context.Facturas.Find(id);
        if (detalleAActualizar != null)
        {
          detalleAActualizar.Detalles = detalleActualizado.Detalles;
          detalleAActualizar.Iva = detalleActualizado.Iva;
          detalleAActualizar.CalcularSubTotal();
          detalleAActualizar.CalcularTotal();
          context.Facturas.Update(detalleAActualizar);
          context.SaveChanges();
          return new EditarFacturaResponse(detalleAActualizar, "Factura editada correctamente", false);
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
        var detalleAEliminar = context.Facturas.Find(id);
        if (detalleAEliminar != null)
        {
          context.Facturas.Remove(detalleAEliminar);
          context.SaveChanges();
          return new EliminarFacturaResponse(detalleAEliminar, "Factura eliminada correctamente");
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
      public Factura Detalle { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EliminarFacturaResponse(Factura detalle, string mensaje)
      {
        Mensaje = mensaje;
        Detalle = detalle;
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
      public EditarFacturaResponse(Factura detalle, string mensaje, bool error)
      {
        Factura = detalle;
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