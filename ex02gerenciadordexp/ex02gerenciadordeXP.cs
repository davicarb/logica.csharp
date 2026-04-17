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

      int pontosAtuaisArq = Arquitetura.PontosAtuais;
      Console.WriteLine($"Pontos atuais no módulo de Arquitetura: {pontosAtuaisArq}");
      Console.WriteLine("O valor de XP necessário para subir de nível no módulo de Arquitetura é de 100.");

      int valorGanho = RecebeValorGanho();
      AdicionarExperiencia(Arquitetura, valorGanho);
      
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
    static void AdicionarExperiencia(Modulo Arquitetura, int valorGanho)
    {
      Arquitetura.PontosAtuais = +valorGanho;
      int diferencaPontos = Arquitetura.PontosParaUparNivel - valorGanho;

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