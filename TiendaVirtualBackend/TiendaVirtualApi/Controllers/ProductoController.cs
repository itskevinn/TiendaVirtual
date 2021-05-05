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
    private readonly ProductoService _productoService;

    public ProductoController(TiendaVirtualContext context)
    {
      _productoService = new ProductoService(context);
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
        Id = productoInputModel.Id,
        Nombre = productoInputModel.Nombre,
        Descripcion = productoInputModel.Descripcion,
        PrecioBase = productoInputModel.Precio,
        CantidadDisponible = productoInputModel.Cantidad,
        Descuento = productoInputModel.Descuento,
        NitProveedor = productoInputModel.NitProveedor,
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
    public ActionResult<string> Put(Producto producto, string id)
    {
      var mensaje = _productoService.Editar(id, producto).Mensaje;
      return Ok(mensaje);
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(string id)
    {
      var mensaje = _productoService.Eliminar(id).Mensaje;
      return Ok(mensaje);
    }
  }
}
