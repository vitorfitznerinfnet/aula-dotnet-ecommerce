using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Produtos;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Ecommerce.Controllers
{
    //falar sobre app stateless vs statefull
    //fazer refactoring

    //elaborar o enunciado do assessment
    //elabore um cadastro de clientes 
    //nome, email, cpf e data de cadastro
    //nome precisa ter apenas letras e maximo 200 caracteres
    //email com formato válido de e-mail
    //cpf valido também

    //remover código duplicado
    public class ProdutosController : Controller
    {
        private IDbConnection conexao;

        public ProdutosController(IDbConnection conexao)
        {
            this.conexao = conexao;
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var viewModel = new CadastrarViewModel();
            ViewBag.Titulo = "Cadastro de Produtos";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarViewModel formulario)
        {
            bool temErros = false;

            if (string.IsNullOrEmpty(formulario.Nome))
            {
                formulario.NomeError = "nome é obrigatório";
                temErros = true;
            }
            if (formulario.Nome.Length > 50)
            {
                formulario.NomeError = "nome não pode ter mais de 50 caracteres";
                temErros = true;
            }
            if (formulario.Quantidade < 0)
            {
                formulario.QuantidadeError = "quantidade não pode ser menor que zero";
                temErros = true;
            }
            if (formulario.Preco < 0)
            {
                formulario.PrecoErro = "preco não pode ser menor que zero";
                temErros = true;
            }

            ViewBag.MeuNome = "Vitor";

            if (temErros)
            {
                return View(formulario);
            }

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
            ViewBag.Titulo = "Lista de Produtos";

            conexao.Open();

            string sql = "select * from PRODUTO";

            if (nome != null)
            {
                sql += " where nome like '%@nome%'";
            }

            var resultado = conexao.Query<ListarViewModel.Produto>(sql, new { nome = nome}); 
            conexao.Close();                                                 

            return View(resultado);
        }

        [HttpGet]
        public ActionResult Remover(int id)
        {
            conexao.Open();
            conexao.Execute("delete from produto where id = @id", new { id = id });
            conexao.Close();

            return RedirectToAction("listar");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            conexao.Open();
            var produto = conexao.QuerySingle<ListarViewModel.Produto>("select * from produto where id = @id", new { id = id });
            conexao.Close();

            return View(produto);
        }

        [HttpPost]
        public ActionResult Editar(ListarViewModel.Produto produto)
        {
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
