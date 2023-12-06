# Desafio Eclipseworks

## Docker Setup
---
Esta seção dedica-se a configurar e rodar a aplicação no Docker.

- [Instalação](#install-docker)
- [Configuração](#configure-docker)

### Instalação
---
Você pode instalar o Docker Desktop aqui (Docker Desktop para Windows): https://docs.docker.com/docker-for-windows/install/

### Configuração
---
Vamos utilizar o **WSL 2 backend**. Você consegue fazer a instalação seguindo este passo-a-passo: https://docs.microsoft.com/en-us/windows/wsl/install-win10

Com tudo instalado, rode na pasta onde está o Dockerfile o seguinte comando:

```
docker-compose up --build
```

Set tudo deu certo, você pode acessar a URL: http://localhost:5000/swagger.

## Refinamento

### Performance
---
1) Quais são as atuais métricas de performance da API? (Tempo de resposta, taxa de transferência, taxa de erros)
2) Há alguma melhoria específica de performance ou otimização desejada?

### Escalabilidade
---
1) Quais são as cargas de usuários e de tráfego esperadas para utilização da API?
2) Há algum atual requisito de escalabilidade?

### Segurança
---
1) Quais são os atuais mecanismos de atutenticação e autorização implementados?
2) Há alguma preocupação no quesito segurança atualmente?

### Logging e Monitoramento
---
1) Quais mecanismos de logs existem hoje e que tipo de informação é registrada em log?
2) Há alguma ferramenta de monitoramento disponível?

### CI/CD
---
1) Quais são, atualmente, os processos de deploy, e como estão (se estão) implementadas as esteiras de CI/CD?


## Final

### Orquestração
---
Como a API já roda em um contêiner Docker, usar uma ferramenta de orquestração como o Kubernetes ajudaria a gerenciar a escalabilidade em contêineres da aplicação.

### Arquitetura em Microsserviços
---
Se aplicável, podemos desconsiderar o padrão monolito e dividi-lo em microsserviços, ou seja, visando escalabilidade, poderíamos ter um microsserviço para Projeto, Tarefas, Usuários etc.

### Mecanismo de cache
---
Implementação de estratégias de cache para melhorar performance e garantir proteção a grandes gargalos de conexão ao banco de dados, ou seja, utilizar alguma ferramenta de in-memoring cache (como Redis, por exemplo) para salvar dados genéricos utilizados com maior frequência, evitando requisições no banco de dados.

### Logging e Monitoramento
---
Implementação de soluções de logs (pode ser ILogger). Utilização de ferramentas como o Azure Application Insights ou AWS CloudWatch para ajudar no rastreio de erros, padrões de uso etc.

### Limite a número de requisições
---
Configurar limite ao número de requisições da API (Azure DevOps).

