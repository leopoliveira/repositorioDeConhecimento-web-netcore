<h1>Repositório de Conhecimento</h1>

Este é um projeto de um Repositório de Conhecimento, desenvolvido utilizando .Net 6.0 com C#, SQL Server, Automapper, Entity Framework Core 6, Asp.Net core MVC, Identity e bootstrap 5. O padrão DDD e Repository foi utilizado, além de Injeção de Dependência e código limpo.

<h2>Tecnologias Utilizadas</h2>
As seguintes tecnologias foram utilizadas no desenvolvimento deste projeto:

<ul>
    <li><h3><a href="https://dotnet.microsoft.com/download/dotnet/6.0">.Net 6.0</a></h3></li>
    <li><h3><a href="https://docs.microsoft.com/pt-br/dotnet/csharp/">C#</a></h3></li>
    <li><h3><a href="https://docs.microsoft.com/pt-br/sql/sql-server/?view=sql-server-ver15">SQL Server</a></h3></li>
    <li><h3><a href="https://automapper.org/">Automapper</a></h3></li>
    <li><h3><a href="https://docs.microsoft.com/pt-br/ef/core/">Entity Framework Core 6</a></h3></li>
    <li><h3><a href="https://docs.microsoft.com/pt-br/aspnet/core/mvc/?view=aspnetcore-6.0">Asp.Net core MVC</a></h3></li>
    <li><h3><a href="https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=netcore-cli">Identity</a></h3></li>
    <li><h3><a href="https://getbootstrap.com/docs/5.1/getting-started/introduction/">Bootstrap 5</a></h3></li>
</ul>

<h2>Instalação</h2>
Para executar o projeto localmente em sua máquina, você precisará seguir os seguintes passos:

Faça o download ou clone este repositório em sua máquina local.
Abra a solução do projeto no Visual Studio ou VS Code.
Certifique-se de que o banco de dados foi criado, executando o script de criação de banco de dados que se encontra no diretório RepositorioDeConhecimento.Infra\Scripts.
No Visual Studio, abra o Package Manager Console e execute o comando Update-Database para atualizar o banco de dados.
Certifique-se de que todas as dependências foram instaladas utilizando o Nuget Package Manager.
Execute o projeto pressionando a tecla F5 no Visual Studio ou digitando dotnet run no terminal, se estiver utilizando o VS Code.
Acesse o aplicativo no seu navegador, em https://localhost:{porta}/.

<h2>Funcionalidades</h2>
O Repositório de Conhecimento possui as seguintes funcionalidades:

<h3>Tela de Cadastro de Conhecimentos</h3>

Busca  de conhecimentos
Paginação nos dados exibidos em cada uma das funcionalidades
![image](https://user-images.githubusercontent.com/35302072/227380924-5fb0e7d1-ec8d-4d2d-bac2-545662159707.png)


Nesta tela, é possível cadastrar novos conhecimentos, informando o título, a descrição, a categoria a que o conhecimento pertence e o(s) autor(es) do conhecimento.
![image](https://user-images.githubusercontent.com/35302072/227380856-5043aa34-7eda-4609-8d0c-80079464ec97.png)


<h3>Tela de Criação de Categoria</h3>
Busca  de categorias
Paginação nos dados exibidos em cada uma das funcionalidades
![image](https://user-images.githubusercontent.com/35302072/227381145-ef689b0f-24e0-46cc-961d-17826f70cb82.png)


Nesta tela, é possível criar uma nova categoria. Basta informar o nome da categoria, descrição e salvar.
![image](https://user-images.githubusercontent.com/35302072/227380966-ba37ccb7-9b97-4aa1-a304-0299e2df11a8.png)


<h3>Tela de Criação de Autores</h3>
Busca  de categorias
Paginação nos dados exibidos em cada uma das funcionalidades
![image](https://user-images.githubusercontent.com/35302072/227381314-e31d75a3-7e10-40e2-a879-de10683338f0.png)


Nesta tela, é possível criar um novo autor para relacionar aos conhecimentos cadastrados. Basta informar o nome do autor e outras dados e salvar.
![image](https://user-images.githubusercontent.com/35302072/227381295-f3c03857-cabc-4c86-99a4-c73aa379ec5d.png)


<h3>Tela de Registro de Usuário</h3>
Ao fazer o registro, um autor com o nome do usuário é criado automaticamente.
![image](https://user-images.githubusercontent.com/35302072/227381398-c5bd4c79-93b1-4f15-908c-4e8dd7c3d16d.png)


<h3>Tela de Login de Usuário</h3>
Para acessar o sistema, é necessário fazer o login com um usuário e senha cadastrados previamente.
![image](https://user-images.githubusercontent.com/35302072/227381478-6a2c0590-2bf2-4bcf-80a3-807f04868e7a.png)


<h3>Busca</h3>
Em cada uma das telas, é possível realizar buscas por palavras-chave e termos. Os resultados são exibidos em uma lista.
![image](https://user-images.githubusercontent.com/35302072/227381519-2c781394-0c1b-423d-9a75-c84084bb0156.png)

<h3>Paginação</h3>
Em todas as listas de resultados, há uma paginação para melhorar a usabilidade da aplicação.
![image](https://user-images.githubusercontent.com/35302072/227381544-f3af2f88-3119-4342-b301-6470e65b0d93.png)


<h3>Conclusão</h3>
O Repositório de Conhecimento é um projeto simples e útil para quem busca organizar seus conhecimentos de forma eficiente. Esperamos que este projeto possa ajudá-lo a aprender e aprimorar suas habilidades em diversos assuntos do cotidiano.
