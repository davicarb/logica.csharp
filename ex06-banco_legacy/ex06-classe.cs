namespace banco_legacy
{
  public class ContaBancaria
  {
    public string Titular { get; set; }
    public int IdTitular { get; set; }
    public decimal Saldo { get; private set; }

    // public List<string> Historico {get; set;} - desenvolver a lista somente depois

    public ContaBancaria(string nomeTitular, int idDado, decimal saldoConta)
    {
      Titular = nomeTitular;
      IdTitular = idDado;
      Saldo = saldoConta;
    }

    public bool Depositar(decimal valorDepositoValidado)
    {     
        Saldo = Saldo + valorDepositoValidado;
        return true;
    }

    public bool Sacar(decimal valorSaqueValidado)
    {
      if (valorSaqueValidado > Saldo)
      {
        return false;
      }

      else
      {
        Saldo = Saldo - valorSaqueValidado;
        return true;
      }
    }
  }
}