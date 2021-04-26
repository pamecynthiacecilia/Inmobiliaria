using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioContrato : RepositorioBase
    {

        protected readonly IConfiguration configuration;
        protected readonly string connectionString;


        public RepositorioContrato(IConfiguration configuration) : base(configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionsStrings:DefaultConnection"];
        }


        public List<Contrato> ObtenerTodos()
        {
            List<Contrato> res = new List<Contrato>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT c.id,fechaInicio,fechaFin,importe,idInquilino,idInmueble,i.apellido,i.nombre,inm.tipo,inm.direccion FROM Contratos c,Inmuebles inm,Inquilinos i WHERE (c.idInmueble = inm.id)AND(c.idInquilino = i.id)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new Contrato
                        {
                            Id = reader.GetInt32(0),
                            FechaInicio = reader.GetDateTime(1),
                            FechaFin = reader.GetDateTime(2),
                            Importe = reader.GetDecimal(3),
                            IdInmueble = reader.GetInt32(4),
                            IdInquilino = reader.GetInt32(5),



                            InmuebleContrato = new Inmueble
                            {
                                Id = reader.GetInt32(4),
                                Tipo = reader.GetString(6),
                                Direccion = reader.GetString(7),
                            },

                            InquilinoContrato = new Inquilino
                            {
                                Id = reader.GetInt32(5),
                                Apellido = reader.GetString(8),
                                Nombre = reader.GetString(9),

                            }
                        };
                        res.Add(e);
                    }
                    connection.Close();
                }
                return res;
            }
        }

        public int Agregar(Contrato c)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Contratos (fechaInicio,fechaFin,importe,idInmueble,idInquilino)" +
                    "VALUES (@fechaInicio,@fechaFin,@importe, @idInmueble, @idInquilino);" +
                    "SELECT SCOPE_IDENTITY();";
                using (var command = new SqlCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@fechaInicio", c.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", c.FechaFin);
                    command.Parameters.AddWithValue("@importe", c.Importe);
                    command.Parameters.AddWithValue("@idInmueble", c.IdInmueble);
                    command.Parameters.AddWithValue("@idInquilino", c.IdInquilino);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    c.Id = res;


                    connection.Close();
                }
            }
            return res;
        }


        virtual public Contrato ObtenerPorId(int id)
        {
            Contrato e = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT c.id,fechaInicio,fechaFin,importe,idInmueble,idInquilino,inm.tipo,inm.direccion,i.apellido,i.nombre" +
                    " FROM Contratos c, Inquilinos i, Inmuebles inm WHERE ((c.idInmueble = inm.id) and (c.idInquilino = i.id) and (c.id=@id));";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        e = new Contrato
                        {
                            Id = reader.GetInt32(0),
                            FechaInicio = reader.GetDateTime(1),
                            FechaFin = reader.GetDateTime(2),
                            Importe = reader.GetDecimal(3),
                            IdInmueble = reader.GetInt32(4),
                            IdInquilino = reader.GetInt32(5),


                            InmuebleContrato = new Inmueble
                            {
                                Id = reader.GetInt32(4),
                                Tipo = reader.GetString(6),
                                Direccion = reader.GetString(7),
                            },

                            InquilinoContrato = new Inquilino
                            {
                                Id = reader.GetInt32(5),
                                Apellido = reader.GetString(8),
                                Nombre = reader.GetString(9),

                            }
                        };

                    }
                    connection.Close();
                }
                return e;
            }

        }

        public int Editar(Contrato e)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Contratos SET fechaInicio= @fechaInicio, fechaFin= @fechaFin, importe= @importe, idInmueble= @idInmueble, idInquilino=@idInquilino " +
                    "WHERE id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", e.Id);
                    command.Parameters.AddWithValue("@fechaInicio", e.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", e.FechaFin);
                    command.Parameters.AddWithValue("@importe", e.Importe);
                    command.Parameters.AddWithValue("@idInmueble", e.IdInmueble);
                    command.Parameters.AddWithValue("@idInquilino", e.IdInquilino);



                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }


        public int Eliminar(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Contratos WHERE id = {id}";
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

        public int ObtenerId(int id)
        {
            int nroId = id;
            return nroId;
        }
    }
}

           
                   