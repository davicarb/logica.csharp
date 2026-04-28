using System;
using System.ComponentModel;

namespace validador_fintech
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("insira seu nome: ");
      string nomeTitular = Console.ReadLine();
      decimal valorDeposito = RetornaValorDeposito();
      Conta novaConta = new Conta(nomeTitular, valorDeposito);

      if (valorDeposito > 0)
      {
        if (novaConta.DepositaValor(valorDeposito))
        {
          Console.WriteLine("depósito efetuado com sucesso.");
          Console.WriteLine($"saldo atual: R${novaConta.ValorSaldo}.");
        }
      }
      else
      {
        Console.WriteLine("valor inválido... depósito falhou.");
      }
    }

    static decimal RetornaValorDeposito()
    {
      int contadorTentativasInvalidas = 0;
      bool validValor = false;
      decimal decimalValido;

      do
      {
        Console.WriteLine("insira o valor que deseja depositar: ");
        validValor = decimal.TryParse(Console.ReadLine(), out decimalValido);

        if (!validValor || decimalValido <= 0)
        {
          Console.WriteLine("valor inserido é inválido ou negativo. impossível depositar valores negativos ou nada.");
          contadorTentativasInvalidas++;
        }
      } while (!validValor || decimalValido <= 0);

      Console.WriteLine($"quantidade de tentativas falhas no loop do while: {contadorTentativasInvalidas}");
      Console.WriteLine($"pressione qualquer tecla para continuar...");
      Console.ReadKey();

      return decimalValido;
    }
  }
}