using Microsoft.Data.Sqlite;

namespace sqlite_livros
{
  public class Livro
  {
    public string Titulo { get; set; }
    public string Autor { get; set; }

    public Livro(string tituloRecebido, string autorRecebido)
    {
      Titulo = tituloRecebido;
      Autor = autorRecebido;
    }
  }
}