using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Models.RolModel;

namespace TiendaVirtualApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RolController : ControllerBase
  {
    private readonly TiendaVirtualContext context;
    private RolService rolService;
    public RolController(TiendaVirtualContext _context)
    {
      context = _context;
      rolService = new RolService(context);
    }
    // POST: api/Rol
    [HttpPost]
    public ActionResult<RolViewModel> Post(RolInputModel rolInputModel)
    {
      Rol rol = MapearARol(rolInputModel);
      var response = rolService.Guardar(rol);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar el rol", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.Rol);
    }

    private Rol MapearARol(RolInputModel rolInputModel)
    {
      var rol = new Rol
      {
        Nombre = rolInputModel.Nombre
      };
      return rol;
    }
    //GET: api/Rol/{id}
    [HttpGet("{id}")]
    public ActionResult<RolViewModel> Get(int id)
    {
      var rol = rolService.Consultar(id);
      if (rol == null) return NotFound();
      var rolViewModel = new RolViewModel(rol);
      return rolViewModel;
    }
    //GET: api/Rol
    [HttpGet]
    public IEnumerable<RolViewModel> Gets()
    {
      var response = rolService.Consultar().Select(p => new RolViewModel(p));
      return response.ToList();
    }
  }
}