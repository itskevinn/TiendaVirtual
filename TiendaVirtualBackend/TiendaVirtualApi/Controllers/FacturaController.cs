using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static Models.FacturaModel;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FacturaController : ControllerBase
  {
    private readonly FacturaService _facturaService;

    public FacturaController(TiendaVirtualContext context)
    {
      _facturaService = new FacturaService(context);
    }

    // POST: api/factura
    [HttpPost]
    public ActionResult<FacturaViewModel> Post(FacturaInputModel facturaInputModel)
    {
      Factura factura = MapToFactura(facturaInputModel);
      var response = _facturaService.Guardar(factura);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar la factura", response.Mensaje);
        var facturasProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(facturasProblema);
      }
      return Ok(response.Factura);
    }

    private Factura MapToFactura(FacturaInputModel facturaInputModel)
    {
      var factura = new Factura
      {
        IdFactura = facturaInputModel.IdFactura,
        Detalles = facturaInputModel.Detalles,
        IdUsuario = facturaInputModel.IdUsuario
      };
      return factura;
    }

    // GET: api/factura
    [HttpGet]
    public IEnumerable<FacturaViewModel> Gets()
    {
      var response = _facturaService.Consultar().Select(p => new FacturaViewModel(p));
      return response;
    }


    [HttpGet("{id}")]
    public ActionResult<FacturaViewModel> Get(string id)
    {
      var factura = _facturaService.Consultar(id);
      if (factura == null) return NotFound();
      var facturaViewModel = new FacturaViewModel(factura);
      return facturaViewModel;
    }
    [HttpPut("{id}")]
    public ActionResult<string> Put(Factura factura, string id)
    {
      var mensaje = _facturaService.Editar(id, factura).Mensaje;
      return Ok(mensaje);
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(string id)
    {
      var mensaje = _facturaService.Eliminar(id).Mensaje;
      return Ok(mensaje);
    }
  }
}