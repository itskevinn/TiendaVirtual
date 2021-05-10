using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Models.ProfesionalVentaModel;

namespace TiendaVirtualApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfesionalVentaController : ControllerBase
  {
    private readonly TiendaVirtualContext context;
    private ProfesionalVentaService profesionalVentaService;
    public ProfesionalVentaController(TiendaVirtualContext _context)
    {
      profesionalVentaService = new ProfesionalVentaService(_context);
      context = _context;
    }
    // POST: api/ProfesionalVenta
    [HttpPost]
    public ActionResult<ProfesionalVentaViewModel> Post(ProfesionalVentaInputModel profesionalVentaInputModel)
    {
      ProfesionalVenta profesionalVenta = MapearAProfesionalVenta(profesionalVentaInputModel);
      var response = profesionalVentaService.Guardar(profesionalVenta);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar al profesional de ventas", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.ProfesionalVenta);
    }

    private ProfesionalVenta MapearAProfesionalVenta(ProfesionalVentaInputModel profesionalVentaInputModel)
    {
      var profesionalVenta = new ProfesionalVenta
      {
        Usuario = profesionalVentaInputModel.Usuario
      };
      return profesionalVenta;
    }
    //GET: api/ProfesionalVenta/{id}
    [HttpGet("{id}")]
    public ActionResult<ProfesionalVentaViewModel> Get(int id)
    {
      var profesionalVenta = profesionalVentaService.Consultar(id);
      if (profesionalVenta == null) return NotFound();
      var profesionalVentaViewModel = new ProfesionalVentaViewModel(profesionalVenta);
      return profesionalVentaViewModel;
    }
    //GET: api/ProfesionalVenta
    [HttpGet]
    public IEnumerable<ProfesionalVentaViewModel> Gets()
    {
      var response = profesionalVentaService.Consultar().Select(p => new ProfesionalVentaViewModel(p));
      return response.ToList();
    }
  }
}