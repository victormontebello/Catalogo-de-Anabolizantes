using domain.Entities;
using domain.Enums;
using domain.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorios
{
    public class AnabolizanteRepositorio : IAnabolizanteRepositorio
    {
        public void Delete(int id)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["BombasDB"].ConnectionString))
                {
                    conexao.Open();

                    var sqlQuery = "DELETE FROM Anabolizantes WHERE Id= @id";

                    using (SqlCommand comandoSQL = new SqlCommand(sqlQuery, conexao))
                    {
                        comandoSQL.Parameters.AddWithValue("@id", id);
                        comandoSQL.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar bomba" + ex.Message);
            }

        }

        public List<Anabolizante> GetAll()
        {
            var listaDeAnabolizantes = new List<Anabolizante>();
            var comando = "SELECT * FROM Anabolizantes";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BombasDB"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(comando, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Anabolizante bomba = new();
                        bomba.Id = Convert.ToInt32(dr["Id"]);
                        bomba.Nome = (string)dr["Nome"];
                        bomba.Preco = (double)dr["Preco"];
                        bomba.Vencimento = (DateTime)dr["Vencimento"];
                        bomba.Composicao = Enum.Parse<TipoBase>((dr["Composicao"].ToString()));
                        bomba.Injetavel = (bool)dr["Injetavel"];
                        listaDeAnabolizantes.Add(bomba);
                    }
                }
                return listaDeAnabolizantes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter bombas" + ex.Message);
                return null;
            }
        }

        public Anabolizante GetById(int id)
        {
            var bomba = new Anabolizante();
            try
            {
                using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["BombasDB"].ConnectionString))
                {
                    conexao.Open();
                    var sqlQuery = "SELECT * FROM Anabolizantes WHERE Id=@id";

                    SqlCommand comandoSQL = new SqlCommand(sqlQuery, conexao);
                    comandoSQL.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = comandoSQL.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bomba.Id = Convert.ToInt32(reader["Id"]);
                            bomba.Nome = (string)reader["Nome"];
                            bomba.Preco = (double)reader["Preco"];
                            bomba.Vencimento = (DateTime)reader["Vencimento"];
                            bomba.Composicao = Enum.Parse<TipoBase>((reader["Composicao"].ToString()));
                            bomba.Injetavel = (bool)reader["Injetavel"];
                        }
                    }
                }
                return bomba;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar bomba" + ex.Message);
                return null;
            }
        }

        public void Insert(Anabolizante al)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["BombasDB"].ConnectionString))
                {
                    conexao.Open();

                    var sqlQuery = "INSERT INTO Anabolizantes (Nome, Preco, Vencimento, Composicao, Injetavel) VALUES  (@Nome, @Preco, @Vencimento, @Composicao, @Injetavel)";

                    using (SqlCommand comandoSQL = new SqlCommand(sqlQuery, conexao))
                    {
                        comandoSQL.Parameters.AddWithValue("@Nome", al.Nome);
                        comandoSQL.Parameters.AddWithValue("@Preco", al.Preco);
                        comandoSQL.Parameters.AddWithValue("@Vencimento", al.Vencimento);
                        comandoSQL.Parameters.AddWithValue("@Composicao", al.Composicao.ToString());
                        comandoSQL.Parameters.AddWithValue("@Injetavel", al.Injetavel);
                        comandoSQL.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir bomba" + ex.Message);
            }
        }

        public void Update(Anabolizante al)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["BombasDB"].ConnectionString))
                {
                    conexao.Open();

                    var sqlQuery = "UPDATE Anabolizantes SET Nome = @Nome, Preco = @Preco, Composicao = @Composicao, Vencimento = @Vencimento, Injetavel = @Injetavel WHERE Id = @id";

                    using (SqlCommand comandoSQL = new SqlCommand(sqlQuery, conexao))
                    {
                        comandoSQL.Parameters.AddWithValue("@id", al.Id);
                        comandoSQL.Parameters.AddWithValue("@Nome", al.Nome);
                        comandoSQL.Parameters.AddWithValue("@Preco", al.Preco);
                        comandoSQL.Parameters.AddWithValue("@Vencimento", al.Vencimento);
                        comandoSQL.Parameters.AddWithValue("@Composicao", al.Composicao.ToString());
                        comandoSQL.Parameters.AddWithValue("@Injetavel", al.Injetavel);
                        comandoSQL.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar bomba" + ex.Message);
            }
        }
    }
}
