namespace Ecommerce.Models.Produtos
{
    public class CadastrarViewModel
    {
        public string Nome { get; set; } = "";
        public string NomeError { get; set; }

        public decimal Preco { get; set; }
        public string PrecoErro { get; set; }

        public int Quantidade { get; set; }
        public string QuantidadeError { get; set; }
    }
}
