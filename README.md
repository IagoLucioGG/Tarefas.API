# Tarefas.API

Este projeto demonstra a construção de uma **Web API em .NET 10** utilizando um **único projeto**, mantendo organização, separação lógica de responsabilidades e boas práticas de backend.

O objetivo não é criar apenas mais um CRUD de tarefas, mas apresentar uma abordagem estruturada para desenvolvimento de APIs, com domínio bem definido, casos de uso claros, validação, segurança e tratamento consistente de erros — sem a complexidade de múltiplos projetos ou class libraries.

---

## Objetivo do Projeto

- Demonstrar organização de código em APIs ASP.NET Core
- Aplicar separação de responsabilidades mesmo em um único projeto
- Modelar um domínio simples de forma consistente
- Implementar casos de uso explícitos
- Expor endpoints REST com contratos bem definidos
- Aplicar validação, autenticação e tratamento global de erros

---

## Escopo Funcional

A API permite:

- Criar tarefas  
- Atualizar tarefas  
- Concluir tarefas  
- Listar tarefas com filtros  
- Autenticação via JWT  
- Autorização básica por perfil  

Fora do escopo:

- Frontend  
- Microserviços  
- Integrações externas  
- Mensageria  
- Cache distribuído  

---

## Organização do Projeto

Apesar de existir apenas um projeto (`Tarefas.API`), o código é organizado por **camadas lógicas**, separadas em pastas, respeitando dependências claras entre responsabilidades.

### Estrutura lógica

- **Domínio**  
  Contém as entidades, estados e regras de negócio.  
  Não depende de infraestrutura nem de detalhes técnicos.

- **Aplicação**  
  Contém os casos de uso, DTOs e contratos.  
  Orquestra o domínio sem conhecer HTTP ou persistência.

- **Infraestrutura**  
  Implementa persistência, repositórios e detalhes técnicos.  
  Serve à aplicação, sem concentrar regras de negócio.

- **API**  
  Responsável pela exposição HTTP, controllers, rotas e status codes.  
  Atua apenas como camada de entrada.

- **Compartilhado**  
  Elementos reutilizáveis como exceções, resultados padrão e utilitários.

Essa abordagem reduz complexidade estrutural sem abrir mão de clareza arquitetural.

---

## Decisões Arquiteturais

- Projeto único para simplificar manutenção e onboarding
- Separação por pastas para preservar fronteiras lógicas
- Domínio isolado de detalhes técnicos
- Casos de uso explícitos em vez de lógica espalhada em controllers
- Tratamento global de erros para respostas consistentes
- Uso consciente de autenticação e validação

---

## Tecnologias Utilizadas

- .NET 10  
- ASP.NET Core Web API  
- Entity Framework Core  
- Autenticação JWT  

---

## Considerações Finais

Este projeto foi desenvolvido com foco em **clareza, manutenção e boas práticas**, refletindo decisões comuns em aplicações backend reais.

A intenção é demonstrar maturidade técnica, organização e capacidade de estruturar APIs que possam evoluir de forma sustentável ao longo do tempo.


**Readm criado por IA, para facilitar na descrição do projeto.**