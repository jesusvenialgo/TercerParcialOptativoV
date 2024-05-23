using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class ClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCliente(ClienteModel cliente)
        {
            _context.cliente.Add(cliente);
            _context.SaveChanges();
        }

        public void UpdateCliente(ClienteModel cliente)
        {
            _context.cliente.Update(cliente);
            _context.SaveChanges();
        }

        public ClienteModel GetClienteById(int id)
        {
            return _context.cliente.FirstOrDefault(c => c.id == id);
        }
        public void DeleteCliente(int id)
        {
            var cliente = _context.cliente.FirstOrDefault(c => c.id == id);
            if (cliente != null)
            {
                _context.cliente.Remove(cliente);
                _context.SaveChanges();
            }
        }
        public IEnumerable<ClienteModel> GetAllClientes()
        {
            return _context.cliente.ToList();
        }
    }
}
