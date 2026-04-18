using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Net;
using Microsoft.VisualBasic;

namespace ex03simuladorsmart
{
  class Program
  {
    static void Main(string[] args)
    {
      var cartaoBasico = new CartaoCredito(700);

      bool rodando = true;

      while (rodando)
      {
        int opcaoMenu = MostraMenuRetornaValorMenu();

        switch (opcaoMenu)
        {
          case 1:
            decimal valorCompraValidado = RetornaValorCompraCorreto();
            cartaoBasico.RegistrarCompra(valorCompraValidado);
            break;

          case 2:
            decimal valorFaturaValidado = RetornaValorAPagarFaturaCorreto(cartaoBasico);
            cartaoBasico.PagarFatura(valorFaturaValidado);
            break;

          case 3:
            MostraLimite(cartaoBasico);
            break;

          case 0:
            Console.WriteLine("saindo...");
            rodando = false;
            break;
            
        }
      }
    }
    static int MostraMenuRetornaValorMenu()
    {
      Console.WriteLine("1. fazer uma compra");
      Console.WriteLine("2. pagar sua fatura");
      Console.WriteLine("3. ver seu limite atual");
      Console.WriteLine("0. sair");

      Console.WriteLine("insira a opcao desejada:");
      bool valid = int.TryParse(Console.ReadLine(), out int opcao);

      while (!valid || opcao > 3 || opcao < 0)
      {
        Console.WriteLine("entrada incorreta!");
        Console.WriteLine("tente novamente: ");
        valid = int.TryParse(Console.ReadLine(), out opcao);
      }

      return opcao;
    }
    static void MostraLimite(CartaoCredito cartaoBasico)
    {
      Console.WriteLine($"o limite atual do seu cartão básico é de: {cartaoBasico.LimiteCredito}");
    }

    static decimal RetornaValorCompraCorreto()
    {
      Console.WriteLine("insira o valor de sua compra:");
      bool valid = decimal.TryParse(Console.ReadLine(), out decimal valorCompraInput);

      while (!valid)
      {
        Console.WriteLine("entrada incorreta!");
        Console.WriteLine("tente novamente: ");
        valid = decimal.TryParse(Console.ReadLine(), out valorCompraInput);
      }

      return valorCompraInput;
    }

    static decimal RetornaValorAPagarFaturaCorreto(CartaoCredito cartaoBasico)
    {
      Console.WriteLine($"insira o valor que quer pagar de sua fatura no valor de {cartaoBasico.SaldoDevedor}:");
      bool valid = decimal.TryParse(Console.ReadLine(), out decimal valorPagFaturaInput);

      while (!valid || valorPagFaturaInput > cartaoBasico.SaldoDevedor)
      {
        Console.WriteLine("entrada incorreta ou valor maior que a fatura a ser paga!");
        Console.WriteLine("tente novamente: ");
        valid = decimal.TryParse(Console.ReadLine(), out valorPagFaturaInput);
      }

      return valorPagFaturaInput;
    }
  }
}