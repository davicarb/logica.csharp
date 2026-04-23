namespace sis_auth
{
  public class Usuario
  {
    public string NomeLogin {get; set;}
    public string SenhaLogin {get; set;}
    public DateTime DataCriacao {get; set;}

    public Usuario() { } // construtor para o JSON
    public Usuario(string nomeLoginFornecido, string senhaLoginFornecida, DateTime dataCriacao )
    {
      NomeLogin = nomeLoginFornecido;
      SenhaLogin = senhaLoginFornecida;
      DataCriacao = dataCriacao;
    }
  }

}
