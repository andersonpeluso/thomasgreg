# GraphQL vs REST

## REST: O que é, benefícios e limitações
REST é uma arquitetura de design de API usada para implementar serviços da web. Os serviços da web RESTful permitem que os sistemas acessem e manipulem as representações textuais dos recursos da web usando um conjunto predefinido de operações sem estado (incluindo GET, POST, PUT e DELETE).
Além disso, é que a implementação do cliente e do servidor geralmente é feita de forma independente. Isso significa que o código do lado do cliente pode ser alterado sem afetar a maneira como o servidor opera e vice-versa. Dessa forma, eles são mantidos modulares e separados.
A idéia principal do REST é que tudo é um recurso identificado por uma URL. Na sua forma mais simples, você recuperaria um recurso inserindo uma solicitação GET na URL do recurso e obteria uma resposta JSON (ou algo semelhante, dependendo da API).
Pode ser algo como isto: **GET/filmes/1**

É importante observar que, com o REST, o tipo (ou forma) do recurso e a maneira como você busca esse recurso específico são acoplados. Portanto, você pode se referir ao snippet acima como o ponto final do usuário.

## Benefícios do REST
Uma das principais vantagens do REST é que o REST é escalável. A arquitetura desacopla cliente e servidor, o que permite que os desenvolvedores escalem produtos e aplicativos indefinidamente, sem muita dificuldade.
Além disso, as APIs REST oferecem uma grande flexibilidade. Na prática, como os dados não estão vinculados a recursos ou métodos, o REST pode lidar com diferentes tipos de chamadas e retornar diferentes formatos de dados. Isso permite que os desenvolvedores criem APIs que atendam às necessidades especí
