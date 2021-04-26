using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{

    public class Pago
    {

        private int id;
        private int nroPago;
        private DateTime fechaP;
        private decimal importe;
        private int idContrato;

        public int Id { get => id; set => id = value; }
        public int NroPago { get => nroPago; set => nroPago = value; }
        public DateTime FechaP { get => fechaP; set => fechaP = value; }
        public decimal Importe { get => importe; set => importe = value; }
        public int IdContrato { get => idContrato; set => idContrato = value; }

        public Contrato ContratoPago{ get ;set; }






    }
}
