**Rota de Registro**
- Rota: /register
- Explicação: Registra um novo usuário na plataforma.
- Método: POST
- Body Request (JSON):

``
{
  "email": "seuemail@email.com",
  "password": "senha"
}
``

</br>
</br>
**Rota de Autenticação**
- Rota: /login
- Explicação: Autentica o usuário registrado e retorna um JWT Token.
- Método: POST
- Body Request (JSON):

``
{
  "email": "seuemail@email.com",
  "password": "senha"
}
``

- Body de Resposta (em caso de sucesso):

``
{
  "message": "Login realizado com sucesso",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}``


</br>
</br>

**Rota para Incluir Tarefa**
- Rota: /api/todos
- Explicação: Inclui uma nova tarefa para o usuário autenticado.
- Método: POST
- Requisito: O usuário deve estar autenticado com um JWT Token.
- Header: Authorization: Bearer {token}
- Body Request (JSON):

``
{
  "title": "Tarefa atribuída para o usuário",
  "description": "Essa tarefa é uma tarefa difícil e complicada"
}
``


</br>
</br>

**Rota para Listar Todas as Tarefas**
- Rota: /api/todos
- Explicação: Retorna todas as tarefas atribuídas ao usuário autenticado.
- Método: GET
- Requisito: O usuário deve estar autenticado com um JWT Token.
- Header: Authorization: Bearer {token}

</br>
</br>

**Rota para Visualizar uma Tarefa Específica**
- Rota: /api/todos/{id}
- Explicação: Retorna os detalhes de uma tarefa específica com base no TaskId.
- Método: GET
- Requisito: O usuário deve estar autenticado com um JWT Token.
- Header: Authorization: Bearer {token}

</br>
</br>


**Rota para Editar uma Tarefa**
- Rota: /api/todos/{id}
- Explicação: Edita uma tarefa existente com base no TaskId.
- Método: PUT
- Requisito: O usuário deve estar autenticado com um JWT Token.
- Header: Authorization: Bearer {token}
- Body Request (JSON):

``
{
  "title": "Segunda Tarefa Modificada para o usuário",
  "description": "Tarefa que antes era assim agora é assado",
  "status": "Concluída"
}``



</br>
</br>

**Rota para Remover uma Tarefa**
- Rota: /api/todos/{id}
- Explicação: Remove uma tarefa específica com base no TaskId, desde que o usuário tenha permissão.
- Método: DELETE
- Requisito: O usuário deve estar autenticado com um JWT Token.
- Header: Authorization: Bearer {token}
