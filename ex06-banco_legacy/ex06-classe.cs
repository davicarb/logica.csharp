namespace banco_legacy
{
  public class ContaBancaria
  {
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Titular { get; set; }
    public int IdTitular { get; set; }
    public decimal Saldo { get; private set; }

    // public List<string> Historico {get; set;} - desenvolver a lista somente depois

    public ContaBancaria() { }

    public ContaBancaria(string nomeTitularRecebido, string emailRecebido, string senhaRecebida, int idGerado, decimal saldoConta)
    {
      Titular = nomeTitularRecebido;
      Email = emailRecebido;
      Senha = senhaRecebida;
      IdTitular = idGerado;
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