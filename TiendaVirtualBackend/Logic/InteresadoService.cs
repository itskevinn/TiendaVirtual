using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Logic
{
  public class InteresadoService
  {
    private TiendaVirtualContext context;
    private readonly FacturaService facturaService;
    public InteresadoService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      facturaService = new FacturaService(tiendaVirtualContext);
    }
    public GuardarInteresadoResponse Guardar(Interesado interesado)
    {
      try
      {
        Interesado interesadoBuscado = context.Interesados.Where((u) => u.IdInteresado == interesado.IdInteresado).FirstOrDefault();
        if (interesadoBuscado == null)
        {
          interesado.Facturas = facturaService.ConsultarPorInteresado(interesado.IdInteresado);
          context.Interesados.Add(interesado);
          context.SaveChanges();
          return new GuardarInteresadoResponse(interesado, "Interesado guardado con éxito", false);
        }
        return new GuardarInteresadoResponse("Interesado duplicado, por favor, rectifique la información", true);
      }
      catch (System.Exception)
      {
        return new GuardarInteresadoResponse("Ha ocurrido un error en el servidor. Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Interesado> Consultar()
    {
      List<Interesado> interesados = context.Interesados.ToList();
      interesados.ForEach((u) => u.Facturas = facturaService.ConsultarPorInteresado(u.IdInteresado));
      return interesados;
    }
    public Interesado Consultar(string id)
    {
      Interesado interesado = context.Interesados.Where((i) => i.IdInteresado == id).FirstOrDefault();
      interesado.Facturas = facturaService.ConsultarPorInteresado(interesado.IdInteresado);
      return interesado;
    }
    public EditarInteresadoResponse Editar(string id, Interesado interesadoActualizado)
    {
      try
      {
        var interesadoAActualizar = context.Interesados.Where((i) => i.IdInteresado == id).FirstOrDefault();
        if (interesadoAActualizar != null)
        {
          interesadoAActualizar.IdInteresado = interesadoActualizado.IdInteresado;
          interesadoAActualizar.Facturas = interesadoActualizado.Facturas;
          interesadoAActualizar.Usuario = interesadoActualizado.Usuario;
          interesadoAActualizar.IdUsuario = interesadoAActualizar.IdUsuario;
          context.Interesados.Update(interesadoAActualizar);
          context.SaveChanges();
          return new EditarInteresadoResponse(interesadoAActualizar, "Interesado editado correctamente", false);
        }
        else
        {
          return new EditarInteresadoResponse("Interesado no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarInteresadoResponse($"Ocurrió un error al editar el interesado {e.Message}", true);
      }
    }
    public EliminarInteresadoResponse Eliminar(string id)
    {
      try
      {
        var interesadoAEliminar = context.Interesados.Where((i) => i.IdInteresado == id).FirstOrDefault();
        if (interesadoAEliminar != null)
        {
          context.Interesados.Remove(interesadoAEliminar);
          context.SaveChanges();
          return new EliminarInteresadoResponse(interesadoAEliminar, "Interesado eliminado correctamente");
        }
        return new EliminarInteresadoResponse("No se encontró el interesado");
      }
      catch (Exception e)
      {
        return new EliminarInteresadoResponse("Ocurrió un error al eliminar el interesado " + e.Message);
      }
    }
    public class EliminarInteresadoResponse
    {
      public Interesado Interesado { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EliminarInteresadoResponse(Interesado interesado, string mensaje)
      {
        Mensaje = mensaje;
        Interesado = interesado;
        Error = false;
      }
      public EliminarInteresadoResponse(string mensaje)
      {
        Mensaje = mensaje;
        Error = true;
      }
    }
    public class EditarInteresadoResponse
    {
      public Interesado Interesado { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarInteresadoResponse(Interesado interesado, string mensaje, bool error)
      {
        Interesado = interesado;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarInteresadoResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarInteresadoResponse
    {
      public Interesado Interesado { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarInteresadoResponse(Interesado interesado, string mensaje, bool error)
      {
        Interesado = interesado;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarInteresadoResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}