using Data;
using Entity;
using Infraestructura;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Models.InteresadoModel;
using static Models.LoginModel;

namespace TiendaVirtualApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LoginController : ControllerBase
  {
    private readonly TiendaVirtualContext _tiendaContext;
    private UsuarioService usuarioService;
    public LoginController(TiendaVirtualContext tiendaContext)
    {
      _tiendaContext = tiendaContext;
      var admin = _tiendaContext.Usuarios.Find(1);
      if (admin == null)
      {
        _tiendaContext.Usuarios.Add(new Entity.Usuario() { Rol = new Rol { Nombre = "Administrador", IdRol = 1 }, Contrasena = Hash.GetSha256("admin"), NombreUsuario = "admin" });
        var i = _tiendaContext.SaveChanges();
      }
      usuarioService = new UsuarioService(tiendaContext);
    }
    [HttpPost]
    public ActionResult<LoginViewModel> Post(LoginInputModel usuario)
    {
      var _usuario = usuarioService.IniciarSesion(usuario.Usuario, usuario.Contrasena);
      if (_usuario == null)
      {
        ModelState.AddModelError("No se pudo iniciar sesión", "Usuario y/o contraseña incorrectos");
        var problemDetails = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status401Unauthorized,
        };
        return Unauthorized(problemDetails);
      }
      return Ok(_usuario);
    }
  }
}