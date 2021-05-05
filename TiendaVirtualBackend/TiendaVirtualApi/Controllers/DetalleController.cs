using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static Models.DetalleModel;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DetalleController : ControllerBase
  {
    private readonly DetalleService _detalleService;

    public DetalleController(TiendaVirtualContext context)
    {
      _detalleService = new DetalleService(context);
    }

    // POST: api/detalle
    [HttpPost]
    public ActionResult<DetalleViewModel> Post(DetalleInputModel detalleInputModel)
    {
      Detalle detalle = MapToDetalle(detalleInputModel);
      var response = _detalleService.Guardar(detalle);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar al detalle", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.Detalle);
    }

    private Detalle MapToDetalle(DetalleInputModel detalleInputModel)
    {
      var detalle = new Detalle
      {
        IdDetalle = detalleInputModel.IdDetalle,
        Cantidad = detalleInputModel.Cantidad,
        IdProducto = detalleInputModel.IdProducto,
      };
      return detalle;
    }

    // GET: api/detalle
    [HttpGet]
    public IEnumerable<DetalleViewModel> Gets()
    {
      var response = _detalleService.Consultar().Select(p => new DetalleViewModel(p));
      return response;
    }


    [HttpGet("{id}")]
    public ActionResult<DetalleViewModel> Get(string id)
    {
      var detalle = _detalleService.Consultar(id);
      if (detalle == null) return NotFound();
      var detalleViewModel = new DetalleViewModel(detalle);
      return detalleViewModel;
    }
    [HttpPut("{id}")]
    public ActionResult<string> Put(Detalle detalle, string id)
    {
      var mensaje = _detalleService.Editar(id, detalle).Mensaje;
      return Ok(mensaje);
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(string id)
    {
      var mensaje = _detalleService.Eliminar(id).Mensaje;
      return Ok(mensaje);
    }
  }
}
