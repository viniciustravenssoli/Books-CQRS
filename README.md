# Books Info

## Descrição
Uma WebApi, que realiza autenticação e autorização de usuarios, CRUD de livros, comentarios, generos e autores com seu respectivos relacionamentos, nele tento aplicar alguns padroes do CQRS e Repository Pattern

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
