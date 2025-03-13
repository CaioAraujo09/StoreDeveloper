DOCUMENTAÇÃO DO SISTEMA SALES MANAGEMENT
Versão: 1.0
Data: 2025-03-13
Autor: Caio Rodrigues

1. Introdução e Visão Geral
O sistema SalesManagement foi desenvolvido para gerenciar vendas, usuários registrados, carrinhos de compras, produtos e filiais, utilizando uma abordagem modular e escalável baseada em princípios de Domain-Driven Design (DDD). A API segue boas práticas de RESTful e possui suporte para paginação, ordenação e filtragem.

2. Arquitetura do Sistema
O sistema foi estruturado seguindo os princípios de Domain-Driven Design (DDD) e é dividido em quatro camadas principais:
🔹 API: Camada de apresentação responsável por expor os endpoints REST.
🔹 Application: Contém os serviços e a lógica de aplicação, implementando os casos de uso.
🔹 Domain: Define as entidades, regras de negócio e interfaces de repositórios.
🔹 Infrastructure: Implementa os repositórios, configuração do banco de dados e serviços de infraestrutura.

. Passo a Passo para Execução do Projeto
1. Clone o repositório do projeto:
   ```bash
   git clone https://github.com/seu-repositorio/StoreDeveloper.git
   ```
2. Acesse a pasta do projeto:
   ```bash
   cd SalesManagement
   ```
3. Restaure as dependências do projeto:
   ```bash
   dotnet restore
   ```
4. Configure o banco de dados no arquivo `appsettings.json` dentro da camada API.
5. Execute as migrations para atualizar o banco de dados:
   ```bash
   dotnet ef database update --context SaleDbContext
   dotnet ef database update --context AuthDbContext
   ```
6. Inicie a aplicação:
   ```bash
   dotnet run --project src/SalesManagement.API
   ```
7. Acesse a API no navegador pelo Swagger:
   ```
   https://localhost:7000/swagger


3. Descrição das APIs
3.1 API de Sales
A API de Sales gerencia as vendas, permitindo criação, listagem, cancelamento e consulta detalhada.

**Endpoints:**
✅ `POST /sales` - Cria uma nova venda
✅ `GET /sales` - Lista todas as vendas com paginação e filtros
✅ `GET /sales/{id}` - Consulta uma venda específica
✅ `PUT /sales/{id}/cancel` - Cancela uma venda existente
✅ `DELETE /sales/{id}` - Remove uma venda do sistema

**Regras de Negócio:**
🔹 Compras acima de 4 itens idênticos têm 10% de desconto.
🔹 Compras entre 10 e 20 itens têm 20% de desconto.
🔹 Não é possível vender mais de 20 itens idênticos.
3.2 API de RegisteredUsers
Gerencia os usuários registrados no sistema.

**Endpoints:**
✅ `POST /registeredusers` - Cadastra um novo usuário
✅ `GET /registeredusers` - Lista usuários com paginação e filtros
✅ `GET /registeredusers/{id}` - Consulta um usuário específico
✅ `DELETE /registeredusers/{id}` - Remove um usuário do sistema

**Regras de Negócio:**
🔹 O e-mail deve ser único no sistema.
🔹 Senhas são armazenadas de forma segura com hash.
3.3 API de Carts
Gerencia carrinhos de compra e os produtos dentro deles.

**Endpoints:**
✅ `POST /carts` - Cria um novo carrinho
✅ `GET /carts/{id}` - Consulta um carrinho específico
✅ `DELETE /carts/{id}` - Remove um carrinho do sistema

**Regras de Negócio:**
🔹 Um carrinho pertence a um único usuário registrado.
🔹 Apenas produtos existentes podem ser adicionados ao carrinho.
3.4 API de Products
Gerencia os produtos disponíveis para compra.

**Endpoints:**
✅ `POST /products` - Adiciona um novo produto
✅ `GET /products` - Lista produtos com paginação, filtros e ordenação
✅ `GET /products/{id}` - Consulta um produto específico
✅ `DELETE /products/{id}` - Remove um produto do sistema

**Regras de Negócio:**
🔹 O título do produto deve ser único.
🔹 Cada produto possui uma categoria e uma avaliação média.
3.5 API de Branches
Gerencia as filiais (lojas físicas) da empresa.

**Endpoints:**
✅ `POST /branches` - Adiciona uma nova filial
✅ `GET /branches` - Lista todas as filiais
✅ `GET /branches/{id}` - Consulta uma filial específica
✅ `DELETE /branches/{id}` - Remove uma filial do sistema

**Regras de Negócio:**
🔹 O nome da filial deve ser único.
4. Tratamento de Erros e Middleware
O sistema implementa um middleware global para captura e padronização de erros.
**Formato Padrão de Erro:**
{
  "type": "ValidationError",
  "error": "Erro ao processar a requisição.",
  "detail": "O campo 'username' é obrigatório."
}

5. Padrões de Paginação, Ordenação e Filtros
As listagens principais suportam paginação, ordenação e filtros para melhor performance e experiência do usuário.
**Exemplo de Paginação:** `GET /products?_page=2&_size=20`
**Exemplo de Ordenação:** `GET /products?_order=price desc, title asc`
**Exemplo de Filtro:** `GET /products?category=electronics&_minPrice=100&_maxPrice=500`

6. Eventos e Publicação de Eventos no Sistema
O SalesManagement implementa o conceito de eventos para promover um baixo acoplamento entre os componentes da aplicação e garantir que ações importantes sejam comunicadas de forma assíncrona para outras partes do sistema. Isso é feito utilizando um Event Publisher para disparar eventos que podem ser consumidos por outros serviços ou sistemas externos.

6.1. Objetivo da Implementação de Eventos
A publicação de eventos permite:

Melhor organização do código e desacoplamento entre serviços.
Processamento assíncrono de ações que dependem de eventos passados.
Escalabilidade para suportar novas integrações sem modificar código existente.

7. Autenticação e Autorização com JWT
O SalesManagement utiliza JSON Web Token (JWT) para autenticação e autorização de usuários na API. Esse modelo garante segurança e escalabilidade, permitindo que os clientes façam requisições autenticadas sem a necessidade de armazenar informações de sessão no servidor.

7.1. Como Funciona a Autenticação JWT
O processo de autenticação JWT segue os seguintes passos:

O usuário envia um POST /auth/login com suas credenciais (username e senha).
O sistema valida as credenciais e, se corretas, gera um token JWT contendo informações do usuário.
O cliente usa esse token em todas as requisições subsequentes, enviando-o no cabeçalho Authorization.
O sistema valida o token para autorizar o acesso aos recursos protegidos.

8. Separação dos Contextos: SaleDbContext e AuthDbContext
No SalesManagement, adotamos uma abordagem modular e desacoplada para a gestão do banco de dados, separando as responsabilidades em dois contextos distintos:

SaleDbContext → Gerencia todas as entidades relacionadas às vendas, produtos, carrinhos e filiais.
AuthDbContext → Responsável pela autenticação e gerenciamento de usuários.
Essa estratégia segue os princípios do Domain-Driven Design (DDD), garantindo melhor organização do código, baixo acoplamento e maior escalabilidade.

8.1. Por que separar os contextos?
📌 Benefícios da separação:

✅ Isolamento de Responsabilidades:
Cada contexto foca em um domínio específico, evitando dependências desnecessárias entre vendas e autenticação.

✅ Maior Segurança:
As informações de autenticação (senhas, tokens, permissões) ficam separadas do restante do sistema, reduzindo o risco de exposição.

✅ Escalabilidade:
Podemos escalar ou migrar os bancos separadamente, por exemplo, armazenar autenticação em um serviço externo (ex: IdentityServer, Firebase Auth).

✅ Facilidade na manutenção:
Se precisarmos modificar as regras de autenticação, isso não impactará a lógica de vendas e vice-versa.

✅ Facilidade na Migração de Dados:
Caso a empresa precise mudar o sistema de autenticação (ex: migrar para OAuth 2.0 ou um provedor externo), a separação dos contextos facilita a transição sem afetar o restante do sistema.

8.2. Implementação dos Contextos
8.2.1. SaleDbContext (Contexto de Vendas)
Este contexto gerencia as entidades de domínio relacionadas às vendas.

📌 Entidades Gerenciadas:

Sales (Vendas)
SaleItems (Itens da venda)
Products (Produtos)
Cart (Carrinho de compras)
CartItems (Itens do carrinho)
Branches (Filiais)
RegisteredUsers (Usuários vinculados às vendas)

8.2.2. AuthDbContext (Contexto de Autenticação)
Este contexto é exclusivo para gerenciar autenticação e controle de acesso.

📌 Entidades Gerenciadas:

Users (Usuários autenticáveis)
Roles (Perfis de usuários)
Tokens (Tokens de acesso)

9. Conclusão
O SalesManagement foi projetado com uma arquitetura modular baseada em DDD para garantir escalabilidade, facilidade de manutenção e clareza na separação de responsabilidades. O uso de boas práticas, como middleware para tratamento de erros e suporte completo para paginação e filtros, garante uma API robusta e flexível para o gerenciamento de vendas e estoque.