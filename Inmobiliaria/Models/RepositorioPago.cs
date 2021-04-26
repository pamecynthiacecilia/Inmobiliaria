using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
	public class RepositorioPago : RepositorioBase

	{
		protected readonly IConfiguration configuration;
		protected readonly string connectionString;


		public RepositorioPago(IConfiguration configuration) : base(configuration)
		{
			this.configuration = configuration;
			connectionString = configuration["ConnectionsStrings:DefaultConnection"];
		}



		public List<Pago> ObtenerTodos()
		{
			var res = new List<Pago>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "SELECT p.id,nroPago, fecha,p.importe,idContrato, c.Id FROM Pagos p ,Contratos c Where c.id= p.idContrato";

				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						var e = new Pago
						{
							Id = reader.GetInt32(0),
							NroPago = reader.GetInt32(1),
							FechaP = reader.GetDateTime(2),
							Importe = reader.GetDecimal(3),
							IdContrato = reader.GetInt32(4),

							ContratoPago = new Contrato
							{
								Id = reader.GetInt32(4),
							
                            }
						};
						res.Add(e);
					}
					connection.Close();
				}

				return res;
			}

		}


					public int Agregar(Pago pago)
					{
						int res = -1;
						using (SqlConnection connection = new SqlConnection(connectionString))
						{
							string sql = $"INSERT INTO Pagos (nroPago,fecha,importe,idContrato)" +
								"VALUES (@nroPago,@fecha,@importe, @idContrato);" +
								"SELECT SCOPE_IDENTITY();";

							using (var command = new SqlCommand(sql, connection))
							{

								
								command.Parameters.AddWithValue("@nroPago", pago.NroPago);
								command.Parameters.AddWithValue("@fecha", pago.FechaP);
								command.Parameters.AddWithValue("@importe", pago.Importe);
								command.Parameters.AddWithValue("@idContrato", pago.IdContrato);
								connection.Open();

								res = Convert.ToInt32(command.ExecuteScalar());
								pago.Id = res;
								connection.Close();

							}
						}
						return res;
					}




					virtual public Pago ObtenerPorId(int id)
					{
						Pago e = null;
						using (SqlConnection connection = new SqlConnection(connectionString))
						{

				    string sql = $"SELECT p.id,nroPago,fecha,p.importe,idContrato, i.nombre, i.apellido, inm.tipo, inm.direccion" +
					$" FROM Pagos p , Contratos c ,Inquilinos i , Inmuebles inm  " +
					$"WHERE ((c.idInquilino= i.id) and (c.idInmueble = inm.id) and (p.idContrato= c.id) and (p.id= @id));";
								


					     using (SqlCommand command = new SqlCommand(sql, connection))
						{
								command.Parameters.Add("@id", SqlDbType.Int).Value = id;
								command.CommandType = CommandType.Text;
								connection.Open();
								var reader = command.ExecuteReader();

					if (reader.Read())
					{
						e = new Pago
						{
							Id = reader.GetInt32(0),
							NroPago = reader.GetInt32(1),
							FechaP = reader.GetDateTime(2),
							Importe = reader.GetDecimal(3),
							IdContrato = reader.GetInt32(4),


							ContratoPago = new Contrato
							{
								Id = reader.GetInt32(4),
								//IdInquilino = reader.GetInt32(5),
								//IdInmueble = reader.GetInt32(6),

								
								
								InquilinoContrato = new Inquilino
								{
								
									Nombre = reader.GetString(5),
									Apellido = reader.GetString(6),
								},
								InmuebleContrato = new Inmueble
								{
									//Id = reader.GetInt32(6),
									Tipo = reader.GetString(7),
									Direccion = reader.GetString(8),
								}
							}
									};
								}
								connection.Close();
							}
							return e;
						}

					}

			   public int Editar(Pago e)
	{
		int res = -1;
		using (SqlConnection connection = new SqlConnection(connectionString))
		{
			string sql = "UPDATE Pagos SET nroPago= @nroPago, fecha= @fecha, importe= @importe, idContrato= @idContrato " +
				"WHERE id = @id";

			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@id", e.Id);
				command.Parameters.AddWithValue("@nroPago", e.NroPago);
				command.Parameters.AddWithValue("@fecha", e.FechaP);
				command.Parameters.AddWithValue("@importe", e.Importe);
				command.Parameters.AddWithValue("@idContrato", e.IdContrato);
				



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

		
			public int Eliminar(int id)
			{
				int res = -1;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string sql = $"DELETE FROM Pagos WHERE id = {id}";
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

				
	}

}

