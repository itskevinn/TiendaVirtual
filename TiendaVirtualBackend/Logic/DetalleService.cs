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
    public DetalleService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
    }
    public GuardarDetalleResponse Guardar(Detalle detalle)
    {
      try
      {
        Detalle detalleBuscado = context.Detalles.Find(detalle.Id);
        if (detalleBuscado != null)
        {
          context.Detalles.Add(detalle);
          context.SaveChanges();
          return new GuardarDetalleResponse(detalle, "Detalle guardado con éxito", false);
        }
        return new GuardarDetalleResponse("Detalle duplicado, por favor, rectifique la información", true);
      }
      catch (System.Exception)
      {
        return new GuardarDetalleResponse("Ha ocurrido un error en el servidor. Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Detalle> Consultar()
    {
      return context.Detalles.ToList();
    }
    public EditarDetalleResponse Editar(string id, Detalle detalleActualizado)
    {
      try
      {
        var detalleAActualizar = context.Detalles.Find(id);
        if (detalleAActualizar != null)
        {
          detalleAActualizar.Cantidad = detalleActualizado.Cantidad;
          detalleAActualizar.Producto = detalleActualizado.Producto;
          detalleAActualizar.CalcularTotal();
          context.Detalles.Update(detalleAActualizar);
          context.SaveChanges();
          return new EditarDetalleResponse(detalleAActualizar, "Detalle editado correctamente", false);
        }
        else
        {
          return new EditarDetalleResponse("Detalle no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarDetalleResponse($"Ocurrió un error al editar el detalle {e.Message}", true);
      }
    }
    public EliminarDetalleResponse Eliminar(string id)
    {
      try
      {
        var detalleAEliminar = context.Detalles.Find(id);
        if (detalleAEliminar != null)
        {
          context.Detalles.Remove(detalleAEliminar);
          context.SaveChanges();
          return new EliminarDetalleResponse(detalleAEliminar, "Detalle eliminado correctamente");
        }
        return new EliminarDetalleResponse("No se encontró el detalle");
      }
      catch (Exception e)
      {
        return new EliminarDetalleResponse("Ocurrió un error al eliminar el detalle " + e.Message);
      }
    }
    public class EliminarDetalleResponse
    {
      public Detalle Detalle { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EliminarDetalleResponse(Detalle detalle, string mensaje)
      {
        Mensaje = mensaje;
        Detalle = detalle;
        Error = false;
      }
      public EliminarDetalleResponse(string mensaje)
      {
        Mensaje = mensaje;
        Error = true;
      }
    }
    public class EditarDetalleResponse
    {
      public Detalle Detalle { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarDetalleResponse(Detalle detalle, string mensaje, bool error)
      {
        Detalle = detalle;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarDetalleResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
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