using System;
using System.Diagnostics.Contracts;
using System.Net;

namespace ex01validador
{

  class Program
  {
    static bool ConfirmacaoSenha(string senhaUsuario)
    {
      Console.Write("Confirme sua senha: ");
      string confirmaSenhaUsuario = Console.ReadLine();

      if (confirmaSenhaUsuario == senhaUsuario)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    static string CadastraNomeUsuario()
    {
      Console.Write("Insira seu nome: ");
      string nomeCadastro = Console.ReadLine();

      return nomeCadastro;

    }
 
    static string CadastraSenhaUsuario()
    {
      Console.Write("Crie sua senha: ");
      string senhaCadastrada = Console.ReadLine();

      return senhaCadastrada;

    }
    static bool ValidarCadastro(string usuario, string senha, bool confirmacaoSenha)
    {
      if (usuario.Length < 5 || senha.Length < 8 || confirmacaoSenha == false )
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    static void MostraRegraQuebrada(string usuario, string senha, bool confirmacaoSenha, bool CadastroValido)
    {
      if (CadastroValido)
      {
        Console.Write("Cadastro feito com sucesso!");
      }
      else
      {
        Console.Write("Cadastro não concluído com sucesso.\n");
        if (usuario.Length < 5)
        {
          Console.Write("O seu nome de usuário deve conter no mínimo 5 caracteres.\n");
        }
        if (senha.Length < 8)
        {
          Console.Write("Sua senha deve conter no mínimo 8 caracteres.\n");
        }
        if (!confirmacaoSenha)
        {
          Console.Write("A etapa da confirmação falhou. Você digitou corretamente as duas senhas?");
        }
      }
    }

    static void Main(string[] args)
    {
      string nomeUsuario = CadastraNomeUsuario();
      string senhaUsuario = CadastraSenhaUsuario();
      bool senhaValida = ConfirmacaoSenha(senhaUsuario);
      bool cadastroValido = ValidarCadastro(nomeUsuario, senhaUsuario, senhaValida);
      
      MostraRegraQuebrada(nomeUsuario, senhaUsuario, senhaValida, cadastroValido);

    }
  }
}