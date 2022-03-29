using System.Data;
using Dapper;

namespace Ecommerce.Controllers
{
    public class Service
    {
        private readonly IRepository repository;

        public Service(IRepository repository)
        {
            this.repository = repository;
        }

        public CadastrarProdutoResult CadastrarProduto(Models.Produtos.CadastrarViewModel formulario)
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

            if (temErros)
            {
                return new CadastrarProdutoResult { CadastradoComSucesso = false };
            }

            repository.InserirProduto(formulario.Nome, formulario.Preco, formulario.Quantidade);

            return new CadastrarProdutoResult { CadastradoComSucesso = true };
        }
    }

    public class CadastrarProdutoResult
    {
        public bool CadastradoComSucesso { get; set; }
    }
}
