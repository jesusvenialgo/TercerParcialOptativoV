using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services;

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

        [HttpPost("ADD")]
        public IActionResult CreateCliente([FromBody] ClienteModel cliente)
        {
            var validador = new Services.ClienteService.ValidarClienteFluente(_clienteRepository);
            var resultado = validador.Validate(cliente);

            if (!resultado.IsValid)
            {
                return BadRequest(resultado.Errors);
            }

            _clienteRepository.AddCliente(cliente);
            return Ok("Se han agregado los datos correctamente");
        }

        [HttpPut("UPDATE")]
        public IActionResult PutCliente([FromBody] ClienteModel cliente)
        {
            var exCli = _clienteRepository.GetClienteById(cliente.id);
            if (exCli == null)
                return NotFound("ID no encontrado.");

            var validador = new Services.ClienteService.ValidarClienteFluente(_clienteRepository);
            var resultado = validador.Validate(cliente);

            if (!resultado.IsValid)
            {
                return BadRequest(resultado.Errors);
            }

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
        public IActionResult GetAll()
        {
            var clientes = _clienteRepository.GetAllClientes();
            return Ok(clientes);
        }
    }
}