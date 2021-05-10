using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Infraestructura;
using Microsoft.EntityFrameworkCore;
namespace Logic
{
  public class UsuarioService
  {
    private readonly TiendaVirtualContext context;
    private RolService rolService;
    private PersonaService personaService;
    private FacturaService facturaService;
    public UsuarioService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      rolService = new RolService(tiendaVirtualContext);
      facturaService = new FacturaService(tiendaVirtualContext);
      personaService = new PersonaService(tiendaVirtualContext);
    }
    public GuardarUsuarioResponse Guardar(Usuario usuario)
    {
      try
      {
        Usuario usuarioBuscado = context.Usuarios.Where((u) => u.NombreUsuario == usuario.NombreUsuario).FirstOrDefault();
        if (usuarioBuscado == null)
        {
          var idPersona = usuario.IdPersona;
          var idRol = usuario.IdRol;
          if (rolService.ValidarRol(idRol) != null)
          {
            if (personaService.Consultar(usuario.Persona.Id) == null)
            {
              var personaRegistrada = personaService.Guardar(usuario.Persona).Persona;
              usuario.IdPersona = personaRegistrada.Id;
            }
            else
            {
              usuario.Persona = personaService.Consultar(idPersona);
              usuario.IdPersona = usuario.Persona.Id;
              if (usuario.Persona == null)
              {
                return new GuardarUsuarioResponse("Persona no registrada", true);
              }
            }
            usuario.Contrasena = Hash.GetSha256(usuario.Contrasena);
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            return new GuardarUsuarioResponse(usuario, "Usuario guardado con éxito", false);
          }
          return new GuardarUsuarioResponse("Rol inexistente, por favor, rectifique la información", true);
        }
        return new GuardarUsuarioResponse("Usuario duplicado, por favor, rectifique la información", true);
      }
      catch (System.Exception e)
      {
        return new GuardarUsuarioResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Usuario> Consultar()
    {
      List<Usuario> usuarios = context.Usuarios.ToList();
      usuarios.ForEach((u) => u.Rol = rolService.Consultar(u.IdRol));
      usuarios.ForEach((u) => u.Persona = personaService.Consultar(u.IdPersona));
      usuarios.ForEach((u) => u.IdRol = u.Rol.Id);
      return usuarios;
    }
    public Usuario Consultar(int id)
    {
      Usuario usuario = context.Usuarios.Find(id);
      if (usuario != null)
      {
        usuario.Rol = rolService.Consultar(usuario.IdRol);
        usuario.Persona = personaService.Consultar(usuario.IdPersona);
        usuario.IdRol = usuario.Rol.Id;
      }
      return usuario;
    }
    public EditarUsuarioResponse Editar(int id, Usuario usuarioActualizado)
    {
      try
      {
        var usuarioAActualizar = context.Usuarios.Find(id);
        if (usuarioAActualizar != null)
        {
          usuarioAActualizar.NombreUsuario = usuarioActualizado.NombreUsuario;
          usuarioAActualizar.Contrasena = usuarioActualizado.Contrasena;
          usuarioAActualizar.Rol = usuarioActualizado.Rol;
          usuarioAActualizar.IdRol = usuarioAActualizar.IdRol;
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
    public Usuario IniciarSesion(string usuario, string contrasena)
    {
      try
      {
        Usuario usuarioBuscado = context.Usuarios.Where((u) => u.NombreUsuario.ToLower() == usuario.ToLower() && u.Contrasena == Hash.GetSha256(contrasena)).FirstOrDefault();
        usuarioBuscado.Rol = context.Roles.Where((r) => r.Id == usuarioBuscado.IdRol).FirstOrDefault();
        usuarioBuscado.Persona = context.Personas.Where((p) => p.Id == usuarioBuscado.IdPersona).FirstOrDefault();
        return usuarioBuscado;
      }
      catch (Exception)
      {
        return null;
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