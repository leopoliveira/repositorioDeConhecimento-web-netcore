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
Acesse o aplicativo no seu navegador, em https://localhost:5001/.

<h2>Funcionalidades</h2>
O Repositório de Conhecimento possui as seguintes funcionalidades:

<h3>Tela de Cadastro de Conhecimentos</h3>
Criação de categoria
Criação de autores
Login de usuário
Busca em cada uma das funcionalidades
Paginação nos dados exibidos em cada uma das funcionalidades
Tela de Cadastro de Conhecimentos
Nesta tela, é possível cadastrar novos conhecimentos, informando o título, a descrição, a categoria a que o conhecimento pertence e o(s) autor(es) do conhecimento.

<h3>Tela de Cadastro de Conhecimentos</h3>

<h3>Tela de Criação de Categoria</h3>
Nesta tela, é possível criar uma nova categoria para organizar os conhecimentos cadastrados. Basta informar o nome da categoria e salvar.

<h3>Tela de Criação de Categoria</h3>

<h3>Tela de Criação de Autores</h3>
Nesta tela, é possível criar um novo autor para relacionar aos conhecimentos cadastrados. Basta informar o nome do autor e salvar.

<h3>Tela de Login de Usuário</h3>
Para acessar o sistema, é necessário fazer o login com um usuário e senha cadastrados previamente. Ao fazer o login, um autor com o nome do usuário é criado automaticamente.

<h3>Tela de Login de Usuário</h3>

<h3>Busca</h3>
Em cada uma das telas, é possível realizar buscas por palavras-chave e termos. Os resultados são exibidos em uma lista paginada.

<h3>Paginação</h3>
Em todas as listas de resultados, há uma paginação para melhorar a usabilidade da aplicação.

<h3>Conclusão</h3>
O Repositório de Conhecimento é um projeto simples e útil para quem busca organizar seus conhecimentos de forma eficiente. Esperamos que este projeto possa ajudá-lo a aprender e aprimorar suas habilidades em .Net e outras tecnologias utilizadas neste projeto
