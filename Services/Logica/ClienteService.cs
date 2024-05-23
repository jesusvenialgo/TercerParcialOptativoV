using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logica
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public bool ValidarCliente(ClienteModel cliente)
        {
            // Validar nombre
            if (string.IsNullOrEmpty(cliente.nombre) || cliente.nombre.Length < 3)
            {
                return false;
            }
            // Validar apellido
            if (string.IsNullOrEmpty(cliente.apellido) || cliente.apellido.Length < 3)
            {
                return false;
            }

            // Validar cédula
            if (string.IsNullOrEmpty(cliente.documento) || cliente.documento.Length < 3)
            {
                return false;
            }

            if (!int.TryParse(cliente.celular, out _) || cliente.celular.Length != 10)
            {
                return false;
            }
            return true;
        }
    }
}

