using Dapper;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using WebApplicationDapper.Entities;
using Microsoft.Extensions.Configuration;

namespace WebApplicationDapper.Repository
{
    public interface IProdutosRepository
    {
        int Add(Produtos produto);
        int Edit(Produtos produto);
        int Delete(int id);
        Produtos Get(int id);
        List<Produtos> GetProdutos();
    }

    public class ProdutoRepository : IProdutosRepository
    {
        public readonly IConfiguration configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetConnection()
        {
            var connection =
                configuration.GetSection("ConnectionStrings").GetSection("DefaultConn").Value;

            return connection;
        }       

        public int Add(Produtos produto)
        {
            var connectionString = GetConnection();
            int count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = @"INSERT INTO Produtos(Nome, Estoque, Preco) 
                                VALUES
                                (@Nome, @Estoque, @Preco); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                    count = con.Execute(query, produto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }

        public int Delete(int id)
        {
            var connectionString = GetConnection();
            var count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = $"DELETE FROM Produtos WHERE ProdutoId = { id }";
                    count = con.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }

        public int Edit(Produtos produto)
        {
            var connectionString = GetConnection();
            var count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = $@"UPDATE Produtos SET 
                                Nome = @Nome, 
                                Estoque = @Estoque, 
                                Preco = @Preco WHERE ProdutoId = { produto.ProdutoId }";
                    count = con.Execute(query, produto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return count;
            }
        }

        public Produtos Get(int id)
        {
            var connectionString = GetConnection();
            Produtos produto = new Produtos();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = $"SELECT * FROM Produtos WHERE ProdutoId = { id }";
                    produto = con.Query<Produtos>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return produto;
            }
        }

        public List<Produtos> GetProdutos()
        {
            var connectionString = GetConnection();
            List<Produtos> produtos = new List<Produtos>();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Produtos";
                    produtos = con.Query<Produtos>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return produtos;
            }
        }        
    }
}
