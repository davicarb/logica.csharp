namespace banco_legacy
{
  class Program
  {
    static void Main(string[] args)
    {
      string nomeInputRetornado = RetornaNomeTitular();
      int idContaGerado = RetornaIdTitular();
      decimal saldo = 0;

      ContaBancaria novaConta = new ContaBancaria(nomeInputRetornado, idContaGerado, saldo);
      Console.WriteLine("conta criada com sucesso!");
      Console.WriteLine("pressione qualquer tecla para iniciar o sistema do banco...");
      Console.ReadKey();
      
      bool rodando = true;

      while (rodando)
      {
        int opcaoMenuValidada = MostraMenuRetornaOpcaoMenu();


        switch (opcaoMenuValidada)
        {
          case 1:
            decimal valorDepositoValidado = RetornaValorDeDeposito(idContaGerado);

            if (novaConta.Depositar(valorDepositoValidado) == true)
            {
              Console.WriteLine($"deposito no valor de {valorDepositoValidado} feito com sucesso.");
              Console.WriteLine($"saldo após depósito: {novaConta.Saldo}");
              Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
              Console.ReadKey();
            }
            break;

          case 2:
            decimal valorSaqueValidado = RetornaValorDeSaque(idContaGerado);

            if (novaConta.Sacar(valorSaqueValidado) == true)
            {
              Console.WriteLine($"saque efetuado no valor de {valorSaqueValidado}");
              Console.WriteLine($"saldo após saque: {novaConta.Saldo}");
              Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
              Console.ReadKey();
            }

            else
            {
              Console.WriteLine("impossível efetuar saque. saldo insuficiente.");
              Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
              Console.ReadKey();
            }
            break;

          case 3:
            VerSaldoAtual(idContaGerado, novaConta);
            break;

          case 4:
            Console.WriteLine($"nome do titular da conta: {novaConta.Titular}");
            Console.WriteLine($"id da conta: {novaConta.IdTitular}");

            Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
            Console.ReadKey();

            break;

          case 5:
            Console.WriteLine("saindo...");
            rodando = false;
            break;
        }
      }

    }

    static int MostraMenuRetornaOpcaoMenu()
    {
      Console.Clear();
      Console.WriteLine("--- exercicio banco 2.0 ---\n");
      Console.WriteLine("1. efetuar deposito");
      Console.WriteLine("2. efetuar saque");
      Console.WriteLine("3. consultar saldo atual");
      Console.WriteLine("4. ver dados da sua conta");
      Console.WriteLine("5. sair");

      bool validMenu = int.TryParse(Console.ReadLine(), out int opcaoValidada);

      while (!validMenu || opcaoValidada > 5 || opcaoValidada < 1)
      {
        Console.WriteLine("incorreto. insira novamente: ");
        validMenu = int.TryParse(Console.ReadLine(), out opcaoValidada);
      }

      return opcaoValidada;
    }
    static decimal RetornaValorDeDeposito(int idContaGeradoParaDeposito)
    {
      Console.WriteLine($"insira o valor que quer depositar na conta {idContaGeradoParaDeposito}");

      bool validDeposito = decimal.TryParse(Console.ReadLine(), out decimal valorDeposito);

      while (!validDeposito || valorDeposito < 0)
      {
        Console.WriteLine("incorreto. impossível depositar valores negativos. insira novamente: ");
        validDeposito = decimal.TryParse(Console.ReadLine(), out valorDeposito);
      }

      return valorDeposito;
    }
    static decimal RetornaValorDeSaque(int idContaGeradoParaDeposito)
    {
      Console.WriteLine($"insira o valor que quer sacar da conta {idContaGeradoParaDeposito}");
      bool validSaque = decimal.TryParse(Console.ReadLine(), out decimal valorSaque);

      while (!validSaque || valorSaque < 0)
      {
        Console.WriteLine("incorreto. impossível sacar valores negativos. tente novamente: ");
        validSaque = decimal.TryParse(Console.ReadLine(), out valorSaque);
      }

      return valorSaque;
    }

    static void VerSaldoAtual(int idContaParaVerSaldo, ContaBancaria novaConta_versaldo)
    {
      Console.WriteLine($"o saldo da conta nº{idContaParaVerSaldo} é de R${novaConta_versaldo.Saldo}");
      Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
      Console.ReadKey();
    }
    static string RetornaNomeTitular()
    {
      Console.WriteLine("insira seu nome: ");
      string nomeInput = Console.ReadLine() ?? "Nome não informado";

      return nomeInput;
    }

    static int RetornaIdTitular()
    {
      Random random = new Random();
      int idAleatorio = random.Next(10000, 100000);

      Console.WriteLine($"o id gerado para sua conta é: {idAleatorio} ");
      return idAleatorio;
    }
  }
}