# validador de senhas e cadastro de usuários usando métodos no C# #

(note que ainda consta problemas relacionados a nullability, pois ainda não aprendi sobre o assunto).

1. O programa utiliza métodos, onde chamo cada método específico na função principal, tornando o programa
abstrato e escalável.

2. Cada método tem suas respectivas propriedades, para que a validação dos inputs seja feita de forma mais
eficaz e também acabe tornando o programa mais escalável a longo prazo.
(por se tratar de um programa de um exercício, claro que não será necessária manutenção, mas já estou treinando
para que meus futuros projetos, como o "bit learning software" sejam projetos com clean code e códigos mais abstratos
e escaláveis).

3. Como estou aprendendo ultimamente, cada função possui seu respectivo tipo.
Temos variáveis que retornam valores do tipo:
a) bool
b) string

Obviamente, no caso da variável do tipo bool, retornamos valores lógicos (booleanos):
false ou true.
E, no caso da variável do tipo string, retornamos valores do tipo "string".

Claro que em sistemas reais seriam implementados sistemas de segurança, para que não se retorne
valores do tipo null ou que não haja algum try catch. Mas, como eu já disse: Esse programa é para
treino da lógica.

16-04-2026
