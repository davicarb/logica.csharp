using System;
using System.Diagnostics.Contracts;
using System.Net;

namespace ex03simuladorsmart
{
  public class CartaoCredito
  {
    public decimal LimiteCredito { get; private set; }
    public decimal SaldoDevedor { get; private set; }

    // public decimal SisLimiteDinamico;

    public CartaoCredito(decimal limiteInicial)
    {
      LimiteCredito = limiteInicial;
    }

    public void PagarFatura(decimal valorAPagar)
    {
      Console.WriteLine($"seu saldo devedor atual é de: R${this.SaldoDevedor}");
      Console.WriteLine("efetuando pagamento da fatura...");

      if (valorAPagar == SaldoDevedor)
      {
        LimiteCredito = LimiteCredito + valorAPagar;
        SaldoDevedor = SaldoDevedor - valorAPagar;

        Console.WriteLine("fatura paga com sucesso!");
        Console.WriteLine("bonus de confiança de 15% de aumento ao limite");
        Console.WriteLine("concedido por pagamento ser feito no valor total!");

        this.LimiteCredito = this.LimiteCredito + (this.LimiteCredito * 0.15m);
        Console.WriteLine($"seu limite agora é de: {this.LimiteCredito}");
      }

      else
      {
        LimiteCredito = LimiteCredito + valorAPagar;
        SaldoDevedor = SaldoDevedor - valorAPagar;

        Console.WriteLine($"fatura no valor de R${this.SaldoDevedor} paga!");
        Console.WriteLine($"seu limite agora é de R${this.LimiteCredito}");
      }
    }

    public void RegistrarCompra(decimal valor)
    {
      if (valor > this.LimiteCredito)
      {
        Console.WriteLine("saldo insuficiente!");
      }
      else
      {
        LimiteCredito = LimiteCredito - valor;
        SaldoDevedor = SaldoDevedor + valor;

        Console.WriteLine("compra efetuada com sucesso!");
        Console.WriteLine($"seu limite atual agora é de: {this.LimiteCredito}");
      }
    }
  }
}