namespace gestao_estoque_poo
{
  class Program
  {
    static void Main(string[] args)
    {
      var listaProdutos = new List<Produto>();

      Console.Clear();
      string nomeProdutoValidado = RetornaNomeProduto();
      decimal precoProdutoValidado = RetornaValorProduto(nomeProdutoValidado);
      int qtdProdutoValidado = RetornaQuantidadeProduto(nomeProdutoValidado, precoProdutoValidado);

      Produto produto1 = new Produto(nomeProdutoValidado, precoProdutoValidado, qtdProdutoValidado);
      listaProdutos.Add(produto1);

      Console.Clear();
      string nomeProdutoValidado2 = RetornaNomeProduto();
      decimal precoProdutoValidado2 = RetornaValorProduto(nomeProdutoValidado2);
      int qtdProdutoValidado2 = RetornaQuantidadeProduto(nomeProdutoValidado2, precoProdutoValidado2);

      Produto produto2 = new Produto(nomeProdutoValidado2, precoProdutoValidado2, qtdProdutoValidado2);
      listaProdutos.Add(produto2);

      Console.Clear();
      string nomeProdutoValidado3 = RetornaNomeProduto();
      decimal precoProdutoValidado3 = RetornaValorProduto(nomeProdutoValidado3);
      int qtdProdutoValidado3 = RetornaQuantidadeProduto(nomeProdutoValidado3, precoProdutoValidado3);

      Produto produto3 = new Produto(nomeProdutoValidado3, precoProdutoValidado3, qtdProdutoValidado3);

      listaProdutos.Add(produto3);

      decimal valorTotalProdutos = CalculaValorTotalProdutos(precoProdutoValidado, qtdProdutoValidado,
        precoProdutoValidado2, qtdProdutoValidado2,
        precoProdutoValidado3, qtdProdutoValidado3);

      ListaProdutosComEstoqueBaixo(nomeProdutoValidado, qtdProdutoValidado,
        nomeProdutoValidado2, qtdProdutoValidado2,
        nomeProdutoValidado3, qtdProdutoValidado3);
    }
    static void ListaProdutosComEstoqueBaixo(string nomeProdutoValidado, int qtdProdutoValidado,
      string nomeProdutoValidado2, int qtdProdutoValidado2,
      string nomeProdutoValidado3, int qtdProdutoValidado3)
    {
      if (qtdProdutoValidado < 5)
      {
        Console.WriteLine($"O produto {nomeProdutoValidado} está com estoque baixo! (abaixo de 5.).");
        Console.WriteLine($"Quantidade disponível no estoque: {qtdProdutoValidado}");
      }

      if (qtdProdutoValidado2 < 5)
      {
        Console.WriteLine($"O produto {nomeProdutoValidado2} está com estoque baixo! (abaixo de 5.).");
        Console.WriteLine($"Quantidade disponível no estoque: {qtdProdutoValidado2}");
      }

      if (qtdProdutoValidado3 < 5)
      {
        Console.WriteLine($"O produto {nomeProdutoValidado3} está com estoque baixo! (abaixo de 5.).");
        Console.WriteLine($"Quantidade disponível no estoque: {qtdProdutoValidado3}");
      }

      if (qtdProdutoValidado > 5 && qtdProdutoValidado2 > 5 && qtdProdutoValidado3 > 5)
      {
        Console.WriteLine("todos os produtos inseridos estão com estoque acima de 5!");
      }
    }
    static decimal CalculaValorTotalProdutos(decimal precoProdutoValidado, int qtdProdutoValidado,
      decimal precoProdutoValidado2, int qtdProdutoValidado2,
      decimal precoProdutoValidado3, int qtdProdutoValidado3)
    {
      decimal somaTotalProduto1 = precoProdutoValidado * qtdProdutoValidado;
      decimal somaTotalProduto2 = precoProdutoValidado2 * qtdProdutoValidado2;
      decimal somaTotalProduto3 = precoProdutoValidado3 * qtdProdutoValidado3;
      decimal somaTotalProdutos = somaTotalProduto1 + somaTotalProduto2 + somaTotalProduto3;

      Console.WriteLine($"o valor total de todos os produtos (incluindo suas respectivas quantidades) é de: R${somaTotalProdutos}");

      return somaTotalProdutos;
    }

    static string RetornaNomeProduto()
    {
      Console.WriteLine("insira o nome do produto que quer cadastrar:");
      string nomeProdutoInput = Console.ReadLine();

      while (nomeProdutoInput.Length < 5)
      {
        Console.WriteLine("produto deve conter no mínimo 5 caracteres. tente novamente: ");
        nomeProdutoInput = Console.ReadLine();
      }

      return nomeProdutoInput;
    }

    static decimal RetornaValorProduto(string nomeProduto)
    {
      Console.WriteLine($"insira o preço do produto {nomeProduto} que quer cadastrar:");
      bool validPrecoProduto = decimal.TryParse(Console.ReadLine(), out decimal precoProdutoInput);

      while (!validPrecoProduto || precoProdutoInput <= 0)
      {
        Console.WriteLine("preço inválido, nulo ou negativo.");
        Console.WriteLine("insira novamente: ");
        validPrecoProduto = decimal.TryParse(Console.ReadLine(), out precoProdutoInput);
      }

      return precoProdutoInput;

    }

    static int RetornaQuantidadeProduto(string nomeProduto, decimal precoProduto)
    {
      Console.WriteLine($"insira a quantidade disponível do produto {nomeProduto}, no valor de R${precoProduto}  que quer cadastrar:");
      bool validQtdProduto = int.TryParse(Console.ReadLine(), out int qtdProdutoInput);

      while (!validQtdProduto || qtdProdutoInput < 0)
      {
        Console.WriteLine("quantidade inválida, nula ou negativa.");
        Console.WriteLine("insira novamente: ");
        validQtdProduto = int.TryParse(Console.ReadLine(), out qtdProdutoInput);
      }

      return qtdProdutoInput;
    }

  }
}
