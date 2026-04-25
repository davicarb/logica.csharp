namespace gestao_estoque_poo
{
  public class Produto
  {
    public string Nome;
    public decimal Preco;
    public int Quantidade;

    public Produto (string nomeProduto, decimal precoProduto, int quantidadeProduto)
    {
      Nome = nomeProduto;
      Preco = precoProduto;
      Quantidade = quantidadeProduto;
    }
  }
}