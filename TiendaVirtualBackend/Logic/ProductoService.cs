using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;

namespace Logic
{
  public class ProductoService
  {
    private TiendaVirtualContext context;
    public ProductoService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
    }
    public GuardarProductoResponse Guardar(Producto producto)
    {
      try
      {
        Producto productoBuscado = context.Productos.Find(producto.Id);
        if (productoBuscado != null)
        {
          context.Productos.Add(producto);
          context.SaveChanges();
          return new GuardarProductoResponse(producto, "Producto guardado con éxito", false);
        }
        return new GuardarProductoResponse("Producto duplicado, por favor, rectifique la información", true);
      }
      catch (System.Exception)
      {
        return new GuardarProductoResponse("Ha ocurrido un error en el servidor. Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Producto> Consultar()
    {
      return context.Productos.ToList();
    }
    public EditarProductoResponse Editar(string id, Producto productoActualizado)
    {
      try
      {
        var productoAActualizar = context.Productos.Find(id);
        if (productoAActualizar != null)
        {
          productoAActualizar.Cantidad = productoActualizado.Cantidad;
          productoAActualizar.Descripcion = productoActualizado.Descripcion;
          productoAActualizar.NitProveedor = productoActualizado.NitProveedor;
          productoAActualizar.Precio = productoActualizado.Precio;
          productoAActualizar.Nombre = productoActualizado.Nombre;
          productoAActualizar.Id = productoActualizado.Id;
          context.Productos.Update(productoAActualizar);
          context.SaveChanges();
          return new EditarProductoResponse(productoAActualizar, "Producto editado correctamente", false);
        }
        else
        {
          return new EditarProductoResponse("Producto no encontrado", true);
        }
      }
      catch (Exception e)
      {
        return new EditarProductoResponse($"Ocurrió un error al editar el producto {e.Message}", true);
      }
    }
    public EliminarProductoResponse Eliminar(string id)
    {
      try
      {
        var productoAEliminar = context.Productos.Find(id);
        if (productoAEliminar != null)
        {
          context.Productos.Remove(productoAEliminar);
          context.SaveChanges();
          return new EliminarProductoResponse(productoAEliminar, "Producto eliminado correctamente");
        }
        return new EliminarProductoResponse("No se encontró el producto");
      }
      catch (Exception e)
      {
        return new EliminarProductoResponse("Ocurrió un error al eliminar el producto " + e.Message);
      }
    }
    public class EliminarProductoResponse
    {
      public Producto Producto { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EliminarProductoResponse(Producto producto, string mensaje)
      {
        Mensaje = mensaje;
        Producto = producto;
        Error = false;
      }
      public EliminarProductoResponse(string mensaje)
      {
        Mensaje = mensaje;
        Error = true;
      }
    }
    public class EditarProductoResponse
    {
      public Producto Producto { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public EditarProductoResponse(Producto producto, string mensaje, bool error)
      {
        Producto = producto;
        Mensaje = mensaje;
        Error = error;
      }
      public EditarProductoResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
    public class GuardarProductoResponse
    {
      public Producto Producto { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarProductoResponse(Producto producto, string mensaje, bool error)
      {
        Producto = producto;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarProductoResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}