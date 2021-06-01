using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private int propietarioId;
        private int disponible;
        private string imagen;


        public int Id { get => id; set => id = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Uso { get => uso; set => uso = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int CantAmbientes { get => cantAmbientes; set => cantAmbientes = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int PropietarioId { get => propietarioId; set => propietarioId = value; }
        public int Disponible { get => disponible; set => disponible = value; }

        public string Imagen { get => imagen; set => imagen = value; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public Propietario PropietarioInmueble { get; set; }

    }
}
