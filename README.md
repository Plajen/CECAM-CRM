# CECAM CRM
Small CRM full stack project using .NET Core 6.0 Web API + SQL Server + AngularJS to perform CRUD operations on a customer base.

Como rodar o projeto (Windows):
* Primeiramente clone o repositório em uma pasta local

Back End + DB:
1) Crie uma database nova pelo SQL Server Management Studio (ex: 'cecamdb', em um servidor com username 'sa' e senha '123abc')
2) Abra a solução CECAM.API.sln pelo Visual Studio 2019+
3) Dentro do projeto CECAM.API, abra o arquivo appsettings.json e edite a Connection String de acordo com a nova database criada
  Ex: "Data Source=localhost;Initial Catalog=cecamdb;User ID=sa;Password=123abc;TrustServerCertificate=true"
4) Abra um Developer PowerShell no projeto CECAM.Infra
5) Digite 'dotnet ef database update --startup-project ../CECAM.API/' e dê ENTER (isso aplicará a Migration "init" no banco de dados criado e configurado na Connection String)
6) Pressione F5 para buildar e rodar o projeto de back end (certifique-se de estar em modo Debug e com o projeto CECAM.API como inicializador)

Front End:
1) Idealmente pelo Visual Studio Code, abra a pasta no caminho CECAM.API/CECAM.Web/
2) Pressione CTRL+SHIFT+' ou vá até Terminal>New Terminal para abrir um Windows PowerShell
3) Digite 'npm start' e dê ENTER para instalar todas as dependências do projeto (listadas em package.json) e então disponibilizar a aplicação front end em localhost:8000
  Obs: o comando 'npm start' está definido para executar também 'npm install', o que irá, além de instalar os pacotes necessários, copiar determinadas libs de node_modules para a pasta lib
4) Por estar utilizando 'watch-http-server', é possível realizar mudanças em tempo real (aplicadas ao salvar) no projeto de front end, com hot reload

* Se estiver rodando o projeto de back end em outra porta que não seja a 7031, deverá alterar as URLs no projeto de front end, em CECAM.Web/app/core/factories/customerFactory.js, em cada um dos métodos
