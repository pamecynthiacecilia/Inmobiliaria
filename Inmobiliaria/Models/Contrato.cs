using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Contrato
    {
        private int id;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private decimal importe;
        private int idInmueble;
        private int idInquilino;
       
      

        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public int Id { get => id; set => id = value; }
        public decimal Importe { get => importe; set => importe = value; }
        public int IdInquilino { get => idInquilino; set => idInquilino = value; }
        public int IdInmueble { get => idInmueble; set => idInmueble = value; }
        public Inquilino InquilinoContrato { get; set; }
        public Inmueble InmuebleContrato { get; set; }

    }
}
