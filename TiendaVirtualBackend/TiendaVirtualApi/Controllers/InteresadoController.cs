using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Data;
using Logic;
using static Models.InteresadoModel;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InteresadoController : Controller
  {
    private readonly InteresadoService _interesadoService;
    public InteresadoController(TiendaVirtualContext context)
    {
      _interesadoService = new InteresadoService(context);
    }
    // POST: api/Interesado
    [HttpPost]
    public ActionResult<InteresadoViewModel> Post(InteresadoInputModel interesadoInputModel)
    {
      Interesado interesado = MapToInteresado(interesadoInputModel);
      var response = _interesadoService.Guardar(interesado);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar el interesado", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.Interesado);
    }

    private Interesado MapToInteresado(InteresadoInputModel interesadoInputModel)
    {
      var interesado = new Interesado
      {
        IdUsuario = interesadoInputModel.IdUsuario,
        IdInteresado = interesadoInputModel.IdInteresado,
        Facturas = interesadoInputModel.Facturas,
        Usuario = interesadoInputModel.Usuario
      };
      return interesado;
    }

    // GET: api/AjusteInventario
    [HttpGet]
    public IEnumerable<InteresadoViewModel> Gets()
    {
      var response = _interesadoService.Consultar().ConvertAll(p => new InteresadoViewModel(p));

      return response;
    }
    [HttpGet("{id}")]
    public ActionResult<InteresadoViewModel> Get(string id)
    {
      var interesado = _interesadoService.Consultar(id);
      if (interesado == null) return NotFound();
      var interesadoViewModel = new InteresadoViewModel(interesado);
      return interesadoViewModel;
    }
    [HttpPut("{id}")]
    public ActionResult<string> Put(Interesado interesado, string id)
    {
      var interesadoAEditar = _interesadoService.Consultar(id);
      if (interesadoAEditar == null)
      {
        return BadRequest("No se encontró el interesado");
      }
      else
      {
        var mensaje = _interesadoService.Editar(id, interesado).Mensaje;
        return Ok(mensaje);
      }
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(string id)
    {
      var interesadoAEliminar = _interesadoService.Consultar(id);
      if (interesadoAEliminar == null)
      {
        return BadRequest("Interesado no econtrado");
      }
      else
      {
        var mensaje = _interesadoService.Eliminar(id).Mensaje;
        return Ok(mensaje);
      }
    }
  }
}
