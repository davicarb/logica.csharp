# gerenciador de tarefas utilizando json para permanência de dados

O gerenciador de tarefas contido nesta pasta do repositorio logica-
csharp funciona utilizando json, para que os dados permaneçam vivos
depois que o programa é fechado.

(Ainda consta problemas de nullability! Desconsidere por enquanto
pois ainda não aprendi sobre o assunto).

Utilizei:

-> System.Text.Json: para conseguir usar os métodos de deserializar
e serializar;

-> List<Tarefa>: que me ajudou a listar as instâncias do tipo Tarefa,
que são, no caso, as tarefas em si (possuindo propriedades como: des
crição, prioridade e se foi concluída ainda ou não);

* O mais interessante deste exercício foi que, apesar de ainda estar
trabalhando com as mesmas tecnologias que trabalhei no exercício an-
terior, consegui desenvolver mais ainda um dos pilares da POO:
abstração.

Manti a função main limpa, apenas utilizando o switch case, (com o int
da opção já validado), para que  o usuário possa navegar entre os mé-
todos que o programa proporciona.

Apesar de ainda se tratar de um programa de console, trabalhei nele con-
ceitos muito importantes no mundo da programação:

. Como fazer com que os dados permaneçam vivos após o computador ser desli-
gado?

. Como tornar a programação mais abstrata, tornando a manutenção um processo
mais simples e menos trabalhoso?

. Como separar o programa em métodos, e transitar entre estes métodos utili-
zando uma entrada validada, que não faça com que o sistema caia se o usuário
digitar algo errado no input?

github: davicarb
