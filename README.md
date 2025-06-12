# Livraria

Este projeto, chamado **Livraria**, � uma **API RESTful** em **.NET 8.0**, com **Entity Framework Core** e **PostgreSQL**, orquestrados via **Docker Compose**. Permite **criar**, **editar** e **deletar** as entidades **Livro**, **Autor** e **G�nero**.

---

## Pr�-requisitos

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Docker & Docker Compose](https://docs.docker.com/)
* [Git](https://git-scm.com/)
* CLI do Entity Framework Core:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## Configura��o

1. Clone o reposit�rio:

   ```bash
   git clone https://github.com/taillonmiguel/LivrariaDesafio.git
   cd LivrariaDesafio
   ```

2. Verifique o arquivo `.env` existente na raiz. Ele j� cont�m as vari�veis padr�o para PostgreSQL e ASP.NET Core:

   ```dotenv
   POSTGRES_USER=postgres
   POSTGRES_PASSWORD=postgres
   POSTGRES_DB=LivrariaDb
   ASPNETCORE_ENVIRONMENT=Development
   ```

   Caso queira usar outros nomes, ajuste aqui e no docker-compose (ele referencia essas vari�veis).

---

## Executando o projeto

1. Inicie os containers:

   ```bash
   docker-compose up --build -d
   ```

2. Aplique as migra��es para criar o schema:

   **Op��o A: Usando CLI local**

   ```bash
   cd Livraria.Api
   dotnet ef database update --environment Development
   cd ..
   ```

   **Op��o B: Diretamente no container da API**

   ```bash
   docker-compose exec livraria_api \
     dotnet ef database update --environment Development
   ```

   **Op��o C: Pelo Visual Studio**

   1. Abra a solu��o no Visual Studio.
   2. V� em **Ferramentas > Gerenciador de Pacotes NuGet > Console do Gerenciador de Pacotes**.
   3. No dropdown **Projeto padr�o**, selecione **Livraria.Api**.
   4. No Package Manager Console, selecione **Livraria.Infra**.   
   5. Execute:

      ```powershell
      Update-Database
      ```

3. A API estar� dispon�vel em:

   * HTTP:  `http://localhost:5124`
   * HTTPS: `https://localhost:5124`

4. Para acessar a documenta��o Swagger (apenas em Development): Para acessar a documenta��o Swagger (apenas em Development):

   ```
   https://localhost:5124/swagger/index.html
   ```

---

## Comandos �teis

| Comando                         | Descri��o                                 |
| ------------------------------- | ----------------------------------------- |
| `docker-compose up -d`          | Inicia os containers em background        |
| `docker-compose logs -f`        | Exibe logs de todos os containers         |
| `docker-compose down -v`        | Para e remove containers, redes e volumes |
| `dotnet ef migrations add Nome` | Cria nova migration no projeto EF Core    |
| `dotnet ef database update`     | Aplica migra��es pendentes                |

---

## Limpeza

Para remover containers e dados:

```bash
docker-compose down -v
```

---

> Pronto para rodar! Em caso de d�vidas, abra uma issue no reposit�rio.
