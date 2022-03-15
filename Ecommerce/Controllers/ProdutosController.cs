using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Produtos;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Ecommerce.Controllers
{
    //dar exemplo de 1 validação

    //elaborar o enunciado do assessment
    //elabore um cadastro de clientes 
    //nome, email, cpf e data de cadastro
    //nome precisa ter apenas letras e maximo 200 caracteres
    //email com formato válido de e-mail
    //cpf valido também

    public class ProdutosController : Controller
    {
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(DadosDeCadastroDeProduto formulario)
        {
            //abrir conexão //1
            //comando de insert //2 
            //fechar conexão //3

            IDbConnection conexao = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Ecommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); //1
            conexao.Open();                                //1

            string sql = @" 
                insert into produto (nome, preco, quantidade)
                values (@nome, @preco, @quantidade)
                ";                                         //2
            conexao.Execute(sql, new {
                nome = formulario.Nome,
                preco = formulario.Preco,
                quantidade = formulario.Quantidade,
            });                                            //2
            conexao.Close();                               //3

            return RedirectToAction("listar");
        }

        [HttpGet]
        public ActionResult Listar(string nome)
        {
            IDbConnection conexao = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Ecommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); //1
            conexao.Open();

            string sql = "select * from PRODUTO";

            if (nome != null)
            {
                sql += " where nome like '%@nome%'";
            }

            var resultado = conexao.Query<Produto>(sql, new { nome = nome}); 
            conexao.Close();                                                 

            return View(resultado);
        }

        [HttpGet]
        public ActionResult Remover(int id)
        {
            IDbConnection conexao = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Ecommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); //1
            conexao.Open();
            conexao.Execute("delete from produto where id = @id", new { id = id });
            conexao.Close();

            return RedirectToAction("listar");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            IDbConnection conexao = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Ecommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); //1
            conexao.Open();
            var produto = conexao.QuerySingle<Produto>("select * from produto where id = @id", new { id = id });
            conexao.Close();

            return View(produto);
        }

        [HttpPost]
        public ActionResult Editar(Produto produto)
        {
            IDbConnection conexao = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Ecommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); //1
            conexao.Open();

            string sql = @"
                update produto set 
                    nome = @nome,
                    preco = @preco,
                    quantidade = @quantidade
                where id = @id
                ";

            conexao.Execute(sql, new
            {
                nome = produto.Nome,
                preco = produto.Preco,
                quantidade = produto.Quantidade,
                id = produto.Id
            });

            conexao.Close();

            return RedirectToAction("listar");
        }
    }
}
