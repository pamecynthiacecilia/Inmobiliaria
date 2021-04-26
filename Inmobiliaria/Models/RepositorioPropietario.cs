using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioPropietario : RepositorioBase
    {

        protected readonly IConfiguration conf;
        protected readonly string connectionString;

        public RepositorioPropietario(IConfiguration configuration) : base(configuration)
        {
            this.conf = configuration;
            connectionString = configuration["ConnectionsStrings:DefaultConnection"];
        }



        public List<Propietario> ObtenerTodos()
        {
            var resultado = new List<Propietario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Propietarios";
                using (SqlCommand command = new SqlCommand
                    (sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Dni = reader.GetString(1),
                            Nombre = reader.GetString(2),
                            Apellido = reader.GetString(3),
                            Tel = reader.GetString(4),
                            Email = reader.GetString(5),

                        };
                        resultado.Add(e);
                    }
                    connection.Close();
                }
                return resultado;

            }



        }
        public int Agregar(Propietario propietario)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //usar interpolacion
                String sql = $"INSERT INTO Propietarios ({nameof(Propietario.Dni)},{nameof(Propietario.Nombre)},{nameof(Propietario.Apellido)},{nameof(Propietario.Tel)},{nameof(Propietario.Email)})" +

                    "VALUES  (@dni,@nombre, @apellido,@tel,@email);" + "SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                   
                    command.Parameters.AddWithValue("@dni", propietario.Dni);
                    command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                    command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@tel", propietario.Tel);
                    command.Parameters.AddWithValue("@email", propietario.Email);
                    connection.Open();

                    res = Convert.ToInt32(command.ExecuteScalar());
                    propietario.Id = res;
                    connection.Close();

                }
            }
            return res;
        }



        virtual public Propietario Detalles(int id)
        {
            Propietario p = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT id, dni,nombre, apellido,tel,email FROM Propietarios p WHERE p.id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Dni = reader.GetString(1),
                            Nombre = reader.GetString(2),
                            Apellido = reader.GetString(3),
                            Tel = reader.GetString(4),
                            Email = reader.GetString(5),

                        };
                    }
                    connection.Close();
                }

                return p;
            }

        }

        public int Borrar(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Propietarios WHERE id = {id}";
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

        virtual public Propietario ObtenerPorId(int id)
        {
            Propietario p = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT id, dni,nombre, apellido,tel,email FROM Propietarios p  WHERE p.id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Dni = reader.GetString(1),
                            Nombre = reader.GetString(2),
                            Apellido = reader.GetString(3),
                            Tel = reader.GetString(4),
                            Email = reader.GetString(5),

                        };
                    }
                    connection.Close();
                }
                return p;

            }

        }


        public int Editar(Propietario propietario)
        {


            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Propietarios SET dni=@dni,nombre=@nombre," +
                    $"apellido=@apellido,tel=@tel,email=@email WHERE id=@id";


                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", propietario.Id);
                    command.Parameters.AddWithValue("@dni", propietario.Dni);
                    command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                    command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@tel", propietario.Tel);
                    command.Parameters.AddWithValue("@email", propietario.Email);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }


    }


}


