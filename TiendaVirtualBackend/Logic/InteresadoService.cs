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
    private FacturaService facturaService;
    private UsuarioService usuarioService;
    public InteresadoService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      facturaService = new FacturaService(tiendaVirtualContext);
      usuarioService = new UsuarioService(tiendaVirtualContext);
    }
    public GuardarInteresadoResponse Guardar(Interesado interesado)
    {
      try
      {
        var usuario = usuarioService.Consultar(interesado.IdUsuario);
        if (usuario != null)
        {
          interesado.Usuario = usuario;
          interesado.IdUsuario = usuario.IdUsuario;
        }
        else
        {
          var response = usuarioService.Guardar(interesado.Usuario);
          if (response.Error)
          {
            return new GuardarInteresadoResponse(usuarioService.Guardar(interesado.Usuario).Mensaje, true);
          }
          usuarioService.Guardar(interesado.Usuario);
          interesado.IdUsuario = response.Usuario.IdUsuario;
        }
        context.Interesados.Add(interesado);
        context.SaveChanges();
        return new GuardarInteresadoResponse(interesado, "Interesado guardado con éxito", false);
      }
      catch (System.Exception e)
      {
        return new GuardarInteresadoResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Interesado> Consultar()
    {
      List<Interesado> interesados = context.Interesados.ToList();
      interesados.ForEach((u) => u.Facturas = facturaService.ConsultarPorInteresado(u.IdInteresado));
      return interesados;
    }
    public Interesado Consultar(int id)
    {
      Interesado interesado = context.Interesados.Where((i) => i.IdInteresado == id).FirstOrDefault();
      interesado.Facturas = facturaService.ConsultarPorInteresado(interesado.IdInteresado);
      return interesado;
    }
    public EditarInteresadoResponse Editar(int id, Interesado interesadoActualizado)
    {
      try
      {
        var interesadoAActualizar = context.Interesados.Where((i) => i.IdInteresado == id).FirstOrDefault();
        if (interesadoAActualizar != null)
        {
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