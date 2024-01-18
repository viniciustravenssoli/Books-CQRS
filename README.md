# Books API

## Descrição
Uma WebApi que realiza autenticação e autorização de usuários, CRUD de livros, comentários, gêneros e autores com seus respectivos relacionamentos. Tenta aplicar alguns conceitos do CQRS e Repository Pattern.

## Tecnologias Utilizadas
- **.NET Core**: Framework de desenvolvimento rápido e multiplataforma.
- **C#**: Linguagem de programação moderna e orientada a objetos.
- **ASP.NET Identity**: Framework para gerenciamento de identidade e autenticação.
- **Entity Framework**: Mapeamento objeto-relacional para interagir com o banco de dados.
- **MediatR**: Biblioteca para implementação de padrão Mediator para comunicação entre componentes.

## Iniciando uma Migração

Para iniciar uma migração no seu projeto, siga os passos abaixo:

1. Navegue até o diretório de infraestrutura do projeto.

    ```bash
    cd .\Infra\  
    ```

2. Execute o seguinte comando para adicionar uma nova migração:

    ```bash
    dotnet ef migrations add -s ..\Books\Books.csproj NameOfMigration --verbose
    ```

    Certifique-se de substituir `NameOfMigration` pelo nome desejado para a migração.

3. Após adicionar a migração, você precisará atualizar o banco de dados com as alterações. Execute o seguinte comando:

    ```bash
    dotnet ef database update -s ..\Books\Books.csproj --verbose
    ```

Este comando aplicará as alterações no banco de dados usando a migração que você acabou de criar.

## Endpoint para Postar Comentários

Para postar um comentário, é necessário autenticar-se primeiro. Siga os passos abaixo:

1. Faça login na API para obter um token JWT.

    **Endpoint de Login:**
    
    ```
    POST baseurl/api/user/login
    ```

    - Parâmetros do corpo da solicitação: `email` e `password`.
    - Este endpoint retornará um token JWT após uma autenticação bem-sucedida.
    - **Nota: O token JWT possui uma duração de 10 minutos. Certifique-se de renová-lo conforme necessário.**

1. 1 Caso não possua um usuário, é necessário realizar o registro antes de efetuar o login.

    **Endpoint de Registro:**
    
    ```
    POST baseurl/api/user/register
    ```

    - Parâmetros do corpo da solicitação: `username`, `email` e `password`.
    - Este endpoint retornará uma mensagem indicando que o usuário foi criado com sucesso, se todas as validações forem bem-sucedidas.
    - Em caso de falha nas validações, o endpoint retornará os erros correspondentes, como formato de e-mail inválido, senha fraca, etc.

    **Exemplo de Corpo da Solicitação:**
    
    ```json
    {
      "username": "seuNomeDeUsuario",
      "email": "seuEmail@example.com",
      "password": "Abc123_"
    }
    ```

    - Após o registro bem-sucedido, prossiga para o passo de login.

2. Com o token JWT obtido, adicione-o ao cabeçalho de autorização nas solicitações para o endpoint de postagem de comentários.

    **Endpoint de Postagem de Comentários:**
    
    ```
    POST baseurl/api/comment/post
    ```

    - Cabeçalho da Solicitação:
    
        ```
        Authorization: Bearer {seu_token_jwt}
        ```

    - Corpo da Solicitação: Forneça os detalhes necessários para postar o comentário.

    - Este endpoint permite a postagem de comentários após autenticação bem-sucedida.

### Observação: Utilizando o Swagger

Você pode utilizar o Swagger para interagir facilmente com a API. Após obter o token JWT, clique no botão "Authorize" no Swagger UI e insira o token no formato `Bearer {seu_token_jwt}`. Isso permitirá que você faça solicitações autenticadas diretamente no Swagger.

Lembre-se de que a autenticação é essencial para garantir a segurança dos endpoints sensíveis, como a postagem de comentários.
