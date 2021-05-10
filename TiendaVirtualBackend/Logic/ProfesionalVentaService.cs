using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;

namespace Logic
{
  public class ProfesionalVentaService
  {
    private readonly TiendaVirtualContext context;
    private UsuarioService usuarioService;
    private FacturaService facturaService;
    public ProfesionalVentaService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      facturaService = new FacturaService(tiendaVirtualContext);
      usuarioService = new UsuarioService(tiendaVirtualContext);
    }
    public GuardarProfesionalVentaResponse Guardar(ProfesionalVenta profesional)
    {
      try
      {
        var usuario = usuarioService.Consultar(profesional.IdUsuario);
        if (usuario != null)
        {
          profesional.Usuario = usuario;
          profesional.IdUsuario = usuario.IdUsuario;
        }
        else
        {
          var response = usuarioService.Guardar(profesional.Usuario);
          if (response.Error)
          {
            return new GuardarProfesionalVentaResponse(usuarioService.Guardar(profesional.Usuario).Mensaje, true);
          }
          usuarioService.Guardar(profesional.Usuario);
          profesional.IdUsuario = response.Usuario.IdUsuario;
        }
        context.ProfesionalVentas.Add(profesional);
        context.SaveChanges();
        return new GuardarProfesionalVentaResponse(profesional, "Profesional de Ventas guardado con éxito", false);
      }
      catch (System.Exception e)
      {
        return new GuardarProfesionalVentaResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
      }
    }

    public List<ProfesionalVenta> Consultar()
    {
      List<ProfesionalVenta> profesionales = context.ProfesionalVentas.ToList();
      profesionales.ForEach((p) => p.Usuario = usuarioService.Consultar(p.IdUsuario));
      return profesionales;
    }
    public ProfesionalVenta Consultar(int id)
    {
      ProfesionalVenta profesional = context.ProfesionalVentas.Find(id);
      profesional.IdUsuario = usuarioService.Consultar(profesional.IdUsuario).IdUsuario;
      return profesional;
    }
    public EditarProfesionalVentaResponse Editar(string id, ProfesionalVenta profesionalActualizado)
    {
      try
      {
        var profesionalAActualizar = context.ProfesionalVentas.Find(id);
        if (profesionalAActualizar != null)
        {
          profesionalAActualizar.Usuario = profesionalActualizado.Usuario;
          context.ProfesionalVentas.Update(profesionalAActualizar);
          context.SaveChanges();
          return new EditarProfesionalVentaResponse(profesionalAActualizar, "Profesional  editado correctamente", false);
        }
        else
        {
          return new EditarProfesionalVentaResponse("Profesional de Ventas no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarProfesionalVentaResponse($"Ocurrió un error al editar el profesional {e.Message}", true);
      }
    }
    public class EditarProfesionalVentaResponse
    {
      public ProfesionalVenta ProfesionalVenta { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarProfesionalVentaResponse(ProfesionalVenta profesional, string mensaje, bool error)
      {
        ProfesionalVenta = profesional;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarProfesionalVentaResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarProfesionalVentaResponse
    {
      public ProfesionalVenta ProfesionalVenta { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarProfesionalVentaResponse(ProfesionalVenta profesional, string mensaje, bool error)
      {
        ProfesionalVenta = profesional;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarProfesionalVentaResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}