# Cinemaratona Backend

## Visão Geral

Este projeto é o backend para a aplicação Cinemaratona. Ele fornece APIs e serviços para gerenciar e interagir com a plataforma Cinemaratona.

## Pré-requisitos

Antes de começar, certifique-se de que você atendeu aos seguintes requisitos:
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Instalação

1. Clone o repositório:
    ```sh
    git clone https://github.com/yourusername/cinemaratona-backend.git
    cd cinemaratona-backend
    ```

2. Restaure as dependências:
    ```sh
    dotnet restore
    ```

3. Aplique as migrações do banco de dados:
    ```sh
    dotnet ef database update
    ```

4. Adicione uma chave para jwt:
    - Utilizando user-secrets (local)
    ```sh
    dotnet user-secrets set "Jwt:Key" "sua-chave-secreta-supersegura"
    ```

    - Utilizando variaveis de ambiente (Hosting)
    ```sh
    Jwt__Key="sua-chave-secreta-supersegura"
    ```

## Executando o Projeto

1. Inicie a aplicação:
    ```sh
    dotnet run
    ```

2. A aplicação será iniciada em `http://localhost:5199` por padrão. Você pode acessar a documentação da API em `http://localhost:5199/swagger`.

## Executando com Docker Compose

1. Certifique-se de que você tem o Docker e o Docker Compose instalados.

2. Construa e inicie os containers:
    ```sh
    docker-compose up --build
    ```

3. A aplicação será iniciada em `http://localhost:5199` por padrão. Você pode acessar a documentação da API em `http://localhost:5199/swagger`.

## Executando Testes

Para executar os testes, use o seguinte comando:
```sh
dotnet test TestCinemaratona
```

## Construindo o Projeto

Para construir o projeto, use o seguinte comando:
```sh
dotnet build Cinemaratona
```

## Estrutura do Banco de Dados

O esquema do banco de dados está definido no arquivo `DatabaseSchema.dbml`. Ele contém as seguintes tabelas:

- `users`: Armazena informações dos usuários.
- `friendships`: Gerencia as relações de amizade entre os usuários.
- `reviews`: Armazena as avaliações dos filmes feitas pelos usuários.
- `events`: Gerencia os eventos criados pelos usuários.

## Configuração do Insomnia

Para facilitar o desenvolvimento e teste das APIs, um arquivo de configuração do Insomnia (`Insomnia.yaml`) está incluído no projeto. Ele contém as seguintes requisições:

- `Get`: Recupera todos os usuários.
- `Get by id`: Recupera um usuário específico pelo ID.
- `Post`: Cria um novo usuário.
- `Put`: Atualiza um usuário existente.
- `Delete`: Remove um usuário pelo ID.

Para usar o arquivo de configuração, importe-o no Insomnia e ajuste as variáveis de ambiente conforme necessário.

## Contribuindo

Se você deseja contribuir para este projeto, siga estes passos:

1. Faça um fork do repositório.
2. Crie um novo branch (`git checkout -b feature-branch`).
3. Faça suas alterações.
4. Comite suas alterações (`git commit -m 'Adicione alguma funcionalidade'`).
5. Faça o push para o branch (`git push origin feature-branch`).
6. Abra um pull request.


## Contato

Se você tiver alguma dúvida ou sugestão, sinta-se à vontade para abrir uma issue ou entrar em contato com os mantenedores do projeto.
