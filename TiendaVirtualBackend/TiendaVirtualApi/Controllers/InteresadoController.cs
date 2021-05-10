using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Models.InteresadoModel;

namespace TiendaVirtualApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class InteresadoController : ControllerBase
  {
    private readonly TiendaVirtualContext context;
    private InteresadoService interesadoService;
    public InteresadoController(TiendaVirtualContext _context)
    {
      interesadoService = new InteresadoService(_context);
      context = _context;
    }
    //POST: api/LiderAvaluo
    [HttpPost]
    public ActionResult<InteresadoViewModel> Post(InteresadoInputModel interesadoInputModel)
    {
      Interesado interesado = MapearAInteresado(interesadoInputModel);
      var response = interesadoService.Guardar(interesado);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar al líder de avalúos", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.Interesado);
    }

    private Interesado MapearAInteresado(InteresadoInputModel interesadoInputModel)
    {
      var interesado = new Interesado
      {
        Usuario = interesadoInputModel.Usuario
      };
      return interesado;
    }
    //GET: api/Interesado/{id}
    [HttpGet("{id}")]
    public ActionResult<InteresadoViewModel> Get(int id)
    {
      var interesado = interesadoService.Consultar(id);
      if (interesado == null) return NotFound();
      var interesadoViewModel = new InteresadoViewModel(interesado);
      return interesadoViewModel;
    }
    //GET: api/Interesado
    [HttpGet]
    public IEnumerable<InteresadoViewModel> Gets()
    {
      var response = interesadoService.Consultar().Select(p => new InteresadoViewModel(p));
      return response.ToList();
    }
  }
}