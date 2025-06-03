# SafeRoute

SafeRoute é uma solução inteligente para salvar vidas durante desastres naturais, conectando pessoas em risco a alertas em tempo real, abrigos próximos e rotas seguras.

## 🚀 Visão Geral

Com o aumento de eventos extremos como enchentes, deslizamentos e incêndios, o SafeRoute foi criado para ajudar pessoas a escaparem rapidamente e com segurança. A plataforma permite:

- Criação de alertas sobre situações de risco.
- Localização do abrigo mais próximo.
- Exibição da rota mais segura até o abrigo.
- Integração com sensores IoT para alertas automáticos.

## 🛠️ Tecnologias Utilizadas

- AspNET
- banco de dados Oracle
- Swagger

## 📦 Funcionalidades

- **Usuário:** Cadastro,gerenciamento de usuarios.CRUD completo.
- **Alerta:** Cadastro de alertas de risco pelo usuario ,CRUD completo.
- **Abrigo:** Cadastro e consulta de abrigos, CRUD completo.
- **Rota Segura:** Cadastro e consulta de rotas seguras com base no alerta que foi criado pelo usuario e abrigo criado, CRUD completo.

## Passos
1. Clone o repositório

2. Configure a string de conexão no arquivo `appsettings.json`

3. Execute as migrações para criar o banco de dados
   
4. Inicie o servidor

5. Acesse o swagger da API em: http://localhost:5043/swagger/index.html

## Parte de testes

Usuarios- deixamos um usuario ja criado de exemplo no sistema:
{
    "id": 27,
    "nome": "juan pablo",
    "email": "juan@example.com",
    "senha": "string"
  }


## Integrantes

Barbara Dias Santos rm: 556974

Natasha Lopes Rocha Oliveira rm: 554816

Juan Pablo Ruiz de Souza rm: 557727
