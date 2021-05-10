using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
namespace Logic
{
  public class LiderAvaluoService
  {

    private readonly TiendaVirtualContext context;
    private FacturaService facturaService;
    private UsuarioService usuarioService;
    public LiderAvaluoService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      usuarioService = new UsuarioService(tiendaVirtualContext);
      facturaService = new FacturaService(tiendaVirtualContext);
    }
    public GuardarLiderAvaluoResponse Guardar(LiderAvaluo lider)
    {
      try
      {
        var usuario = usuarioService.Consultar(lider.IdUsuario);
        if (usuario != null)
        {
          lider.Usuario = usuario;
          lider.IdUsuario = usuario.IdUsuario;
        }
        else
        {
          var response = usuarioService.Guardar(lider.Usuario);
          if (response.Error)
          {
            return new GuardarLiderAvaluoResponse(usuarioService.Guardar(lider.Usuario).Mensaje, true);
          }
          usuarioService.Guardar(lider.Usuario);
          lider.IdUsuario = response.Usuario.IdUsuario;
        }
        context.LiderAvaluos.Add(lider);
        context.SaveChanges();
        return new GuardarLiderAvaluoResponse(lider, "Lider de avalúos guardado con éxito", false);
      }
      catch (System.Exception e)
      {
        return new GuardarLiderAvaluoResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<LiderAvaluo> Consultar()
    {
      List<LiderAvaluo> lideres = context.LiderAvaluos.ToList();
      lideres.ForEach((l) => l.Usuario = usuarioService.Consultar(l.IdUsuario));
      return lideres;
    }
    public LiderAvaluo Consultar(int id)
    {
      LiderAvaluo lider = context.LiderAvaluos.Find(id);
      lider.IdUsuario = usuarioService.Consultar(lider.IdUsuario).IdUsuario;
      return lider;
    }
    public EditarLiderAvaluoResponse Editar(string id, LiderAvaluo liderActualizado)
    {
      try
      {
        var liderAActualizar = context.LiderAvaluos.Find(id);
        if (liderAActualizar != null)
        {
          liderAActualizar.Usuario = liderActualizado.Usuario;
          context.LiderAvaluos.Update(liderAActualizar);
          context.SaveChanges();
          return new EditarLiderAvaluoResponse(liderAActualizar, "Lider editado correctamente", false);
        }
        else
        {
          return new EditarLiderAvaluoResponse("Lider no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarLiderAvaluoResponse($"Ocurrió un error al editar el lider {e.Message}", true);
      }
    }
    public class EditarLiderAvaluoResponse
    {
      public LiderAvaluo LiderAvaluo { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarLiderAvaluoResponse(LiderAvaluo lider, string mensaje, bool error)
      {
        LiderAvaluo = lider;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarLiderAvaluoResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarLiderAvaluoResponse
    {
      public LiderAvaluo LiderAvaluo { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarLiderAvaluoResponse(LiderAvaluo lider, string mensaje, bool error)
      {
        LiderAvaluo = lider;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarLiderAvaluoResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}