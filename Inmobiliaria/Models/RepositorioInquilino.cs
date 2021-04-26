using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioInquilino: RepositorioBase
    {
        protected readonly IConfiguration conf;
        protected readonly string connectionString;

        public RepositorioInquilino(IConfiguration configuration) : base(configuration)
        {
            this.conf = configuration;
            connectionString = configuration["ConnectionsStrings:DefaultConnection"];
        }

        public List<Inquilino> ObtenerTodos()
        {
            var resultado = new List<Inquilino>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sql = "SELECT * FROM Inquilinos";
                using (SqlCommand command = new SqlCommand
                    (sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Inquilino
                        {
                            Id = reader.GetInt32(0),
                            Dni = reader.GetString(1),
                            Nombre = reader.GetString(2),
                            Apellido = reader.GetString(3),
                            Tel = reader.GetString(4),
                            Email = reader.GetString(5),
                            LugarDeTrabajo = reader.GetString(6),
                            DniGarante = reader.GetString(7),
                            NombreGarante = reader.GetString(8),
                            ApellidoGarante = reader.GetString(9),
                            TelGarante = reader.GetString(10),
                            EmailGarante = reader.GetString(11),
                            LugarTrabajoGarante = reader.GetString(12),
                        };
                        resultado.Add(e);
                    }
                    connection.Close();
                }
                return resultado;

            }



        }

        public int Agregar(Inquilino nuevoInquilino)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //usar interpolacion
                String sql = $"INSERT INTO Inquilinos ({nameof(Inquilino.Dni)},{nameof(Inquilino.Nombre)},{nameof(Inquilino.Apellido)},{nameof(Inquilino.Tel)},{nameof(Inquilino.Email)},{nameof(Inquilino.LugarDeTrabajo)},{nameof(Inquilino.DniGarante)},{nameof(Inquilino.NombreGarante)},{nameof(Inquilino.ApellidoGarante)},{nameof(Inquilino.TelGarante)},{nameof(Inquilino.EmailGarante)},{nameof(Inquilino.LugarTrabajoGarante)})" +

                   "VALUES  (@dni,@nombre, @apellido,@tel,@email,@lugarDeTrabajo,@dniGarante,@nombreGarante, @apellidoGarante,@telGarante,@emailGarante,@lugarTrabajoGarante);" + "SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", nuevoInquilino.Id);
                    command.Parameters.AddWithValue("@dni", nuevoInquilino.Dni);
                    command.Parameters.AddWithValue("@nombre", nuevoInquilino.Nombre);
                    command.Parameters.AddWithValue("@apellido", nuevoInquilino.Apellido);
                    command.Parameters.AddWithValue("@tel", nuevoInquilino.Tel);
                    command.Parameters.AddWithValue("@email", nuevoInquilino.Email);
                    command.Parameters.AddWithValue("@lugarDeTrabajo", nuevoInquilino.LugarDeTrabajo);
                    command.Parameters.AddWithValue("@dniGarante", nuevoInquilino.DniGarante);
                    command.Parameters.AddWithValue("@nombreGarante", nuevoInquilino.NombreGarante);
                    command.Parameters.AddWithValue("@apellidoGarante", nuevoInquilino.ApellidoGarante);
                    command.Parameters.AddWithValue("@telGarante", nuevoInquilino.TelGarante);
                    command.Parameters.AddWithValue("@emailGarante", nuevoInquilino.EmailGarante);
                    command.Parameters.AddWithValue("@lugarTrabajoGarante", nuevoInquilino.LugarTrabajoGarante);
                    connection.Open();
                    //executeScalar devuelve un solo valor en este caso el Id
                    res = Convert.ToInt32(command.ExecuteScalar());
                    nuevoInquilino.Id = res;
                    connection.Close();

                }
            }
            return res;
        }



        virtual public Inquilino ObtenerPorId(int id)
        {
            Inquilino inq = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT id, dni,nombre, apellido,tel,email,lugarDeTrabajo," +
                    $"dniGarante,nombreGarante, apellidoGarante,telGarante,emailGarante," +
                    $"lugarTrabajoGarante FROM Inquilinos i  WHERE i.id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inq = new Inquilino
                        {
                            Id = reader.GetInt32(0),
                            Dni = reader.GetString(1),
                            Nombre = reader.GetString(2),
                            Apellido = reader.GetString(3),
                            Tel = reader.GetString(4),
                            Email = reader.GetString(5),
                            LugarDeTrabajo = reader.GetString(6),
                            DniGarante = reader.GetString(7),
                            NombreGarante = reader.GetString(8),
                            ApellidoGarante = reader.GetString(9),
                            TelGarante = reader.GetString(10),
                            EmailGarante = reader.GetString(11),
                            LugarTrabajoGarante = reader.GetString(12),
                        };
                    }
                    connection.Close();
                }

                return inq;
            }

        }

        public int Borrar(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Inquilinos WHERE id = {id}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;

        }


        public int Editar(Inquilino inquilino)
        {


            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inquilinos SET dni=@dni,nombre=@nombre,apellido=@apellido,tel=@tel," +
                    $"email=@email,lugarDeTrabajo=@lugarDeTrabajo, dniGarante=@dniGarante, nombreGarante= @nombreGarante," +
                    $"apellidoGarante=@apellidoGarante,telGarante=@telGarante,lugarTrabajoGarante=@lugarTrabajoGarante WHERE id=@id";



                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", inquilino.Id);
                    command.Parameters.AddWithValue("@dni", inquilino.Dni);
                    command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
                    command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
                    command.Parameters.AddWithValue("@tel", inquilino.Tel);
                    command.Parameters.AddWithValue("@email", inquilino.Email);
                    command.Parameters.AddWithValue("@lugarDeTrabajo", inquilino.LugarDeTrabajo);
                    command.Parameters.AddWithValue("@dniGarante", inquilino.DniGarante);
                    command.Parameters.AddWithValue("@nombreGarante", inquilino.NombreGarante);
                    command.Parameters.AddWithValue("@apellidoGarante", inquilino.ApellidoGarante);
                    command.Parameters.AddWithValue("@telGarante", inquilino.TelGarante);
                    command.Parameters.AddWithValue("@emailGarante", inquilino.EmailGarante);
                    command.Parameters.AddWithValue("@lugarTrabajoGarante", inquilino.LugarTrabajoGarante);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }



    }
}
