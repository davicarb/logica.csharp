using System;
using Microsoft.Data.Sqlite;

namespace todolistsql

{
  public class Usuario
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Senha { get; private set; }

    // construtor do usuario
    public Usuario(int id, string username, string senha)
    {
      Id = id;
      Username = username;
      Senha = senha;
    }
  }
}
