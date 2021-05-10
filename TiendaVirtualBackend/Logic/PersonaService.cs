using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;

namespace Logic
{
  public class PersonaService
  {
    private readonly TiendaVirtualContext context;
    private FacturaService facturaService;
    public PersonaService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      facturaService = new FacturaService(tiendaVirtualContext);
    }
    public GuardarPersonaResponse Guardar(Persona persona)
    {
      try
      {
        context.Personas.Add(persona);
        context.SaveChanges();
        return new GuardarPersonaResponse(persona, "Persona guardado con éxito", false);
      }
      catch (System.Exception)
      {
        return new GuardarPersonaResponse("Ha ocurrido un error en el servidor. Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Persona> Consultar()
    {
      List<Persona> personas = context.Personas.ToList();
      return personas;
    }
    public Persona Consultar(int id)
    {
      Persona persona = context.Personas.Find(id);
      return persona;
    }
    public EditarPersonaResponse Editar(string id, Persona personaActualizado)
    {
      try
      {
        var personaAActualizar = context.Personas.Find(id);
        if (personaAActualizar != null)
        {
          personaAActualizar.Nombre = personaActualizado.Nombre;
          context.Personas.Update(personaAActualizar);
          context.SaveChanges();
          return new EditarPersonaResponse(personaAActualizar, "Persona editado correctamente", false);
        }
        else
        {
          return new EditarPersonaResponse("Persona no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarPersonaResponse($"Ocurrió un error al editar el persona {e.Message}", true);
      }
    }
    public class EditarPersonaResponse
    {
      public Persona Persona { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarPersonaResponse(Persona persona, string mensaje, bool error)
      {
        Persona = persona;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarPersonaResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarPersonaResponse
    {
      public Persona Persona { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarPersonaResponse(Persona persona, string mensaje, bool error)
      {
        Persona = persona;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarPersonaResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}