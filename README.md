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
   
4. Inicie o servidor, ao iniciar o swagger ira abrir automaticamente

5. Caso nao abrir , acesse o swagger da API em: http://localhost:5251/swagger/index.html

## Parte de testes

Usuarios- deixamos um usuario ja criado de exemplo no sistema:
![telaasp1](https://github.com/user-attachments/assets/deaa2bdc-76dc-41d5-9424-fd22f9295d6a)

Alertas- esse nosso usuario criou uma alerta de um problema que ele noticiou vinculado a ele:
![telaasp2](https://github.com/user-attachments/assets/ea56c9ca-9d4b-4a23-96bb-7c0aab6e827e)

Abrigos- ja temos um abrigo disponivel no sistema para em cassos de dessastres o usuario se abrigar:
![telaasp3](https://github.com/user-attachments/assets/1b131884-93ac-4739-9f17-6b1c268cd4fa)

Rotas seguras- criamos por ultimo ua rota segura partindo da localização do alerta que o usuario criou ate o abrigo que ele deseja chegar:
![telaasp4](https://github.com/user-attachments/assets/6798e4e2-c0af-4be1-a7c5-2ea57733591d)



## Diagrama

## Integrantes

Barbara Dias Santos rm: 556974

Natasha Lopes Rocha Oliveira rm: 554816

Juan Pablo Ruiz de Souza rm: 557727
