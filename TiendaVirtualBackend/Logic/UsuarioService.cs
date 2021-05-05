using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;

namespace Logic
{
  public class UsuarioService
  {
    private TiendaVirtualContext context;
    public UsuarioService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
    }
    public GuardarUsuarioResponse Guardar(Usuario usuario)
    {
      try
      {
        Usuario usuarioBuscado = context.Usuarios.Find(usuario.IdUsuario);
        if (usuarioBuscado == null)
        {
          context.Usuarios.Add(usuario);
          context.SaveChanges();
          return new GuardarUsuarioResponse(usuario, "Usuario guardado con éxito", false);
        }
        return new GuardarUsuarioResponse("Usuario duplicado, por favor, rectifique la información", true);
      }
      catch (System.Exception)
      {
        return new GuardarUsuarioResponse("Ha ocurrido un error en el servidor. Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Usuario> Consultar()
    {
      return context.Usuarios.ToList();
    }
    public Usuario Consultar(string id)
    {
      return context.Usuarios.Find(id);
    }
    public EditarUsuarioResponse Editar(string id, Usuario usuarioActualizado)
    {
      try
      {
        var usuarioAActualizar = context.Usuarios.Find(id);
        if (usuarioAActualizar != null)
        {
          usuarioAActualizar._Usuario = usuarioActualizado._Usuario;
          usuarioAActualizar.Contrasena = usuarioActualizado.Contrasena;
          usuarioAActualizar.Rol = usuarioActualizado.Rol;
          context.Usuarios.Update(usuarioAActualizar);
          context.SaveChanges();
          return new EditarUsuarioResponse(usuarioAActualizar, "Usuario editado correctamente", false);
        }
        else
        {
          return new EditarUsuarioResponse("Usuario no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarUsuarioResponse($"Ocurrió un error al editar el usuario {e.Message}", true);
      }
    }
    public EliminarUsuarioResponse Eliminar(string id)
    {
      try
      {
        var usuarioAEliminar = context.Usuarios.Find(id);
        if (usuarioAEliminar != null)
        {
          context.Usuarios.Remove(usuarioAEliminar);
          context.SaveChanges();
          return new EliminarUsuarioResponse(usuarioAEliminar, "Usuario eliminado correctamente");
        }
        return new EliminarUsuarioResponse("No se encontró el usuario");
      }
      catch (Exception e)
      {
        return new EliminarUsuarioResponse("Ocurrió un error al eliminar el usuario " + e.Message);
      }
    }
    public class EliminarUsuarioResponse
    {
      public Usuario Usuario { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EliminarUsuarioResponse(Usuario usuario, string mensaje)
      {
        Mensaje = mensaje;
        Usuario = usuario;
        Error = false;
      }
      public EliminarUsuarioResponse(string mensaje)
      {
        Mensaje = mensaje;
        Error = true;
      }
    }
    public class EditarUsuarioResponse
    {
      public Usuario Usuario { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarUsuarioResponse(Usuario usuario, string mensaje, bool error)
      {
        Usuario = usuario;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarUsuarioResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarUsuarioResponse
    {
      public Usuario Usuario { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarUsuarioResponse(Usuario usuario, string mensaje, bool error)
      {
        Usuario = usuario;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarUsuarioResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}