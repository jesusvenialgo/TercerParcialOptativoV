using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services.Logica;

namespace SegundoParcialJesusVenialgo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
        {
            private readonly ClienteRepository _clienteRepository;
            private readonly ClienteService _clienteService;


        public ClienteController(ClienteRepository clienteRepository, ClienteService clienteService)
            {
                _clienteRepository = clienteRepository;
                _clienteService = clienteService;
            }
            [HttpPost ("ADD")]
            public IActionResult CreateCliente([FromBody] ClienteModel cliente)
            {
                if (!_clienteService.ValidarCliente(cliente))
                    return BadRequest("Los datos cargados no son válidos");

                _clienteRepository.AddCliente(cliente);
                return Ok("Se han agregado los datos correctamente");
            }
            [HttpPut("UPDATE")]
            public IActionResult PutCliente(int id, [FromBody] ClienteModel cliente)
            {
                var exCli = _clienteRepository.GetClienteById(id);
                if (exCli == null)
                    return NotFound("ID no encontrado.");

                if (!_clienteService.ValidarCliente(cliente))
                    return BadRequest("Se encontraron problemas con las validaciones.");

                exCli.nombre = cliente.nombre;
                exCli.apellido = cliente.apellido;
                exCli.documento = cliente.documento;
                exCli.direccion = cliente.direccion;
                exCli.mail = cliente.mail;
                exCli.celular = cliente.celular;
                exCli.estado = cliente.estado;

                _clienteRepository.UpdateCliente(exCli);

                return Ok("Datos actualizados de manera exitosa.");
            }
        [HttpGet("GET")]
            public IActionResult GetClienteById(int id)
            {
                var cliente = _clienteRepository.GetClienteById(id);
                if (cliente == null)
                    return NotFound("Datos no encontrados");
                return Ok(cliente);
            }
            [HttpDelete("DELETE")]
            public IActionResult DeleteCliente(int id)
            {
                var cliente = _clienteRepository.GetClienteById(id);
                if (cliente == null)
                    return NotFound("No se han encontrados los datos ingresados");

                _clienteRepository.DeleteCliente(id);
                return NoContent();
            }
            [HttpGet("LIST")]
            public IActionResult GetAllClientes()
            {
                var clientes = _clienteRepository.GetAllClientes();
                return Ok(clientes);
            }
        }
    }
