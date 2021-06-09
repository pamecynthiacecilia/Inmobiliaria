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
        private float importe;
        private int inmuebleId;
        private int inquilinoId;
       


        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public int Id { get => id; set => id = value; }
        public float Importe { get => importe; set => importe = value; }
        public int InquilinoId { get => inquilinoId; set => inquilinoId = value; }
        public int InmuebleId { get => inmuebleId; set => inmuebleId = value; }

        public Inquilino InquilinoContrato { get; set; }
        public Inmueble InmuebleContrato { get; set; }

    }
}
