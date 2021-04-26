using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Inmueble
    {
        private int id;
        private string tipo;
        private string uso;
        private string direccion;
        private int cantAmbientes;
        private decimal precio;
        private int idPropietario;
        private int disponible;

       
        public int Id { get => id; set => id = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Uso { get => uso; set => uso = value; }
        public string Direccion { get =>direccion; set => direccion = value; }
        public int CantAmbientes { get => cantAmbientes; set => cantAmbientes = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int IdPropietario { get => idPropietario; set => idPropietario = value; }
        public int Disponible { get => disponible; set => disponible = value; }

        
        public Propietario PropietarioInmueble { get; set; }

    }
}
