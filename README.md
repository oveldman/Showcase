# Showcase

Welcome!

## Quick start Client

1. Install NodeJs from [NodeJs Official Page](https://nodejs.org/en).
2. Open Terminal
3. Go to your file project
4. Run in terminal: ```npm install -g @angular/cli```
5. Then: ```npm install```
6. And: ```ng serve```
7. Navigate to: [http://localhost:4200/](http://localhost:4200/)

## Quick start Server

### Dotnet

1. Open Terminal
2. Run in terminal: ```cd Server/Web```
3. Then: ```dotnet run```
4. Navigate to: [https://localhost:5001/](https://localhost:5001/)

### Docker

1. Install Docker
2. Open Terminal
3. Run in terminal:```docker pull postgres```
4. Then: ```docker run --name ShowCaseDB -e POSTGRES_USERNAME=postgres -e POSTGRES_PASSWORD=mysecretpassword -d -p 5432:5432 postgres```
5. And: ```dotnet ef database update```
6. Add new migration: ```dotnet ef migrations add {{name}}```

## Thank you!
[Bootstrap theme made by creative-tim](https://github.com/creativetimofficial/black-dashboard-angular "Theme's Github page")