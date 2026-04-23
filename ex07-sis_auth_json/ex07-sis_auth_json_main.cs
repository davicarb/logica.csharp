using System.Data;
using System.Text.Json;

namespace sis_auth
{
  class Program
  {
    static void Main(string[] args)
    {
      List<Usuario> listaUsuarios = new List<Usuario>();
      string jsonCarregarArquivo = "usuarios.json";

      if (File.Exists(jsonCarregarArquivo))
      {
        string lerJson = File.ReadAllText(jsonCarregarArquivo);
        if (!string.IsNullOrWhiteSpace(lerJson))
        {
          // caso arquivo estiver corrompido, se inicializa uma nova lista.
          listaUsuarios = JsonSerializer.Deserialize<List<Usuario>>(lerJson) ?? new List<Usuario>();
        }
      }

      bool rodando = true;

      while (rodando)
      {
        int opcao = MostraMenuRetornaOpcaoMenu();

        switch (opcao)
        {
          case 1:
            FazerCadastro(listaUsuarios);
            break;
          case 2:
            FazerLogin(listaUsuarios);
            break;
          case 3:
            Console.WriteLine("saindo...");
            rodando = false;
            break;
        }
      }
    }
    static int MostraMenuRetornaOpcaoMenu()
    {
      Console.Clear();
      Console.WriteLine("--- sis_auth_json ---\n");

      Console.WriteLine("1. criar conta");
      Console.WriteLine("2. fazer login");
      Console.WriteLine("3. sair");

      bool validMenu = int.TryParse(Console.ReadLine(), out int opcaoValidada);

      while (!validMenu || opcaoValidada > 3 || opcaoValidada < 1)
      {
        Console.WriteLine("incorreto. insira novamente: ");
        validMenu = int.TryParse(Console.ReadLine(), out opcaoValidada);
      }

      return opcaoValidada;
    }

    static void FazerLogin(List<Usuario> lista)
    {
      bool usuarioAutenticado = false;

      Console.Clear();
      Console.WriteLine("--- fazendo login no sis_auth ---\n");

      Console.WriteLine("insira o seu nome de usuário cadastrado: ");
      string nomeFornecidoNoLogin = Console.ReadLine();

      foreach (var usuario in lista)
      {
        if (usuario.NomeLogin == nomeFornecidoNoLogin)
        {

          Console.WriteLine($"conta encontrada: {usuario.NomeLogin}");
          Console.WriteLine($"insira sua senha: ");
          string senhaFornecidaNoLogin = Console.ReadLine();

          if (usuario.SenhaLogin == senhaFornecidaNoLogin)
          {
            Console.WriteLine("logado com sucesso!");
            Console.WriteLine($"data da criação: {usuario.DataCriacao}\n");
            usuarioAutenticado = true;

            Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
            Console.ReadKey();

            break;
          }
          else
          {
            Console.WriteLine("senha incorreta.\n");
            Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
            Console.ReadKey();
            break;
          }
        }
      }

      if (usuarioAutenticado == false)
      {
        Console.WriteLine("nome da conta não existente.\n");
        Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
        Console.ReadKey();
      }
    }

    static void FazerCadastro(List<Usuario> lista)
    {
      Console.Clear();
      Console.WriteLine("--- criando conta no sis_auth ---\n");

      Console.WriteLine("insira o seu nome de usuário (mínimo 5 caracteres): ");
      string nomeLoginFornecido = Console.ReadLine();

      while (nomeLoginFornecido.Length < 5)
      {
        Console.WriteLine("nome de usuário deve contar no mínimo 5 caracteres.");
        Console.WriteLine("tente novamente: ");
        nomeLoginFornecido = Console.ReadLine();
      }

      Console.WriteLine("crie sua senha (mínimo 7 caracteres): ");
      string senhaLoginFornecida = Console.ReadLine();


      while (senhaLoginFornecida.Length < 7)
      {
        Console.WriteLine("sua senha deve conter no mínimo 7 caracteres.");
        Console.WriteLine("tente novamente: ");
        senhaLoginFornecida = Console.ReadLine();
      }

      DateTime dataCriacao = DateTime.Now;

      Usuario novoUsuario = new Usuario(nomeLoginFornecido, senhaLoginFornecida, dataCriacao);
      lista.Add(novoUsuario);

      string filePath = "usuarios.json";
      string jsonString = JsonSerializer.Serialize(lista);
      File.WriteAllText(filePath, jsonString);

      Console.WriteLine("conta criada com sucesso!");
      Console.WriteLine($"Data da criação da sua conta: {novoUsuario.DataCriacao}");
      Console.WriteLine("pressione qualquer tecla para retornar ao menu...\n");
      Console.ReadKey();
    }
  }
}
