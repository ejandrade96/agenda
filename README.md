# Projeto CRUD - C#, Entity Framework Core, XUnit, React, Cypress
Este projeto, visa oferecer para o usuário uma forma simples e intuitiva de armazenar e manipular sua rede de contatos.

## Assuntos Abordados no Desenvolvimento do Projeto:

#### Back-end
- Uso da linguagem C#;
- Implementação de testes integrados e unitários utilizando, XUnit, Moq, Fluent Assertions;
- Uso do ORM EF Core para trabalhar com o banco de dados;
- Uso da biblioteca AutoMapper para auxiliar no mapeamento dos objetos;
- Uso do DDD para modelar a aplicação;
- Uso de um objeto de resposta e classes de erros específicas, para notificar o usuário de possíveis erros. Evitando lançar exceções na aplicação;
- Uso de um repositório genérico;
- Uso do padrão Factory e Fluent Interface para criação dos objetos;
- Uso do padrão JWT para verificar se o usuário está autenticado a cada requisição;
- Uso do padrão DTO para o transporte de dados na aplicação;
- Uso dos padrões IoC, DI, DIP para comunicação entre as camadas;
- Uso do princípio ISP do SOLID nas interfaces dos repositórios;

#### Front-end
- Uso da biblioteca React;
- Uso do framework Cypress para realizar os testes;
- Uso do Axios para fazer requisições para api;

## Funcionalidades
- O usuário poderá se registrar:

![agenda-registro](https://user-images.githubusercontent.com/57911863/92414537-0bad0e80-f12b-11ea-8243-5957b347bd7e.png)

- O usuário poderá se autenticar:
![agenda-login](https://user-images.githubusercontent.com/57911863/92414702-cdfcb580-f12b-11ea-8cb1-b144a0fc0f06.png)
- O usuário poderá visualizar a lista de contatos, buscar, editar e deletar:

![agenda-painel](https://user-images.githubusercontent.com/57911863/92414729-f2589200-f12b-11ea-9c8c-1bf6f04a0a45.png)

- O usuário poderá cadastrar um contato:

![agenda-salvar](https://user-images.githubusercontent.com/57911863/92414805-406d9580-f12c-11ea-92ea-ce649189198a.png)
