using System;

namespace validador_fintech
{
  public class Conta
  {
    public string Titular {get; set;}
    public decimal ValorSaldo {get; private set;}

    public Conta(string titular, decimal valorSaldo)
    {
      Titular = titular;
      ValorSaldo = 0;
    }

    public bool DepositaValor(decimal valorDeposito)
    {
      ValorSaldo = ValorSaldo + valorDeposito;
      return true;
    }

  }

}