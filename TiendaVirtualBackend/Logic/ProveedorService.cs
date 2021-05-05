using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;

namespace Logic
{
  public class Proveedoreservice
  {
    private TiendaVirtualContext context;
    public Proveedoreservice(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
    }
    public GuardarProveedorResponse Guardar(Proveedor proveedor)
    {
      try
      {
        Proveedor proveedorBuscado = context.Proveedores.Find(proveedor.Nit);
        if (proveedorBuscado != null)
        {
          context.Proveedores.Add(proveedor);
          context.SaveChanges();
          return new GuardarProveedorResponse(proveedor, "Proveedor guardado con éxito", false);
        }
        return new GuardarProveedorResponse("Proveedor duplicado, por favor, rectifique la información", true);
      }
      catch (System.Exception)
      {
        return new GuardarProveedorResponse("Ha ocurrido un error en el servidor. Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Proveedor> Consultar()
    {
      return context.Proveedores.ToList();
    }
    public EditarProveedorResponse Editar(string nit, Proveedor proveedorActualizado)
    {
      try
      {
        var proveedorAActualizar = context.Proveedores.Find(nit);
        if (proveedorAActualizar != null)
        {
          proveedorAActualizar.Nombre = proveedorActualizado.Nombre;
          context.Proveedores.Update(proveedorAActualizar);
          context.SaveChanges();
          return new EditarProveedorResponse(proveedorAActualizar, "Proveedor editado correctamente", false);
        }
        else
        {
          return new EditarProveedorResponse("Proveedor no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarProveedorResponse($"Ocurrió un error al editar el proveedor {e.Message}", true);
      }
    }
    public EliminarProveedorResponse Eliminar(string id)
    {
      try
      {
        var proveedorAEliminar = context.Proveedores.Find(id);
        if (proveedorAEliminar != null)
        {
          context.Proveedores.Remove(proveedorAEliminar);
          context.SaveChanges();
          return new EliminarProveedorResponse(proveedorAEliminar, "Proveedor eliminado correctamente");
        }
        return new EliminarProveedorResponse("No se encontró el proveedor");
      }
      catch (Exception e)
      {
        return new EliminarProveedorResponse("Ocurrió un error al eliminar el proveedor " + e.Message);
      }
    }
    public class EliminarProveedorResponse
    {
      public Proveedor Proveedor { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EliminarProveedorResponse(Proveedor proveedor, string mensaje)
      {
        Mensaje = mensaje;
        Proveedor = proveedor;
        Error = false;
      }
      public EliminarProveedorResponse(string mensaje)
      {
        Mensaje = mensaje;
        Error = true;
      }
    }
    public class EditarProveedorResponse
    {
      public Proveedor Proveedor { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarProveedorResponse(Proveedor proveedor, string mensaje, bool error)
      {
        Proveedor = proveedor;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarProveedorResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarProveedorResponse
    {
      public Proveedor Proveedor { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarProveedorResponse(Proveedor proveedor, string mensaje, bool error)
      {
        Proveedor = proveedor;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarProveedorResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}