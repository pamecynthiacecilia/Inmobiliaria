using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Inquilino
    {
        private int id;
        private string dni;
        private string nombre;
        private string apellido;
        private string tel;
        private string email;
        private string lugarDeTrabajo;
        private string dniGarante;
        private string nombreGarante;
        private string apellidoGarante;
        private string telGarante;
        private string emailGarante;
        private string lugarTrabajoGarante;

        public int Id { get => id; set => id = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Tel { get => tel; set => tel = value; }
        public string Email { get => email; set => email = value; }
        public string LugarDeTrabajo { get => lugarDeTrabajo; set => lugarDeTrabajo = value; }
        public string DniGarante { get => dniGarante; set => dniGarante = value; }
        public string NombreGarante { get => nombreGarante; set => nombreGarante = value; }
        public string ApellidoGarante { get => apellidoGarante; set => apellidoGarante = value; }
        public string TelGarante { get => telGarante; set => telGarante = value; }
        public string EmailGarante { get => emailGarante; set => emailGarante = value; }
        public string LugarTrabajoGarante { get => lugarTrabajoGarante; set => lugarTrabajoGarante = value; }

    }

}

