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

## 