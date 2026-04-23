using System.Text.Json;

namespace banco_legacy
{
  class Program
  {
    static void Main(string[] args)
    {
      List<ContaBancaria> listaContas = new List<ContaBancaria>();

      string jsonPath = "contas.json";

      if (File.Exists(jsonPath))
      {
        string lerJson = File.ReadAllText(jsonPath);
        listaContas = JsonSerializer.Deserialize<List<ContaBancaria>>(lerJson);
        Console.WriteLine("dev message: json carregado com sucesso...");
        Thread.Sleep(2000);
      }

      else
      {
        listaContas = new List<ContaBancaria>();
        Console.WriteLine("dev message: banco de dados novo iniciado...");
        Thread.Sleep(2000);
      }


      bool menuLogin = true;
      bool menuAutenticado = false;
      ContaBancaria? contaLogada = null;

      while (menuLogin)
      {
        int opcaoMenuNaoAutenticado = MostraMenuRetornaOpcaoMenuNaoAutenticado();

        switch (opcaoMenuNaoAutenticado)
        {
          case 1:
            FazerCadastro(listaContas);
            break;

          case 2:
            if (FazerLogin(listaContas, out ContaBancaria? logada))
            {
              menuAutenticado = true;
              contaLogada = logada;
              Console.WriteLine("\nlogin feito com sucesso!");
              Console.WriteLine("pressione qualquer tecla para iniciar o sistema do banco...");
              Console.ReadKey();
            }
            else
            {
              Console.WriteLine("Falha no login.");
            }
            break;
          case 3:
            Console.WriteLine("saindo do banco...");
            menuLogin = false;
            break;
        }

        while (menuAutenticado && contaLogada != null)
        {

          int opcaoMenuAutenticado = MostraMenuRetornaOpcaoMenuAutenticado();

          switch (opcaoMenuAutenticado)
          {
            case 1:
              decimal valorDepositoValidado = RetornaValorDeDeposito(contaLogada);

              if (contaLogada.Depositar(valorDepositoValidado) == true)
              {
                Console.WriteLine($"deposito no valor de {valorDepositoValidado} feito com sucesso.");
                Console.WriteLine($"saldo na conta {contaLogada.IdTitular} após depósito: {contaLogada.Saldo}");
                Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
                Console.ReadKey();
              }
              break;

            case 2:
              decimal valorSaqueValidado = RetornaValorDeSaque(contaLogada);

              if (contaLogada.Sacar(valorSaqueValidado) == true)
              {
                Console.WriteLine($"saque efetuado no valor de {valorSaqueValidado}");
                Console.WriteLine($"saldo após saque: {contaLogada.Saldo}");
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
              VerSaldoAtual(contaLogada);
              break;

            case 4:
              Console.WriteLine($"nome do titular da conta atualmente logada: {contaLogada.Titular}");
              Console.WriteLine($"e-mail da conta: {contaLogada.Email}");

              Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
              Console.ReadKey();

              break;

            case 5:
              Console.WriteLine("saindo...");
              menuAutenticado = false;
              break;
          }
        }
      }


    }
    static void FazerCadastro(List<ContaBancaria> lista)
    {
      Console.Clear();
      Console.WriteLine("--- criando conta no banco legacy ---\n");

      Console.WriteLine("insira seu nome (mínimo 5 caracteres): ");
      string? nomeTitularRecebido = Console.ReadLine();

      while (nomeTitularRecebido.Length < 5)
      {
        Console.WriteLine("email deve conter no mínimo 5 caracteres.");
        Console.WriteLine("tente novamente: ");
        nomeTitularRecebido = Console.ReadLine();
      }

      Console.WriteLine("insira seu e-mail (mínimo 5 caracteres): ");
      string? emailRecebido = Console.ReadLine();

      while (emailRecebido.Length < 5)
      {
        Console.WriteLine("email deve conter no mínimo 5 caracteres.");
        Console.WriteLine("tente novamente: ");
        emailRecebido = Console.ReadLine();
      }

      Console.WriteLine("crie uma senha forte (mínimo 7 caracteres): ");
      string? senhaContaFornecida = Console.ReadLine();

      while (senhaContaFornecida.Length < 7)
      {
        Console.WriteLine("sua senha deve conter no mínimo 7 caracteres.");
        Console.WriteLine("tente novamente: ");
        senhaContaFornecida = Console.ReadLine();
      }

      Random random = new Random();
      int idAleatorioNovaConta = random.Next(10000, 100000);

      Console.WriteLine($"o id gerado para sua conta é: {idAleatorioNovaConta} ");

      decimal saldo = 0;

      ContaBancaria novaConta = new ContaBancaria(nomeTitularRecebido, emailRecebido, senhaContaFornecida, idAleatorioNovaConta, saldo);
      lista.Add(novaConta);

      string filePath = "contas.json";
      string jsonStringNovaConta = JsonSerializer.Serialize(lista);
      File.WriteAllText(filePath, jsonStringNovaConta);

      Console.WriteLine("conta criada com sucesso!");
      Console.WriteLine("pressione qualquer tecla para retornar ao menu...\n");
      Console.ReadKey();
    }

    static bool FazerLogin(List<ContaBancaria> listaContas, out ContaBancaria? contaLogada)
    {
      contaLogada = null;
      bool usuarioAutenticado = false;

      Console.Clear();
      Console.WriteLine("--- fazendo login no banco legacy ---\n");

      Console.WriteLine("insira o seu e-mail cadastrado: ");
      string emailFornecidoNoLogin = Console.ReadLine();

      foreach (var conta in listaContas)
      {
        if (conta.Email == emailFornecidoNoLogin)
        {

          Console.WriteLine($"conta encontrada: {conta.Titular}");
          Console.WriteLine($"insira sua senha: ");
          string senhaFornecidaNoLogin = Console.ReadLine();

          if (conta.Senha == senhaFornecidaNoLogin)
          {
            Console.WriteLine("logado com sucesso!");
            Console.WriteLine($"id da conta logada: {conta.IdTitular} ");

            contaLogada = conta;
            usuarioAutenticado = true;
            return true;
          }
          else
          {
            Console.WriteLine("senha incorreta.\n");
            Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
            Console.ReadKey();

            return false;
          }
        }
      }
      if (usuarioAutenticado == false)
      {
        Console.WriteLine("nome da conta não existente.\n");
        Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
        Console.ReadKey();

        return false;
      }

      return false;
    }

    static int MostraMenuRetornaOpcaoMenuAutenticado()
    {
      Console.Clear();
      Console.WriteLine("--- autenticado: banco legacy 2.0 ---\n");
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
    static int MostraMenuRetornaOpcaoMenuNaoAutenticado()
    {
      Console.Clear();
      Console.WriteLine("--- banco legacy 2.0 ---\n");
      Console.WriteLine("1. criar conta");
      Console.WriteLine("2. fazer login");
      Console.WriteLine("3. sair");

      bool validMenu = int.TryParse(Console.ReadLine(), out int opcaoValidadaMenuNaoAutenticado);

      while (!validMenu || opcaoValidadaMenuNaoAutenticado > 3 || opcaoValidadaMenuNaoAutenticado < 1)
      {
        Console.WriteLine("incorreto. insira novamente: ");
        validMenu = int.TryParse(Console.ReadLine(), out opcaoValidadaMenuNaoAutenticado);
      }

      return opcaoValidadaMenuNaoAutenticado;
    }
    static decimal RetornaValorDeDeposito(ContaBancaria contaDeposito)
    {
      Console.WriteLine($"insira o valor que quer depositar na conta {contaDeposito.IdTitular}, de {contaDeposito.Titular}");

      bool validDeposito = decimal.TryParse(Console.ReadLine(), out decimal valorDeposito);

      while (!validDeposito || valorDeposito < 0)
      {
        Console.WriteLine("incorreto. impossível depositar valores negativos. insira novamente: ");
        validDeposito = decimal.TryParse(Console.ReadLine(), out valorDeposito);
      }

      return valorDeposito;
    }
    static decimal RetornaValorDeSaque(ContaBancaria contaSaque)
    {
      Console.WriteLine($"insira o valor que quer sacar da conta {contaSaque.IdTitular}, de {contaSaque.Titular}");
      bool validSaque = decimal.TryParse(Console.ReadLine(), out decimal valorSaque);

      while (!validSaque || valorSaque < 0)
      {
        Console.WriteLine("incorreto. impossível sacar valores negativos. tente novamente: ");
        validSaque = decimal.TryParse(Console.ReadLine(), out valorSaque);
      }

      return valorSaque;
    }

    static void VerSaldoAtual(ContaBancaria conta)
    {
      Console.WriteLine($"o saldo da conta nº{conta.IdTitular} é de R${conta.Saldo}");
      Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
      Console.ReadKey();
    }
  }
}