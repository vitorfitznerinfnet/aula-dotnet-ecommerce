using Ecommerce.Models.Produtos;
using System.Data;
using Dapper;

namespace Ecommerce.Controllers
{
    public class Repository : IRepository
    {
        public Repository(IDbConnection conexao)
        {
            this.conexao = conexao;
        }

        private IDbConnection conexao { get; }

        public ListarViewModel.Produto BuscarProduto(int id)
        {
            conexao.Open();
            var produto = conexao.QuerySingle<ListarViewModel.Produto>("select * from produto where id = @id", new { id = id });
            conexao.Close();
            return produto;
        }

        public IEnumerable<ListarViewModel.Produto> ListarProdutos(string nome)
        {
            conexao.Open();

            string sql = "select * from PRODUTO";

            if (nome != null)
            {
                sql += " where nome like '%@nome%'";
            }

            var resultado = conexao.Query<ListarViewModel.Produto>(sql, new { nome = nome });

            conexao.Close();

            return resultado;
        }

        public void RemoverProduto(int id)
        {
            conexao.Open();
            conexao.Execute("delete from produto where id = @id", new { id = id });
            conexao.Close();
        }

        public void InserirProduto(string nome, decimal preco, int quantidade)
        {
            conexao.Open();

            string sql = @" 
                insert into produto (nome, preco, quantidade)
                values (@nome, @preco, @quantidade)
                ";

            conexao.Execute(sql, new
            {
                nome = nome,
                preco = preco,
                quantidade = quantidade,
            });

            conexao.Close();
        }
    }
}
