# exercicio 02 de lógica no csharp #

Neste exercício, trabalhei já com a POO (Programação orientada a objetos).
(Note que, como no exercício 01, ainda consta problemas de nullability).
(Note também que o programa não roda em loop, e é sim uma versão simples.
Futuramente haverá uma versão dele mais elaborada utilizando um loop).

O enunciado do exercício consta aqui na pasta também.

O exercício é baseado na seguinte proposta:

Uma classe, chamada Modulo.
E, nessa classe, crio um objeto da Classe Módulo, chamado "Arquitetura".
Ele é inicializado no inicio na função static main:

    Modulo Arquitetura = new Modulo
      {
       nome = "Arquitetura",
       PontosAtuais = 0,
       PontosParaUparNivel = 100
      };

Para criar qualquer objeto dessa classe, é necessário, como descrito na public
class Modulo (no outro arquivo), declarar as propriedades dela; neste caso, são elas:
Nome, PontosAtuais e PontosParaUparNivel.

* Lembre-se que por se tratar de um objeto que é criado a partir de uma classe (Modulo),
é possível criar um objeto Modulo sem ser necessariamente Arquitetura. É o "blueprint"
que torna possível a criação de qualquer outro objeto. Isso é um dos pilares da POO: Abstração. *

É declarado, logo após a inicialização do programa, que os PontosAtuais são 0 e os PontosParaUparNivel
são 100.

* Detalhe: quando for feita a atualização desse exercício, essas atribuições serão feitas fora do loop
do programa, para que os pontos se acumulem enquanto o programa é executado.


Dentro do método "static int RecebeValorGanho()", temos:
''
{

    Console.WriteLine("Insira o valor de xp ganho: ");
    bool valid = int.TryParse(Console.ReadLine(), out int valorGanho);

    while (!valid || valorGanho < 0)
    {
      Console.WriteLine("Valor inválido. Somente números positivos. Insira novamente: ");
      valid = int.TryParse(Console.ReadLine(), out valorGanho);
    }

      return valorGanho;
}
''

Neste caso, trabalhei utilizando uma estrutura clássica de verificação de input.

Bem sabemos que um input errado pode causar o crash do programa caso tentemos atribuir
diretamente, transformando sem validação, o input do usuário à variável na função main.
Logo, utilizei um sistema bool, que verifica se o input do usuário está certo ou não.
É necessário ele ser "valid" e também ser um valor positivo " >= 0 ".

Caso o valor seja positivo e seja valido, retornamos o valor correto para a variável na função
principal.



Dentro do método:
'' static void AdicionarExperiencia(Modulo Arquitetura, int valorGanho)
    {
      Arquitetura.PontosAtuais = +valorGanho;
      int diferencaPontos = Arquitetura.PontosParaUparNivel - valorGanho;

      if (Arquitetura.PontosAtuais < 100)
      {
        Console.WriteLine("Hmm... você ainda não passou de nível.\n");
        Console.WriteLine("É necessário mais XP!\n");
        Console.WriteLine($"O XP necessário é de: {diferencaPontos} !");
      }

      else
      {
        Console.WriteLine("Parabéns! Você subiu de nível!");
        Arquitetura.PontosAtuais = 0;
      }
    }
''

Neste caso utilizei uma função do tipo "static void", que não retorna valor algum,
apenas faz o que está indicado dentro das chaves.

Criei uma tecnologia que calcula a diferença de pontos (que, por acaso, não estava
descrita no enunciado que o Gemini me passou).

* Essa tecnologia será muito útil quando este programa/exercício evoluir para um pro-
grama contendo loops, onde o usuário pode inserir vários valores até chegar no valor
necessário para subir de nível e resetar novamente os PontosAtuais *

Os pontos atuais é a soma do valorGanho com os PontosAtuais.



Concluindo:
O Project Based Learning (aprendizado a base de projetos) é muito eficiente pois, além
de você ter que ter o conhecimento de como cada coisa funciona, é necessária também
a prática, que você só realmente ganha programando.

"Wanna be good at coding? Just code anything."
