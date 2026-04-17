using System;
using System.Diagnostics.Contracts;
using System.Net;

namespace ex02gerenciadordeXP
{

  class Program
  {
    static void Main(string[] args)
    {
      Modulo Arquitetura = new Modulo
      {
       nome = "Arquitetura",
       PontosAtuais = 0,
       PontosParaUparNivel = 100
      };
      
      bool rodando = true;
      Console.Clear()

      while (rodando)
      {
      Console.WriteLine($"Pontos atuais no módulo de Arquitetura: {Arquitetura.PontosAtuais}");
      Console.WriteLine($"O valor de XP necessário para subir de nível no módulo de Arquitetura é de {Arquitetura.PontosParaUparNivel}.");

      int valorGanho = RecebeValorGanho();

      AdicionarValorGanho(Arquitetura, valorGanho);

      Console.WriteLine("\nRepetir?");
      Console.WriteLine("1 = s");
      Console.WriteLine("0 = n\n");

      bool valid = int.TryParse(Console.ReadLine(), out int option);

      while (!valid || (option != 0 && option != 1))
        {
          Console.WriteLine("Entrada incorreta.\n");
          Console.WriteLine("Insira novamente: ");
          valid = int.TryParse(Console.ReadLine(), out option);
        }      
      
      if (option == 1)
        {
          rodando = true;
        }
      else
        {
          rodando = false;
          break;
        }
      }

    }
    static int RecebeValorGanho()
    {

      Console.WriteLine("Insira o valor de xp ganho: ");
      bool valid = int.TryParse(Console.ReadLine(), out int valorGanho);

      while (!valid || valorGanho < 0)
      {
        Console.WriteLine("Valor inválido. Somente números positivos. Insira novamente: ");
        valid = int.TryParse(Console.ReadLine(), out valorGanho);
      }

      return valorGanho;
    }
    static void AdicionarValorGanho(Modulo Arquitetura, int valorGanho)
    {
      Arquitetura.PontosAtuais = Arquitetura.PontosAtuais + valorGanho;
      int diferencaPontos = Arquitetura.PontosParaUparNivel - Arquitetura.PontosAtuais;

      if (Arquitetura.PontosAtuais < 100)
      {
        Console.WriteLine("Hmm... você ainda não passou de nível.\n");
        Console.WriteLine("É necessário mais XP!\n");
        Console.WriteLine($"O XP necessário é de: {diferencaPontos} !");
      }

      else
      {
        Console.WriteLine("Parabéns! Você subiu de nível!");
        Arquitetura.PontosAtuais = 0;
      }
    }
  }
}