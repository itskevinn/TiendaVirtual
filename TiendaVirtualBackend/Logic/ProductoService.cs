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
        Producto productoBuscado = ConsultarPorId(producto.Codigo);
        if (context.Proveedores.Find(producto.idProveedor) == null)
        {
          return new GuardarProductoResponse("No se encuentra el proveedor, por favor, regístrelo", true);
        }
        if (productoBuscado == null)
        {
          context.Productos.Add(producto);
          context.SaveChanges();
          return new GuardarProductoResponse(producto, "Producto guardado con éxito", false);
        }
        return new GuardarProductoResponse("Producto duplicado, por favor, rectifique la información", true);
      }
      catch (System.Exception e)
      {
        return new GuardarProductoResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
      }

    }
    public Producto ConsultarPorId(string id)
    {
      List<Producto> productos = context.Productos.ToList();
      foreach (Producto producto in productos)
      {
        if (producto.Codigo == id)
        {
          return producto;
        }
      }
      return null;
    }

    public ModificarCantidadResponse AumentarCantidad(Producto producto, int cantidad)
    {
      var productoAModificar = context.Productos.Find(producto.Id);
      productoAModificar.CantidadDisponible += cantidad;
      context.Productos.Update(productoAModificar);
      context.SaveChanges();
      return new ModificarCantidadResponse("Cantidad aumentada", false);
    }
    public ModificarCantidadResponse ReducirCantidad(Producto producto, int cantidad)
    {
      var productoAModificar = context.Productos.Find(producto.Id);
      if (producto.CantidadDisponible == 0)
      {
        return new ModificarCantidadResponse($"El producto {producto.Nombre} no está disponible", true);
      }
      if (cantidad > producto.CantidadDisponible)
      {
        return new ModificarCantidadResponse($"Unidades insuficientes. Sólo hay {producto.CantidadDisponible} unidades de {producto.Nombre}", true);
      }
      productoAModificar.CantidadDisponible -= cantidad;
      context.Productos.Update(productoAModificar);
      context.SaveChanges();
      return new ModificarCantidadResponse("Cantidad reducida", false);
    }
    public List<Producto> Consultar()
    {
      return context.Productos.ToList();
    }
    public Producto Consultar(string id)
    {
      return context.Productos.Where((p) => p.Codigo == id).FirstOrDefault();
    }
    public List<Producto> ProductosPorProveedor(int documento)
    {
      return context.Productos.Where(p => p.idProveedor == documento).ToList();
    }
    public EditarProductoResponse Editar(string id, Producto productoActualizado)
    {
      try
      {
        var productoAActualizar = context.Productos.Where((p) => p.Codigo == id).FirstOrDefault();
        if (productoAActualizar != null)
        {
          productoAActualizar.CantidadDisponible = productoActualizado.CantidadDisponible;
          productoAActualizar.Descripcion = productoActualizado.Descripcion;
          productoAActualizar.idProveedor = productoActualizado.idProveedor;
          productoAActualizar.PrecioBase = productoActualizado.PrecioBase;
          productoAActualizar.Iva = productoActualizado.Iva;
          productoAActualizar.Nombre = productoActualizado.Nombre;
          productoAActualizar.Codigo = productoActualizado.Codigo;
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
    public class ModificarCantidadResponse
    {
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public ModificarCantidadResponse(string mensaje, bool error)
      {
        Error = error;
        Mensaje = mensaje;
      }
    }
  }
}