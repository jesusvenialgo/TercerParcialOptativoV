using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services.Logica;

namespace SegundoParcialJesusVenialgo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaRepository _facturaRepository;
        private readonly FacturaService _facturaService;


        public FacturaController(FacturaRepository facturaRepository, FacturaService facturaService)
        {
            _facturaRepository = facturaRepository;
            _facturaService = facturaService;
        }
        [HttpPost("ADD")]
        public IActionResult CreateFactura([FromBody] FacturaModel factura)
        {
            if (!_facturaService.ValidarDatosFacturas(factura))
                return BadRequest("Los datos cargados no son válidos");

            _facturaRepository.AddFactura(factura);
            return Ok("Se han agregado los datos correctamente");
        }
        [HttpPut("UPDATE")]
        public IActionResult UpdateFactura(int id, [FromBody] FacturaModel factura)
        {
            var exFact = _facturaRepository.GetFacturaById(id);
            if (exFact == null)
                return NotFound("ID no encontrado.");

            if (!_facturaService.ValidarDatosFacturas(factura))
                return BadRequest("Se encontraron problemas con las validaciones.");

            exFact.nro_factura = factura.nro_factura;
            exFact.fecha_hora = factura.fecha_hora;
            exFact.total = factura.total;
            exFact.total_iva5 = factura.total_iva5;
            exFact.total_iva10 = factura.total_iva10;
            exFact.total_iva = factura.total_iva;
            exFact.total_letras = factura.total_letras;
            exFact.sucursal = factura.sucursal;

            _facturaRepository.UpdateFactura(exFact);

            return Ok("Datos actualizados de manera exitosa.");
        }
        [HttpGet("GET")]
        public IActionResult GetFacturaById(int id)
        {
            var factura = _facturaRepository.GetFacturaById(id);
            if (factura == null)
                return NotFound("Datos no encontrados");
            return Ok(factura);
        }
        [HttpDelete("DELETE")]
        public IActionResult DeleteFactura(int id)
        {
            var cliente = _facturaRepository.GetFacturaById(id);
            if (cliente == null)
                return NotFound("No se han encontrados los datos ingresados");

            _facturaRepository.DeleteFactura(id);
            return NoContent();
        }
        [HttpGet("LIST")]
        public IActionResult GetAllFacturas()
        {
            var clientes = _facturaRepository.GetAllFacturas();
            return Ok(clientes);
        }
    }
}
