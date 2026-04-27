using Microsoft.Data.Sqlite;

namespace sqlite_livros
{
  class Program
  {
    static string connectionString = "Data Source=Biblioteca.db";
    static void Main(string[] args)
    {
      CriarTabela();

      bool rodando = true;

      while (rodando)
      {
        int opcaoMenuValidada = MostraMenuRetornaOpcaoMenu();

        switch (opcaoMenuValidada)
        {
          case 1:
            string tituloLivroValidado = RetornaTituloLivro();
            string autorLivroValidado = RetornaAutorLivro();

            Livro novoLivro = new Livro(tituloLivroValidado, autorLivroValidado);

            InsereLivroNoDb(novoLivro);
            Console.WriteLine("livro cadastrado com sucesso!");
            break;

          case 2: //deleta livro existente
            int idLivroValidadoExcluir = RetornaIdLivroParaExcluir();
            ExcluirLivro(idLivroValidadoExcluir);
            break;

          case 3: //lista livros
            ListarLivros();
            break;

          case 4:
            rodando = false;
            Console.WriteLine("saindo...");
            break;

        }
      }
    }
    static void InsereLivroNoDb(Livro novoLivro)
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();
      var command = connection.CreateCommand();
      command.CommandText = "INSERT INTO Livros (Titulo, Autor) VALUES ($titulo, $autor)";
      command.Parameters.AddWithValue("$titulo", novoLivro.Titulo);
      command.Parameters.AddWithValue("$autor", novoLivro.Autor);

      command.ExecuteNonQuery();
    }
    static string RetornaAutorLivro()
    {
      Console.WriteLine("insira o autor do livro que quer adicionar: ");
      string autorLivroInput = Console.ReadLine() ?? "";

      while (string.IsNullOrWhiteSpace(autorLivroInput) || autorLivroInput.Length < 4)
      {
        Console.WriteLine("autor do livro deve conter no mínimo 4 caracteres e não pode ser nulo...");
        Console.WriteLine("insira novamente: ");
        autorLivroInput = Console.ReadLine() ?? "";
      }

      return autorLivroInput;
    }
    static void CriarTabela()
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();
      var command = connection.CreateCommand();
      command.CommandText = "CREATE TABLE IF NOT EXISTS Livros (Id INTEGER PRIMARY KEY AUTOINCREMENT, Titulo TEXT, Autor TEXT)";
      command.ExecuteNonQuery();
    }

    static void ExcluirLivro(int idInformado)
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      using var command = connection.CreateCommand();

      // 1. Qual seria o CommandText aqui?
      command.CommandText = $"DELETE FROM Livros WHERE Id = $idInformado";
      // 2. Como você adiciona o parâmetro do ID?
      command.Parameters.AddWithValue("$idInformado", idInformado);
      // 3. Qual comando executa (NonQuery ou Reader)?
      command.ExecuteNonQuery();

      Console.WriteLine("livro excluído com sucesso!\n");

      Console.WriteLine("pressione qualquer tela para voltar ao menu...");
      Console.ReadKey();
    }
    static int MostraMenuRetornaOpcaoMenu()
    {
      Console.Clear();
      Console.WriteLine("--- sis gestao de livros sqlite ---\n");
      Console.WriteLine("1. cadastrar novo livro");
      Console.WriteLine("2. excluir livro existente");
      Console.WriteLine("3. listar livros"); ;
      Console.WriteLine("4. sair");

      bool validMenu = int.TryParse(Console.ReadLine(), out int opcaoValidada);

      while (!validMenu || opcaoValidada > 4 || opcaoValidada < 1)
      {
        Console.WriteLine("incorreto. insira novamente: ");
        validMenu = int.TryParse(Console.ReadLine(), out opcaoValidada);
      }

      return opcaoValidada;
    }

    static string RetornaTituloLivro()
    {
      Console.WriteLine("insira o título do livro que quer adicionar: ");
      string tituloLivroInput = Console.ReadLine() ?? "";

      while (string.IsNullOrWhiteSpace(tituloLivroInput) || tituloLivroInput.Length < 4)
      {
        Console.WriteLine("título do livro deve conter no mínimo 4 caracteres e não pode ser nulo...");
        Console.WriteLine("insira novamente: ");
        tituloLivroInput = Console.ReadLine() ?? "";
      }

      return tituloLivroInput;
    }


    static void ListarLivros()
    {
      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      using var command = connection.CreateCommand();
      command.CommandText = "SELECT Id, Titulo, Autor FROM Livros";

      // usamos ExecuteReader porque esperamos um retorno de dados (uma tabela)
      using var reader = command.ExecuteReader();

      Console.WriteLine("\n--- lista de livros cadastrados ---");

      // o reader.Read() tenta ir para a próxima linha. 
      // enquanto houver linhas para ler, ele retorna true.
      while (reader.Read())
      {
        // pegamos os dados pelas colunas (0 é Id, 1 é Titulo, 2 é Autor)
        int id = reader.GetInt32(0);
        string titulo = reader.GetString(1);
        string autor = reader.GetString(2);

        Console.WriteLine($"id: {id} | título: {titulo} | autor: {autor}\n");
      }

      Console.WriteLine("-----------------------------------\n");

      Console.WriteLine("pressione qualquer tela para voltar ao menu...");
      Console.ReadKey();
    }
    static int RetornaIdLivroParaExcluir()
    {
      Console.WriteLine("insira o id do livro que quer excluir: ");
      bool validIdLivro = int.TryParse(Console.ReadLine(), out int idLivro);

      while (!validIdLivro || idLivro < 1)
      {
        Console.WriteLine("id do livro deve ser acima de 1.");
        Console.WriteLine("insira novamente: ");
        validIdLivro = int.TryParse(Console.ReadLine(), out idLivro);
      }

      return idLivro;
    }
  }
}