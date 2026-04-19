using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ex04sis_gestao_biblio;

class Program
{
  static void Main(string[] args)
  {
    List<Livro> livros = new List<Livro>();

    if (File.Exists("livros.json"))
    {
      string conteudoJson = File.ReadAllText("livros.json");
      livros = JsonSerializer.Deserialize<List<Livro>>(conteudoJson);

    }
    bool rodando = true;

    while (rodando)
    {
      MostraMenu();
      int opcaoValidada = RetornaOpcaoMenuValidada();

      switch (opcaoValidada)
      {
        case 1:
          ListaLivros(livros);
          break;

        case 2:
          CadastraESalvaLivro(livros);
          break;

        case 3:
          Console.WriteLine("saindo...");
          rodando = false;
          break;
      }
    }

  }
  static int RetornaOpcaoMenuValidada()
  {
    bool valid = int.TryParse(Console.ReadLine(), out int opcao);
    while (!valid || opcao < 0 || opcao > 3)
    {
      Console.WriteLine("entrada invalida.");
      Console.WriteLine("insira novamente: ");
      valid = int.TryParse(Console.ReadLine(), out opcao);
    }

    return opcao;
  }
  static void MostraMenu()
  {
    Console.Clear();
    Console.WriteLine("1. listar livros guardados");
    Console.WriteLine("2. cadastrar novos livros");
    Console.WriteLine("3. sair");

    Console.WriteLine("insira a opção: ");
  }
  
  static void CadastraESalvaLivro(List<Livro> listaLivros)
  {
    Console.Clear();
    Livro novoLivro = new Livro();

    Console.WriteLine("insira o nome do livro que você quer cadastrar:");
    string nomeLivro = Console.ReadLine();
    novoLivro.Titulo = nomeLivro;

    Console.WriteLine("insira o nome do autor do livro:");
    string autorLivro = Console.ReadLine();
    novoLivro.Autor = autorLivro;

    listaLivros.Add(novoLivro);

    string jsonString = JsonSerializer.Serialize(listaLivros);
    File.WriteAllText("livros.json", jsonString);

    Console.WriteLine($"json gerado: {jsonString}");
  }

  static void ListaLivros(List<Livro> listaLivros)
  {
    Console.WriteLine("--- biblioteca json ---\n");

    if (listaLivros.Count > 0)
    {
      for (int i = 0; i < listaLivros.Count; i++)
      {
        Console.WriteLine($"Livro {i + 1}: {listaLivros[i].Titulo}\nAutor: {listaLivros[i].Autor}\n");
      }
    }

    else
    {
      Console.WriteLine("a biblioteca está vazia! adicione livros...");
    }
    
    Console.WriteLine("pressione qualquer tecla para voltar ao menu...");
    Console.ReadKey();
  }
}