using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class FacturaRepository
    {
        private readonly AppDbContext _context;

        public FacturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddFactura(FacturaModel factura)
        {
            _context.factura.Add(factura);
            _context.SaveChanges();
        }

        public void UpdateFactura(FacturaModel factura)
        {
            _context.factura.Update(factura);
            _context.SaveChanges();
        }

        public FacturaModel GetFacturaById(int id)
        {
            return _context.factura.FirstOrDefault(c => c.id == id);
        }
        public void DeleteFactura(int id)
        {
            var factura = _context.factura.FirstOrDefault(c => c.id == id);
            if (factura != null)
            {
                _context.factura.Remove(factura);
                _context.SaveChanges();
            }
        }
        public IEnumerable<FacturaModel> GetAllFacturas()
        {
            return _context.factura.ToList();
        }
    }
}
