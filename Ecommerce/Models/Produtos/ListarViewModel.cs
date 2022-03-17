namespace Ecommerce.Models.Produtos
{
    public class ListarViewModel
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }


        public class Produto
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public decimal Preco { get; set; }
            public int Quantidade { get; set; }
        }
    }
}
