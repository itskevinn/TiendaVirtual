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
    private TiendaVirtualContext context;
    private readonly DetalleService detalleService;
    private readonly ProductoService productoService;
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
      try
      {
        Factura facturaBuscada = context.Facturas.Find(factura.IdFactura);
        Interesado interesado = context.Interesados.Find(factura.IdInteresado);
        if (facturaBuscada == null)
        {
          if (interesado == null && factura.Tipo == "venta")
          {
            factura.IdInteresado = "No registrado";
          }

          factura.Detalles.ForEach((d) => d.Tipo = factura.Tipo == "venta" ? "resta" : "aumento");
          factura.Detalles.ForEach((d) => d.IdFactura = GenerarIdFacturaTemporal());
          foreach (Detalle detalle in factura.Detalles)
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
        return new GuardarFacturaResponse("Factura duplicada, por favor, rectifique la información", true);
      }
      catch (System.Exception e)
      {
        return new GuardarFacturaResponse($"Ha ocurrido un error en el servidor. {e.Message} Por favor, vuelva a internar más tarde", true);
      }

    }
    public List<Factura> Consultar()
    {
      List<Factura> facturas = context.Facturas.ToList();
      facturas.ForEach((f) => f.Detalles = detalleService.ConsultarPorFactura(f.IdFactura));
      return facturas;
    }
    public List<Factura> ConsultarPorTipo(string tipo)
    {
      List<Factura> facturas = context.Facturas.Where((f) => f.Tipo.ToLower() == tipo).ToList();
      facturas.ForEach((f) => f.Detalles = detalleService.ConsultarPorFactura(f.IdFactura));
      return facturas;
    }
    public List<Factura> ConsultarPorInteresado(string idInteresado)
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