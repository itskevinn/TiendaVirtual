using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Data;
using Logic;
using static Models.ProveedorModel;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProveedorController : Controller
  {
    private readonly TiendaVirtualContext context;
    private ProveedorService _proveedorService;
    public ProveedorController(TiendaVirtualContext _context)
    {
      _proveedorService = new ProveedorService(_context);
      context = _context;
    }
    // POST: api/proveedor
    [HttpPost]
    public ActionResult<ProveedorViewModel> Post(ProveedorInputModel proveedorInputModel)
    {
      Proveedor proveedor = MapToProveedor(proveedorInputModel);
      var response = _proveedorService.Guardar(proveedor);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar el proveedor", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.Proveedor);
    }

    private Proveedor MapToProveedor(ProveedorInputModel proveedorInputModel)
    {
      var proveedor = new Proveedor
      {
        TipoDocumento = proveedorInputModel.TipoDocumento,
        Documento = proveedorInputModel.Documento,
        Nombre = proveedorInputModel.Nombre,
        Productos = proveedorInputModel.Productos
      };
      return proveedor;
    }

    // GET: api/AjusteInventario
    [HttpGet]
    public IEnumerable<ProveedorViewModel> Gets()
    {
      var response = _proveedorService.Consultar().ConvertAll(p => new ProveedorViewModel(p));

      return response;
    }
    [HttpGet("{id}")]
    public ActionResult<ProveedorViewModel> Get(string id)
    {
      var proveedor = _proveedorService.Consultar(id);
      if (proveedor == null) return NotFound();
      var proveedorViewModel = new ProveedorViewModel(proveedor);
      return proveedorViewModel;
    }
    [HttpPut("{id}")]
    public ActionResult<string> Put(Proveedor proveedor, string id)
    {
      var proveedorAEditar = _proveedorService.Consultar(id);
      if (proveedorAEditar == null)
      {
        return BadRequest("No se encontró el proveedor");
      }
      else
      {
        var mensaje = _proveedorService.Editar(id, proveedor).Mensaje;
        return Ok(mensaje);
      }
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(string id)
    {
      var proveedorAEliminar = _proveedorService.Consultar(id);
      if (proveedorAEliminar == null)
      {
        return BadRequest("Proveedor no econtrado");
      }
      else
      {
        var mensaje = _proveedorService.Eliminar(id).Mensaje;
        return Ok(mensaje);
      }
    }
  }
}
