# Livraria

Este projeto, chamado **Livraria**, é uma **API RESTful** em **.NET 8.0**, com **Entity Framework Core** e **PostgreSQL**, orquestrados via **Docker Compose**. Permite **criar**, **editar** e **deletar** as entidades **Livro**, **Autor** e **Gênero**.

---

## Pré-requisitos

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Docker & Docker Compose](https://docs.docker.com/)
* [Git](https://git-scm.com/)
* CLI do Entity Framework Core:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## Configuração

1. Clone o repositório:

   ```bash
   git clone https://github.com/taillonmiguel/LivrariaDesafio.git
   cd LivrariaDesafio
   ```

2. Verifique o arquivo `.env` existente na raiz. Ele já contém as variáveis padrão para PostgreSQL e ASP.NET Core:

   ```dotenv
   POSTGRES_USER=postgres
   POSTGRES_PASSWORD=postgres
   POSTGRES_DB=LivrariaDb
   ASPNETCORE_ENVIRONMENT=Development
   ```

   Caso queira usar outros nomes, ajuste aqui e no docker-compose (ele referencia essas variáveis).

---

## Executando o projeto

1. Inicie os containers:

   ```bash
   docker-compose up --build -d
   ```

2. Aplique as migrações para criar o schema:

  # **na raiz do repositório**
   **Opção A: Usando CLI local**
   ```bash
   dotnet tool install -g dotnet-ef
   ```

   ```bash
docker-compose exec livraria_api \
  dotnet ef database update \
    --project /app/Livraria.Infra \
    --startup-project /app/Livraria.Api \
    --context AppDbContext
   ```

   **Opção B: Diretamente no container da API**

   ```bash
   docker-compose exec livraria_api \
     dotnet ef database update --environment Development
   ```

  **Opção C. Atualizar o banco pelo Visual Studio**

1. Abra a solução `Livraria.sln` no Visual Studio.
2. No **Solution Explorer**, clique com o botão direito em **Livraria.Api** e escolha **Definir como projeto de inicialização**.  
3. No menu superior, vá em **Ferramentas > Gerenciador de Pacotes NuGet > Console do Gerenciador de Pacotes**.  
4. Na janela do console:
   - Em **Projeto de inicialização** (Startup project), confirme que está **Livraria.Api**.  
   - Em **Projeto padrão** (Default project), selecione **Livraria.Infra**
5. Execute o comando:

   ```powershell
   Update-Database
   ```

3. A API estará disponível em:

   * HTTP:  `http://localhost:5124`
   * HTTPS: `https://localhost:5124`

4. Para acessar a documentação Swagger (apenas em Development): Para acessar a documentação Swagger (apenas em Development):

   ```
   https://localhost:5124/swagger/index.html
   ```

---

## Comandos úteis

| Comando                         | Descrição                                 |
| ------------------------------- | ----------------------------------------- |
| `docker-compose up -d`          | Inicia os containers em background        |
| `docker-compose logs -f`        | Exibe logs de todos os containers         |
| `docker-compose down -v`        | Para e remove containers, redes e volumes |
| `dotnet ef migrations add Nome` | Cria nova migration no projeto EF Core    |
| `dotnet ef database update`     | Aplica migrações pendentes                |

---

## Limpeza

Para remover containers e dados:

```bash
docker-compose down -v
```

---

> Pronto para rodar! Em caso de dúvidas, abra uma issue no repositório.
