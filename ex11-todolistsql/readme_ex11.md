## exercicio 11 - CRUD básico no dotnet, utilizando ADO.NET para manipulação de dados num database SQLite (um banco de dados local).

* para o desenvolvimento deste exercício, utilizei ADO.NET para manipular os dados na database de forma manual (comandos), sem usar, ainda, nenhuma camada de abstração, para aprender melhor como funciona o banco de dados.

* o banco de dados SQLite, por outro lado, é criado no momento em que o código é executado.
  o banco de dados conta com duas tabelas: usuário e tarefas.
  as duas tabelas estão conectadas com uma foreign-key: o UsuarioId.

  - este UsuarioId serve para localizar tarefas específicas de cada usuário, servindo para dividir o que é tarefa do usuárioA do que é tarefa do usuárioB, por exemplo.

  - para uma melhor experiência do usuário, foram introduzidos dois menus para navegação entre os vários métodos do programa.
  
este sistema serve como blue-print para próximos projetos que pretendo fazer utilizando ADO.NET e SQLite.
futuramente, pretendo utilizar o Entitiy Framework para diminuir um pouco a quantidade de código necessária para acessar o banco de dados.
mas, por enquanto, prefiro usar o ADO.NET para entender a base de como a database se comunica com o código C#.

project based learning, 2026.