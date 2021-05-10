using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;

namespace Logic
{
  public class RolService
  {
    private readonly TiendaVirtualContext context;
    private FacturaService facturaService;
    public RolService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      facturaService = new FacturaService(tiendaVirtualContext);
    }
    public GuardarRolResponse Guardar(Rol rol)
    {
      try
      {
        Rol rolBuscado = context.Roles.Find(rol.IdRol);
        if (rolBuscado == null)
        {
          context.Roles.Add(rol);
          context.SaveChanges();
          return new GuardarRolResponse(rol, "Rol guardado con éxito", false);
        }
        return new GuardarRolResponse("Rol duplicado, por favor, rectifique la información", true);
      }
      catch (System.Exception)
      {
        return new GuardarRolResponse("Ha ocurrido un error en el servidor. Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Rol> Consultar()
    {
      List<Rol> roles = context.Roles.ToList();
      return roles;
    }
    public Rol Consultar(int id)
    {
      Rol rol = context.Roles.Find(id);
      return rol;
    }
    public Rol ValidarRol(int idRol)
    {
      if (context.Roles.Find(idRol) != null)
      {
        return context.Roles.Find(idRol);
      }
      return null;
    }
    public EditarRolResponse Editar(string id, Rol rolActualizado)
    {
      try
      {
        var rolAActualizar = context.Roles.Find(id);
        if (rolAActualizar != null)
        {
          rolAActualizar.Nombre = rolActualizado.Nombre;
          context.Roles.Update(rolAActualizar);
          context.SaveChanges();
          return new EditarRolResponse(rolAActualizar, "Rol editado correctamente", false);
        }
        else
        {
          return new EditarRolResponse("Rol no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarRolResponse($"Ocurrió un error al editar el rol {e.Message}", true);
      }
    }
    public class EditarRolResponse
    {
      public Rol Rol { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarRolResponse(Rol rol, string mensaje, bool error)
      {
        Rol = rol;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarRolResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarRolResponse
    {
      public Rol Rol { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarRolResponse(Rol rol, string mensaje, bool error)
      {
        Rol = rol;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarRolResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}