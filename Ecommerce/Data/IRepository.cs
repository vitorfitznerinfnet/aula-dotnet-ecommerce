using Ecommerce.Models.Produtos;

namespace Ecommerce.Controllers
{
    public interface IRepository
    {
        IEnumerable<ListarViewModel.Produto> ListarProdutos(string nome);
        void RemoverProduto(int id);
        ListarViewModel.Produto BuscarProduto(int id);
        void InserirProduto(string nome, decimal preco, int quantidade);
    }
}
