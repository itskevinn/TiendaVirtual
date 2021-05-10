using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Logic
{
  public class FacturaService
  {
    private readonly TiendaVirtualContext context;
    private DetalleService detalleService;
    private ProductoService productoService;
    public FacturaService(TiendaVirtualContext tiendaVirtualContext)
    {
      context = tiendaVirtualContext;
      detalleService = new DetalleService(tiendaVirtualContext);
      productoService = new ProductoService(tiendaVirtualContext);
    }
    private int GenerarIdFacturaTemporal()
    {
      int id = context.Facturas.Count();
      return id + 1;
    }
    public GuardarFacturaResponse Guardar(Factura factura)
    {
      using (var transaccion = context.Database.BeginTransaction())
      {
        try
        {
          Interesado interesado = context.Interesados.Find(factura.IdInteresado);
          if (interesado == null && factura.Tipo == "venta")
          {
            factura.IdInteresado = 0;
          }
          foreach (Detalle detalle in factura.ObtenerDetalles())
          {
            if (detalleService.Guardar(detalle).Error)
            {
              return new GuardarFacturaResponse(detalleService.Guardar(detalle).Mensaje, true);
            }
            detalleService.Guardar(detalle);
          }
          factura.CalcularTotales();
          context.Facturas.Add(factura);
          context.SaveChanges();
          return new GuardarFacturaResponse(factura, "Factura guardada con éxito", false);
        }
        catch (System.Exception e)
        {
          transaccion.Rollback();
          return new GuardarFacturaResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
        }
      }
    }
    public List<Factura> Consultar()
    {
      List<Factura> facturas = context.Facturas.ToList();
      facturas.ForEach((f) => detalleService.ConsultarPorFactura(f.IdFactura).ForEach((d) => f.AgregarDetalle(d)));
      return facturas;
    }
    public List<Factura> ConsultarPorTipo(string tipo)
    {
      List<Factura> facturas = context.Facturas.Where((f) => f.Tipo.ToLower() == tipo).ToList();
      facturas.ForEach((f) => detalleService.ConsultarPorFactura(f.IdFactura).ForEach((d) => f.AgregarDetalle(d)));
      return facturas;
    }
    public List<Factura> ConsultarPorInteresado(int idInteresado)
    {
      return context.Facturas.Where((f) => f.IdInteresado == idInteresado).ToList();
    }
    public Factura Consultar(string id)
    {
      return context.Facturas.Find(id);
    }

    public class GuardarFacturaResponse
    {
      public Factura Factura { get; set; }
      public string Mensaje { get; set; }
      public bool Error { get; set; }
      public GuardarFacturaResponse(Factura factura, string mensaje, bool error)
      {
        Factura = factura;
        Mensaje = mensaje;
        Error = error;
      }
      public GuardarFacturaResponse(string mensaje, bool error)
      {
        Mensaje = mensaje;
        Error = error;
      }
    }
  }
}