using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Data;
using Logic;
using static Models.UsuarioModel;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : Controller
  {
    private readonly UsuarioService _usuarioService;
    public UsuarioController(TiendaVirtualContext context)
    {
      _usuarioService = new UsuarioService(context);
    }
    // POST: api/Usuario
    [HttpPost]
    public ActionResult<UsuarioViewModel> Post(UsuarioInputModel usuarioInputModel)
    {
      Usuario usuario = MapToUsuario(usuarioInputModel);
      var response = _usuarioService.Guardar(usuario);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar el usuario", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.Usuario);
    }

    private Usuario MapToUsuario(UsuarioInputModel usuarioInputModel)
    {
      var usuario = new Usuario
      {
        NombreUsuario = usuarioInputModel.NombreUsuario,
        Contrasena = usuarioInputModel.Contrasena,
        IdRol = usuarioInputModel.IdRol,
      };
      return usuario;
    }

    // GET: api/AjusteInventario
    [HttpGet]
    public IEnumerable<UsuarioViewModel> Gets()
    {
      var response = _usuarioService.Consultar().ConvertAll(p => new UsuarioViewModel(p));

      return response;
    }
    [HttpGet("{id}")]
    public ActionResult<UsuarioViewModel> Get(int id)
    {
      var usuario = _usuarioService.Consultar(id);
      if (usuario == null) return NotFound();
      var usuarioViewModel = new UsuarioViewModel(usuario);
      return usuarioViewModel;
    }
    [HttpPut("{id}")]
    public ActionResult<string> Put(Usuario usuario, int id)
    {
      var usuarioAEditar = _usuarioService.Consultar(id);
      if (usuarioAEditar == null)
      {
        return BadRequest("No se encontr?? el usuario");
      }
      else
      {
        var mensaje = _usuarioService.Editar(id, usuario).Mensaje;
        return Ok(mensaje);
      }
    }
  }
}
