Para realizar a autenticação do usuario será necessário fazer o registro:
###### Rota de Registro ######
na rota /register

Com o Body Request em JSON dessa maneira: 

{
  "email": "seuemail@email.com",
  "password": "senha"
}

###### Rota de Autenticação ######
Após a realização do registro será necessário realizar a autenticação na rota de /login 
com as informações que foram registradas.

{
  "email": "seuemail@email.com",
  "password": "senha"
}

Se houver sucesso o usuário terá acesso a sua chave JWT Token no Body de Resposta  com a seguinte mensagem:

{
    "message": "Login realizado com suceeso",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjb3JyZWlhLjAxNi50Y0BnbWFpbC5jb20iLCJqdGkiOiIxODNlYzFlZC1kM2I2LTQwMjMtYmQ1ZS01MWRiZjYzYzQ5OWYiLCJleHAiOjE3MjcxODkwMDYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwODAiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDgwIn0.h-4mFhDmzWO97xJBQDAbeyW0fAcledYJQA28ldHP-bg"
}

###### Rota de Tarefa  ######

Para incluir uma tarefa o usuário devera estar autenticado com o JWT Token caso contrário ele encontará um erro de usuário inválido 
Se o usuário não passar o token no Header vai retornar um Bad Request Token Vazio, válidando assim o login do usuário e única possibilidade de registrar/ver/atualizar e deletar uma tarefa é estando logado ao sistema. 

Para Incluir uma tarefa deverá utilizar o Auth no Header e o Json: 
{
    "title": "Tarefa atribuida para ao usuario",
    "description": "Essa tarefa é uma tarefa dificil e complicada"
}


Para pegar todas as tarefas atribuidas ao usuário autenticado devemos acessar a rota /api/todos com o método GET. 
Onde ele irá retornar um JSON com todas as tarefas que estão atribuidas ao UserId.

Se quisermos solicitar a visualização de apenas uma tarefa, será necessário passar o Id na rota /api/todos/{id}
ex: /api/todos/23

Onde irá retornar um JSON com apenas aquela TaskId e todos seus detalhes

Para editarmos uma tarefa basta estar com o usuario autenticado na rota /api/todos/{id} com o método PUT e o id da tarefa que iremos realizar a edição. 
{
    "Title": "Segunda Tarefa Modificada para o usuario ",
    "Description": "Tarefa que antes era asssim agora é assado"
}

Obs: Se quisermos alterar o Status basta colocar dentro do JSON o Status que a tarefa se encontra, caso contrário ela irá com o valor Default para o banco de dados até ser modificada
{
    "Title": "Segunda Tarefa Modificada para o usuario ",
    "Description": "Tarefa que antes era asssim agora é assado",
    "Status": "Concluida"  
}

Para Remover uma tarefa basta acessar a rota /api/todos/{id} com o método Delete se o usuário tiver permissão para realizar a remoção da terafa irá retornar sucesso. 



