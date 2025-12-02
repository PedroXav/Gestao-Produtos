# Gestão de Produtos - Projeto Integrado

Aplicação integrada composta por **API**, **WebSite** e **Aplicação Desktop**, desenvolvida para gestão de usuários e produtos.  
O projeto atende aos requisitos de CRUD completo e controle de acesso com login.

---

## Arquitetura

- **Domain** → Entidades principais (`Usuario`, `Produto`)
- **Infrastructure** → `AppDbContext`, migrations e acesso ao banco
- **API** → Endpoints REST para CRUD de Usuários e Produtos
- **Web** → Aplicação ASP.NET MVC para gestão de Produtos
- **Desktop** → Aplicação WPF para gestão de Usuários

---

## Banco de Dados

### Usuário
- `Id` (int)
- `Nome` (string)
- `Senha` (string)
- `Status` (bool - ativo/inativo)

### Produto
- `Id` (int)
- `Nome` (string)
- `Preço` (decimal)
- `Status` (bool - ativo/inativo)
- `IdUsuarioCadastro` (int)
- `IdUsuarioUpdate` (int)

---
