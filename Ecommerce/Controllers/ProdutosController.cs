using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Produtos;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Ecommerce.Controllers
{
    //adicionar "remover" e "editar"
    //elaborar o enunciado do assessment

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
        public ActionResult Listar()
        {
            //abrir conexão
            //executar comando
            //fechar conexão

            IDbConnection conexao = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Ecommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); //1
            conexao.Open();                                                  //1
            var resultado = conexao.Query<Produto>("select * from PRODUTO"); //2
            conexao.Close();                                                 //3

            return View(resultado);
        }
    }
}
