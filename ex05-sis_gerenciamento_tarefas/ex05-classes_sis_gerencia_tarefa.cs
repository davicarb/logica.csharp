using System.Text.Json;

namespace ex05_sis_gerencia_tarefa
{
  public class Tarefa
  {
    public string Descricao {get; set;}
    public int  Prioridade {get; set;}
    public bool IsConcluda {get; set;}
  }
} 