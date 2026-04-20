using System.Text.Json;

namespace ex05_sis_gerencia_tarefa
{
  class Program
  {
    static void Main(string[] args)
    {
      var listaTarefas = new List<Tarefa>();

      if (File.Exists("tarefas.json"))
      {
        string fileName = "tarefas.json";
        string jsonStringCarregar = File.ReadAllText(fileName);
        listaTarefas = JsonSerializer.Deserialize<List<Tarefa>>(jsonStringCarregar);
      }

      else
      {
        Console.WriteLine("arquivo não encontrado...");
      }

      bool rodando = true;
      while (rodando)
      {

        int opcao = MostraMenuRetornaOpcaoMenu();

        switch (opcao)
        {
          case 1:
            AdicionaESalvaTarefaNoJson(listaTarefas);
            break;

          case 2:
            MarcarComoConcluida(listaTarefas);
            break;

          case 3:
            SomenteListarTarefas(listaTarefas);
            break;

          case 4:
            Console.WriteLine("saindo...");
            rodando = false;
            break;
        }
      }
    }

    static void SomenteListarTarefas(List<Tarefa> lista)
    {
      Console.Clear();
      Console.WriteLine("--- lista de tarefas ---\n");
      if (lista.Count > 0)
      {
        for (int i = 0; i < lista.Count; i++)
        {
          Console.WriteLine($"Tarefa {i + 1}: {lista[i].Descricao}");
          Console.WriteLine($"Prioridade: {lista[i].Prioridade}");
          Console.WriteLine($"Concluída?: {lista[i].IsConcluda}\n");
        }

        Console.WriteLine("pressione qualquer tela para retornar ao menu...");
        Console.ReadKey();
      }

      else
      {
        Console.WriteLine("lista de tarefas atualmente vazia!");
        Console.WriteLine("adicione tarefas e elas aparecerão aqui.\n");
        Console.WriteLine("pressione qualquer tela para retornar ao menu...");
        Console.ReadKey();
      }
    }
    static int MostraMenuRetornaOpcaoMenu()
    {
      Console.Clear();
      Console.WriteLine("--- gerenciador de tarefas json ---\n");
      Console.WriteLine("1. adicionar nova tarefa");
      Console.WriteLine("2. marcar tarefa como concluída");
      Console.WriteLine("3. listar tarefas atuais");
      Console.WriteLine("4. sair");

      bool validMenu = int.TryParse(Console.ReadLine(), out int opcaoValidada);

      while (!validMenu || opcaoValidada > 4 || opcaoValidada < 1)
      {
        Console.WriteLine("incorreto. insira novamente: ");
        validMenu = int.TryParse(Console.ReadLine(), out opcaoValidada);
      }

      return opcaoValidada;

    }
    static void MarcarComoConcluida(List<Tarefa> lista)
    {
      Console.Clear();
      Console.WriteLine("--- gerenciador de tarefas ---\n");

      if (lista.Count > 0)
      {
        for (int i = 0; i < lista.Count; i++)
        {
          Console.WriteLine($"Tarefa {i + 1}: {lista[i].Descricao}");
          Console.WriteLine($"Prioridade: {lista[i].Prioridade}");
          Console.WriteLine($"Concluída?: {lista[i].IsConcluda}\n");
        }

        Console.WriteLine("Digite o número da tarefa:");
        bool valid = int.TryParse(Console.ReadLine(), out int numTarefa);

        while (!valid || numTarefa > lista.Count || numTarefa <= 0)
        {
          Console.WriteLine("valor inválido ou tarefa não existente! : ");
          Console.WriteLine("tente novamente: ");
          valid = int.TryParse(Console.ReadLine(), out numTarefa);
        }

        numTarefa = numTarefa - 1;

        lista[numTarefa].IsConcluda = true;
        Console.WriteLine($"parabéns, tarefa {lista[numTarefa].Descricao} concluída! :D");

        Console.WriteLine("pressione qualquer tela para voltar ao menu...");
        Console.ReadKey();
      }

      else
      {
        Console.WriteLine("lista de tarefas atualmente vazia!");
        Console.WriteLine("adicione tarefas e elas aparecerão aqui.\n");

        Console.WriteLine("pressione qualquer tela para voltar ao menu...");
        Console.ReadKey();
      }
    }

      static void AdicionaESalvaTarefaNoJson(List<Tarefa> lista)
      {
        Console.Clear();
        Tarefa novaTarefa = new Tarefa();

        Console.WriteLine("descrição da tarefa: ");
        string descricaoTarefa = Console.ReadLine();

        novaTarefa.Descricao = descricaoTarefa;

        Console.WriteLine("prioridade da tarefa (1 até 3): ");
        bool valid = int.TryParse(Console.ReadLine(), out int prioridadeTarefa);

        while (!valid || prioridadeTarefa > 3 || prioridadeTarefa < 1)
        {
          Console.WriteLine("valor incorreto. valor deve ser de 1 até 3: ");
          valid = int.TryParse(Console.ReadLine(), out prioridadeTarefa);
        }

        novaTarefa.Prioridade = prioridadeTarefa;
        novaTarefa.IsConcluda = false;

        lista.Add(novaTarefa);

        var jsonString = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText("tarefas.json", jsonString);

        Console.WriteLine("tarefa cadastrada com sucesso!");
      }
    }
  }