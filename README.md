# Livl reviews api

## Install dev database

- You need to have docker installed and running
- Navigate to the postgres-compose folder
- Run `docker-compose up -d`
- The database will be available at `localhost:5432`
- The default credentials are displayed in the docker-compose file.
- Theses default credentials are also used in the application configuration file (appsettings.json) at the root of the LivlReviewsApi project.s

## Migrations

- To create a migration you can use the Rider's integrated tools by right clicking on the LivlReviews Api project, then "Entity Framework Core" -> "Add Migration". You can manage everything else from there.
- Otherwise, figure it out yourself with visual studio. You can also use the dotnet ef command line tool. (maybe you will need to install it with `dotnet tool install --global dotnet-ef`)
- You can also watch [this video](https://youtu.be/z7G6HV7WWz0?si=UtG2vFA434Mu8dFu&t=337) who will explain how to create new tables and do the migrations in visual studio.

## Use Mailhog for testing emails

- You need to have docker installed and running
- Go in the folder "mailhog" at the root of the project and run `docker-compose up -d`
- The mailhog interface will be available at `localhost:8025`
- The SMTP settings are already configured in the `appsettings.json` file at the root of the LivlReviewsApi project.

> For the develpment, the server is "localhost" and the port is "1025".


## Setup SMTP server (development)

The password should be secret and not stored in the source code. To do this, we can use the `dotnet user-secrets` tool.

- Run the following command **in the LivlReviews.Api project directory** to init secrets :
```
dotnet user-secrets init
```

- Then run the following command to set the password :
```
dotnet user-secrets set "Smtp:Password" "your_password_here"
```



