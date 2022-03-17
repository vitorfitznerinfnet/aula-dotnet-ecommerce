using static Ecommerce.Models.Produtos.ListarViewModel;

namespace Ecommerce.Models.Produtos
{
    public class EditarViewModel
    {
        public Produto Produto { get; set; }
        public List<string> Erros { get; set; }
    }
}
