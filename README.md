# Projeto Incrível (Manage Employees) 

Olá! Bem-vindo(a) ao repositório do Projeto (Manage Employees), uma aplicação full-stack construída com .NET 8, Angular 18 e Postgres. A melhor parte? Colocá-la para rodar é *muito* fácil! 

## Sobre o Projeto

Este projeto demonstra um CRUD de funcionário. Ele utiliza as seguintes tecnologias:

*   **Backend:** .NET 8
*   **Frontend:** Angular 18
*   **Banco de Dados:** Postgres
*   **Orquestração:** Docker Compose

## Pré-requisitos

Antes de começar, certifique-se de ter instalado:

*   [Docker](https://www.docker.com/get-started) (com Docker Compose)

## Executando a aplicação (Modo Turbo ️)

A maneira mais rápida de rodar este projeto é com o Docker Compose. Basta seguir estes passos:

1.  Clone este repositório:

    ```bash
    git clone https://github.com/KleberRibeiro89/manage-employees
    cd manage-employees
    ```

2.  Navegue até o diretório raiz do projeto (onde está o arquivo `docker-compose.yml`).

3.  Execute o comando mágico:

    ```bash
    docker-compose up -d --build
    ```

    Aguarde alguns instantes para o Postgres inicializar e o backend compilar.

4.  Em um novo terminal, navegue até o diretório `frontend` (ou o nome da sua pasta do Angular):

    ```bash
    cd frontend
    ```

5.  Instale as dependências do Angular:

    ```bash
    npm install # ou yarn install
    ```

6.  Inicie o servidor de desenvolvimento do Angular:

    ```bash
    ng serve --open
    ```

    (O `--open` abre o navegador automaticamente.)


Pronto! A aplicação estará disponível em `http://localhost:4200` (ou a porta que o Angular definir).

7.  A aplicação carrega via migration um usuário padrão `admin@example.com` e a senha: 123456

## Parando a aplicação

Para parar os containers Docker:

```bash
docker-compose down
