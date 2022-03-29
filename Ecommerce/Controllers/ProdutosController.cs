using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Produtos;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Data.Common;

namespace Ecommerce.Controllers
{
    //estrutura de dados e algoritmos
    //dominar bem uma linguagem de programação
    //SQL avançado
    //padrões de projeto -> diagrama
    //arquitetura de software

    public class ProdutosController : Controller
    {
        private IDbConnection conexao;
        private readonly IRepository repository;
        private readonly Service service;

        public ProdutosController(IDbConnection conexao, IRepository repository, Service service)
        {
            this.conexao = conexao;
            this.repository = repository;
            this.service = service;
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
            var resultado = service.CadastrarProduto(formulario);

            if (resultado.CadastradoComSucesso == true)
            {
                return RedirectToAction("listar");
            }

            return View(formulario);
        }

        [HttpGet]
        public ActionResult Listar(string nome)
        {
            ViewBag.Titulo = "Lista de Produtos";

            var resultado = repository.ListarProdutos(nome);

            return View(resultado);
        }

        [HttpGet]
        public ActionResult Remover(int id)
        {
            repository.RemoverProduto(id);

            return RedirectToAction("listar");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var produto = repository.BuscarProduto(id);

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
