using System.Collections.Generic;
using System.Linq;
using Data;
using Entity;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static Models.ProductoModel;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductoController : ControllerBase
  {
    private ProductoService _productoService;
    private readonly TiendaVirtualContext context;

    public ProductoController(TiendaVirtualContext _context)
    {
      _productoService = new ProductoService(_context);
      context = _context;
    }

    // POST: api/Producto
    [HttpPost]
    public ActionResult<ProductoViewModel> Post(ProductoInputModel productoInputModel)
    {
      Producto producto = MapToProducto(productoInputModel);
      var response = _productoService.Guardar(producto);
      if (response.Error)
      {
        ModelState.AddModelError("Error al registrar al producto", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.Producto);
    }

    private Producto MapToProducto(ProductoInputModel productoInputModel)
    {
      var producto = new Producto
      {
        Codigo = productoInputModel.Id,
        Nombre = productoInputModel.Nombre,
        Descripcion = productoInputModel.Descripcion,
        PrecioBase = productoInputModel.PrecioBase,
        CantidadDisponible = productoInputModel.CantidadDisponible,
        Descuento = productoInputModel.Descuento,
        idProveedor = productoInputModel.IdProveedor,
        Iva = productoInputModel.Iva,
      };
      return producto;
    }

    // GET: api/Producto
    [HttpGet]
    public IEnumerable<ProductoViewModel> Gets()
    {
      var response = _productoService.Consultar().Select(p => new ProductoViewModel(p));
      return response;
    }


    [HttpGet("{id}")]
    public ActionResult<ProductoViewModel> Get(string id)
    {
      var producto = _productoService.Consultar(id);
      if (producto == null) return NotFound();
      var productoViewModel = new ProductoViewModel(producto);
      return productoViewModel;
    }
    [HttpPut("{id}")]
    public ActionResult<ProductoViewModel> Put(Producto producto, string id)
    {
      var response = _productoService.Editar(id, producto);
      if (response.Error)
      {
        ModelState.AddModelError("Error al editar el producto", response.Mensaje);
        var detallesProblema = new ValidationProblemDetails(ModelState)
        {
          Status = StatusCodes.Status400BadRequest
        };
        return BadRequest(detallesProblema);
      }
      return Ok(response.Producto);
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(string id)
    {
      var mensaje = _productoService.Eliminar(id).Mensaje;
      return Ok(mensaje);
    }
  }
}
