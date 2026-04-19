# Sistema de Gestão de Livros (Biblioteca) usando JSON

Neste exercício, incluído no meu repositorio logica csharp, trabalhei um pouco
mais a entrada e saída de dados e, principalmente, a permanência dos dados.

Neste exercício utilizei a tecnologia System.Text.Json, que me permite utilizar
a serialização e deseralização de arquivos do tipo JSON.
(Note também que essa é a primeira vez que utilizo JSON nos meus exercícios,
apesar do meu projeto pessoal bit já ter incluído esse tipo de tecnologia).

No início do programa (main), é carregada uma lista vazia, e depois lemos tudo aquilo que está escrito dentro do arquivo livros.json, e depois o desserializamos.

Após desserializar (tornar os objetos json em objetos dotnet), se incia a lógica do
programa, que está bem simples de entender.

Para salvar novos livros se utiliza os códigos:

''
  listaLivros.Add(novoLivro);

    string jsonString = JsonSerializer.Serialize(listaLivros);
    File.WriteAllText("livros.json", jsonString);
''

Isso após de termos preenchido as propriedades necessárias para criar um objeto da classe "Livro", que são "Título" e "Autor".

A primeira linha serializa o que foi escrito na lista de livros, ou seja, transforma as propriedades do objeto numa string json, e depois escreve o
que foi escrito nessa string json no próprio arquivo json.

Desta forma, é possível começar a trabalhar com a persistência de dados, o que é mais do que essencial no mundo da programação e também em banco de dados.

github: davicarb