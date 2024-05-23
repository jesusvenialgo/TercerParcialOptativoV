using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Logica
{
    public class FacturaService
    {
        public bool ValidarDatosFacturas(FacturaModel factura)
        {
            return true;
            // Validar Nro. Factura
            if (string.IsNullOrEmpty(factura.nro_factura) || !Regex.IsMatch(factura.nro_factura, @"^\d{3}-\d{3}-\d{6}$"))
            {
                return false;
            }

            // Validar Total, Total_iva5, Total_iva10, Total_iva (datos numéricos obligatorios)
            if (!decimal.TryParse(factura.total, out decimal total) || total <= 0 ||
                !decimal.TryParse(factura.total_iva5, out decimal totalIva5) || totalIva5 <= 0 ||
                !decimal.TryParse(factura.total_iva10, out decimal totalIva10) || totalIva10 <= 0 ||
                !decimal.TryParse(factura.total_iva, out decimal totalIva) || totalIva <= 0)
            {
                return false;
            }

            // Validar Total en letras (obligatorio, al menos 6 caracteres)
            if (string.IsNullOrEmpty(factura.total_letras) || factura.total_letras.Length < 6)
            {
                return false;
            }

            return true;
        }

    }
}
