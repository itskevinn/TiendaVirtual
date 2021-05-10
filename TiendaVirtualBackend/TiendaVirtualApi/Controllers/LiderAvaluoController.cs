using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Models.LiderAvaluoModel;

namespace TiendaVirtualApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LiderAvaluoController : ControllerBase
  {
    private readonly TiendaVirtualContext context;
    private LiderAvaluoService liderAvaluoService;
    public LiderAvaluoController(TiendaVirtualContext _context)
    {
      liderAvaluoService = new LiderAvaluoService(_context);
      context = _context;
    }
    //POST: api/LiderAvaluo
    [HttpPost]
    public ActionResult<LiderAvaluoViewModel> Post(LiderAvaluoInputModel liderAvaluoInputModel)
    {
      LiderAvaluo liderAvaluo = MapearALiderAvaluo(liderAvaluoInputModel);
      var response = liderAvaluoService.Guardar(liderAvaluo);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar al líder de avalúos", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.LiderAvaluo);
    }

    private LiderAvaluo MapearALiderAvaluo(LiderAvaluoInputModel liderAvaluoInputModel)
    {
      var liderAvaluo = new LiderAvaluo
      {
        Usuario = liderAvaluoInputModel.Usuario
      };
      return liderAvaluo;
    }
    //GET: api/LiderAvaluo/{id}
    [HttpGet("{id}")]
    public ActionResult<LiderAvaluoViewModel> Get(int id)
    {
      var interesado = liderAvaluoService.Consultar(id);
      if (interesado == null) return NotFound();
      var interesadoViewModel = new LiderAvaluoViewModel(interesado);
      return interesadoViewModel;
    }
    //GET: api/Interesado
    [HttpGet]
    public IEnumerable<LiderAvaluoViewModel> Gets()
    {
      var response = liderAvaluoService.Consultar().Select(p => new LiderAvaluoViewModel(p));
      return response.ToList();
    }
  }
}