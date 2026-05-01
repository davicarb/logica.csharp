using System;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;

namespace ContactListSQLite
{
  class Program
  {
    static void Main(string[] args)
    {
      string connectionString = "Data source=contatos.db";
      CriaTabela(connectionString);
      AddContact(connectionString);
      ListContacts(connectionString);
      DeleteContact(connectionString);
    }

    static void CriaTabela(string connectionString)
    {
      using var connectionCreateTable = new SqliteConnection(connectionString);
      connectionCreateTable.Open();

      var commandCreateTable = connectionCreateTable.CreateCommand();
      commandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS Contatos (Id INTEGER PRIMARY KEY AUTOINCREMENT, Nome TEXT NOT NULL, Telefone TEXT NOT NULL)";
      commandCreateTable.ExecuteNonQuery();
    }

    static void AddContact(string connectionString)
    {
      using var connectionAddContact = new SqliteConnection(connectionString);
      connectionAddContact.Open();

      var commandAdd = connectionAddContact.CreateCommand();

      Console.WriteLine("insira o nome do contato que quer adicionar: ");
      string nomeContato = Console.ReadLine() ?? "";

      while (nomeContato.Length < 3)
      {
        Console.WriteLine("insira um nome maior ou válido");
        nomeContato = Console.ReadLine() ?? "";
      }

      Console.WriteLine("insira o nº do contato que quer adicionar: ");
      string numeroContato = Console.ReadLine() ?? "";

      while (numeroContato.Length < 10)
      {
        Console.WriteLine("insira numero maior ou válido");
        numeroContato = Console.ReadLine() ?? "";
      }

      commandAdd.CommandText = "INSERT INTO Contatos (Nome, Telefone) VALUES ($nome, $tel)";
      commandAdd.Parameters.AddWithValue("$nome", nomeContato);
      commandAdd.Parameters.AddWithValue("$tel", numeroContato);
      commandAdd.ExecuteNonQuery();
    }

    static void ListContacts(string connectionString)
    {
      using var connectionList = new SqliteConnection(connectionString);
      connectionList.Open();

      var commandList = connectionList.CreateCommand();
      commandList.CommandText = "SELECT * FROM Contatos";
      using var reader = commandList.ExecuteReader();

      while (reader.Read())
      {
        int idLido = reader.GetInt32(0);
        string nomeLido = reader.GetString(1);
        string telLido = reader.GetString(2);

        Console.WriteLine($"id do contato: {idLido}\nnome do contato: {nomeLido}\ntelefone: {telLido}\n\n");
      }

      Console.WriteLine("-----------------------------------\n");

      Console.WriteLine("pressione qualquer tela para voltar ao menu...");
      Console.ReadKey();
    }

    static void DeleteContact(string connectionString)
    {
      using var connectionDelete = new SqliteConnection(connectionString);
      connectionDelete.Open();

      var commandDelete = connectionDelete.CreateCommand();

      Console.WriteLine("insira o ID do contato que quer excluir:");

      bool validId = int.TryParse(Console.ReadLine(), out int id);

      while (!validId || id < 0)
      {
        Console.WriteLine("id da tarefa inválido.");
        Console.WriteLine("insira novamente: ");
        validId = int.TryParse(Console.ReadLine(), out id);
      }

      commandDelete.CommandText = "DELETE FROM Contatos WHERE Id = $id";
      commandDelete.Parameters.AddWithValue("$id", id);
      commandDelete.ExecuteNonQuery();
    }
  }
}