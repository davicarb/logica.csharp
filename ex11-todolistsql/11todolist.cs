using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace todolistsql

{
  class Program
  {
    static int _usuarioLogadoId;
    static void Main(string[] args)
    {
      string connectionString = "Data Source=usersdb.db";
      // inicializamos conexão com database local
      // criamos tabela no diretório do programa caso arquivo não exista ainda
      CriarTabelas(connectionString);

      bool rodandoMenuLogar = true;
      bool rodandoMenuCRUD = false;

      while (rodandoMenuLogar)
      {
        int opcaoMenuLogarValidado = MostraMenuRetornaOpcaoMenuLogin();

        switch (opcaoMenuLogarValidado)
        {
          case 1:
            CriaConta(connectionString);
            break;

          case 2:
            if (FazLogin(connectionString) && _usuarioLogadoId != 0)
            {
              rodandoMenuCRUD = true;
              Console.WriteLine("pressione qualquer tecla para iniciar o sistema de tarefas...");
              Console.ReadKey();
            }

            else
            {
              Console.WriteLine("falha no login.");
            }
            break;

          case 3:
            rodandoMenuLogar = false;
            Console.WriteLine("saindo...");
            break;
        }

        while (rodandoMenuCRUD && _usuarioLogadoId != 0)
        {
          int opcaoMenuCRUD = MostraMenuRetornaOpcaoMenuCRUD(_usuarioLogadoId);

          switch (opcaoMenuCRUD)
          {
            case 1:
              InsereNovaTarefa(connectionString, _usuarioLogadoId);
              break;

            case 2:
              LerTarefas(connectionString, _usuarioLogadoId);
              break;

            case 3:
              ExcluirTarefa(connectionString, _usuarioLogadoId);
              break;

            case 4:
              AtualizarStatusTarefa(connectionString, _usuarioLogadoId);
              break;
              
            case 5:
              rodandoMenuCRUD = false;
              break;
          }
        }
      }
    }
    static int MostraMenuRetornaOpcaoMenuLogin()
    {
      Console.Clear();
      Console.WriteLine("--- sqlite carb todolist ---\n");
      Console.WriteLine("1. criar conta");
      Console.WriteLine("2. fazer login");
      Console.WriteLine("3. sair");

      bool validMenu = int.TryParse(Console.ReadLine(), out int opcaoValidadaMenuLogin);

      while (!validMenu || opcaoValidadaMenuLogin > 3 || opcaoValidadaMenuLogin < 1)
      {
        Console.WriteLine("incorreto. insira novamente: ");
        validMenu = int.TryParse(Console.ReadLine(), out opcaoValidadaMenuLogin);
      }

      return opcaoValidadaMenuLogin;
    }

    static int MostraMenuRetornaOpcaoMenuCRUD(int _usuarioLogadoId)
    {
      Console.Clear();
      Console.WriteLine($"--- autenticado no id do usuário: {_usuarioLogadoId}: tarefas sqlite ---\n");
      Console.WriteLine("1. inserir nova tarefa");
      Console.WriteLine("2. ler tarefas existentes");
      Console.WriteLine("3. excluir tarefa existente");
      Console.WriteLine("4. atualizar status de tarefa existente");
      Console.WriteLine("5. sair da conta");

      bool validMenu = int.TryParse(Console.ReadLine(), out int opcaoValidada);

      while (!validMenu || opcaoValidada > 5 || opcaoValidada < 1)
      {
        Console.WriteLine("incorreto. insira novamente: ");
        validMenu = int.TryParse(Console.ReadLine(), out opcaoValidada);
      }

      return opcaoValidada;
    }
    static void CriaConta(string connectionString)
    {
      Console.WriteLine("insira o username que quer criar: ");
      string usernameInput = Console.ReadLine() ?? "";

      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var commandUsername = connection.CreateCommand();
      commandUsername.CommandText = "SELECT Username FROM Usuarios WHERE Username = $uDigitado";
      commandUsername.Parameters.AddWithValue("$uDigitado", usernameInput);

      using var reader = commandUsername.ExecuteReader();

      if (reader.Read())
      {
        Console.WriteLine("falha no cadastro: nome de usuário já existente.");
        Console.ReadKey();
      }

      else
      {
        Console.WriteLine("insira a senha que quer criar para sua conta: ");
        string senhaInput = Console.ReadLine() ?? "";

        var commandPassword = connection.CreateCommand();
        commandPassword.CommandText = "INSERT INTO Usuarios (Username, Senha) VALUES ($usernameDigitadoCriar, $senhaDigitadaCriar)";
        commandPassword.Parameters.AddWithValue("$usernameDigitadoCriar", usernameInput);
        commandPassword.Parameters.AddWithValue("$senhaDigitadaCriar", senhaInput);

        commandPassword.ExecuteNonQuery();

        Console.WriteLine("conta criada com sucesso!");
        Console.ReadKey();
      }
    }
    static bool FazLogin(string connectionString)
    {
      Console.WriteLine("insira o seu username: ");
      string inputUsername = Console.ReadLine() ?? "";

      Console.WriteLine("insira a sua senha: ");
      string inputSenha = Console.ReadLine() ?? "";

      using var connection = new SqliteConnection(connectionString);
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = "SELECT Id FROM Usuarios WHERE Username = $u AND Senha = $s";
      command.Parameters.AddWithValue("$u", inputUsername);
      command.Parameters.AddWithValue("$s", inputSenha);

      var result = command.ExecuteScalar();

      if (result != null)
      {
        _usuarioLogadoId = Convert.ToInt32(result);
        Console.WriteLine("login realizado com sucesso!");
        Console.ReadKey();
        return true;
      }
      else
      {
        Console.WriteLine("usuário ou senha inválidos.");
        Console.ReadKey();
        return false;
      }
    }
    static void CriarTabelas(string connectionString)
    {
      // primeiro passo: criamos a conexão com o banco de dados utilizando a connectionString declarada no Main.
      using var connection = new SqliteConnection(connectionString);
      // segundo passo: abrimos a conexão com o database.
      connection.Open();
      // terceiro passo: criamos o comando.
      var command = connection.CreateCommand();
      // quarto passo: damos o comando.
      command.CommandText = "CREATE TABLE IF NOT EXISTS Usuarios (Id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL UNIQUE, Senha TEXT NOT NULL)";
      command.ExecuteNonQuery();

      command.CommandText = "CREATE TABLE IF NOT EXISTS Tarefas (Id INTEGER PRIMARY KEY AUTOINCREMENT, Descricao TEXT NOT NULL, Concluida INTEGER DEFAULT 0, UsuarioId INTEGER, FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id))";
      command.ExecuteNonQuery();
    }

    static void InsereNovaTarefa(string connectionString, int _usuarioLogadoId)
    {
      using var connectionNovaTarefa = new SqliteConnection(connectionString);
      connectionNovaTarefa.Open();

      Console.WriteLine("insira o nome da tarefa que quer adicionar na sua lista: ");
      string tarefaInput = Console.ReadLine() ?? "";

      while (tarefaInput.Length < 5)
      {
        Console.WriteLine("tarefa deve conter no mínimo 5 caracteres. insira novamente: ");
        tarefaInput = Console.ReadLine() ?? "";
      }

      var commandCriarTarefa = connectionNovaTarefa.CreateCommand();
      commandCriarTarefa.CommandText = "INSERT INTO Tarefas (Descricao, UsuarioId) VALUES ($tarefa, $uId)";
      commandCriarTarefa.Parameters.AddWithValue("$tarefa", tarefaInput);
      commandCriarTarefa.Parameters.AddWithValue("$uId", _usuarioLogadoId);

      commandCriarTarefa.ExecuteNonQuery();
      Console.WriteLine("tarefa inserida com sucesso.");
      Console.WriteLine("pressione qualquer tecla para retornar ao menu...");
      Console.ReadKey();
    }

    static void LerTarefas(string connectionString, int _usuarioLogadoId)
    {
      using var connectionLerTarefas = new SqliteConnection(connectionString);
      connectionLerTarefas.Open();

      var commandLerTarefas = connectionLerTarefas.CreateCommand();
      commandLerTarefas.CommandText = "SELECT * FROM Tarefas WHERE UsuarioId = $uId";
      commandLerTarefas.Parameters.AddWithValue("$uId", _usuarioLogadoId);

      using var readerTarefas = commandLerTarefas.ExecuteReader();

      Console.WriteLine("\n--- lista de tarefas cadastradas ---");

      while (readerTarefas.Read())
      {
        int idTarefa = readerTarefas.GetInt32(0);
        string descricaoTarefa = readerTarefas.GetString(1);
        int isConcluida = readerTarefas.GetInt32(2);

        string concluidaOuNao;

        if (isConcluida == 0)
        {
          concluidaOuNao = "não.";
        }

        else
        {
          concluidaOuNao = "sim.";
        }

        Console.WriteLine($"id: {idTarefa}\ntítulo da tarefa: {descricaoTarefa}\nconcluída?: {concluidaOuNao}\n\n");
      }

      Console.WriteLine("-----------------------------------\n");

      Console.WriteLine("pressione qualquer tela para voltar ao menu...");
      Console.ReadKey();

    }
    static void ExcluirTarefa(string connectionString, int _usuarioLogadoId)
    {
      // algoritmo para pedir id para o usuario e validá-lo
      Console.WriteLine("insira o id da tarefa que quer excluir: ");

      bool validIdTarefa = int.TryParse(Console.ReadLine(), out int idTarefa);

      while (!validIdTarefa || idTarefa < 0)
      {
        Console.WriteLine("id da tarefa inválido.");
        Console.WriteLine("insira novamente: ");
        validIdTarefa = int.TryParse(Console.ReadLine(), out idTarefa);
      }

      using var connectionExcluirTarefas = new SqliteConnection(connectionString);
      connectionExcluirTarefas.Open();

      var commandLerTarefas = connectionExcluirTarefas.CreateCommand();



      // algoritmo sql de deletar tarefas
      commandLerTarefas.CommandText = "DELETE FROM Tarefas WHERE UsuarioId = $usuarioId AND Id = $tarefaId";
      commandLerTarefas.Parameters.AddWithValue("$usuarioId", _usuarioLogadoId);
      commandLerTarefas.Parameters.AddWithValue("$tarefaId", idTarefa);
      commandLerTarefas.ExecuteNonQuery();


      Console.WriteLine("tarefa excluída com sucesso.");
      Console.WriteLine("pressione qualquer tela para voltar ao menu...");
      Console.ReadKey();
    }
    static void AtualizarStatusTarefa(string connectionString, int _usuarioLogadoId)
    {
      // algoritmo para pedir id para o usuario e validá-lo
      Console.WriteLine("insira o id da tarefa que quer marcar como concluída: ");

      bool validIdTarefaAtualizar = int.TryParse(Console.ReadLine(), out int idTarefaAtualizar);

      while (!validIdTarefaAtualizar || idTarefaAtualizar < 1)
      {
        Console.WriteLine("id da tarefa deve ser acima de 1.");
        Console.WriteLine("insira novamente: ");
        validIdTarefaAtualizar = int.TryParse(Console.ReadLine(), out idTarefaAtualizar);
      }

      int boolSqlConcluida = 1;

      using var connectionAtualizarTarefas = new SqliteConnection(connectionString);
      connectionAtualizarTarefas.Open();

      var commandAtualizarTarefas = connectionAtualizarTarefas.CreateCommand();

      // algoritmo sql de atualizar status das tarefas
      commandAtualizarTarefas.CommandText = "UPDATE Tarefas SET Concluida = $status WHERE UsuarioId = $uId AND Id = $idTarefa";
      commandAtualizarTarefas.Parameters.AddWithValue("$uId", _usuarioLogadoId);
      commandAtualizarTarefas.Parameters.AddWithValue("$idTarefa", idTarefaAtualizar);
      commandAtualizarTarefas.Parameters.AddWithValue("$status", boolSqlConcluida);
      commandAtualizarTarefas.ExecuteNonQuery();

      Console.WriteLine("tarefa atualizada com sucesso.");
      Console.WriteLine("(agora marcada como concluída. caso queira checar, use o menu Listar Tarefas.)\n");
      Console.WriteLine("pressione qualquer tela para voltar ao menu...");
      Console.ReadKey();
    }

  }
}