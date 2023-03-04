## Minimal ASP.NET CRUD API with Dapper

This project is a minimal API (2 class) made in ASP.NET to perform CRUD type actions on a SQL Server database. 

### Built With

* ASP.NET Web API
* Dapper (package)
* SqlClient (package)

## Getting Started

### Prerequisites

* Sql Server
* MSSMS (Sql server managment)

### Installation

1. Create the table in the database with some mock data
   ```sh
     CREATE TABLE CrewTable(
       id INTEGER PRIMARY KEY IDENTITY,
       name VARCHAR(50) not null,
       description VARCHAR(200) not null,
       dateOfCreation DATETIME CONSTRAINT Default_date_creation DEFAULT GETDATE(),
       isStillActif BIT CONSTRAINT Default_isStillActif_value DEFAULT 1
     );
  
     CREATE TABLE UserTable(
       id INTEGER PRIMARY KEY IDENTITY,
       username VARCHAR(50) not null,
       gender VARCHAR(5) not null,
       birthDate DATETIME,
       birthPlace VARCHAR(75),
       email VARCHAR(75) not null,
       crewId INTEGER not null,
       CONSTRAINT FK_PersonOrder FOREIGN KEY (crewId) REFERENCES CrewTable(id)
     );
  
     INSERT INTO CrewTable(name, description) VALUES('Athena', 'Gather the members of the athena crew');
     INSERT INTO CrewTable(name, description) VALUES('The Angels', 'Gather the members of the angels crew');

     INSERT INTO UserTable(username, gender, birthDate, birthPlace, email, crewId) VALUES ('GoodMan', 'Man', GETDATE(), 'Paris', 'GoodMan@gmail.com', 2);
     INSERT INTO UserTable(username, gender, birthDate, birthPlace, email, crewId) VALUES ('Lalo', 'Man', GETDATE(), 'Bordeaux', 'Lalo@gmail.com', 1);
     INSERT INTO UserTable(username, gender, birthDate, birthPlace, email, crewId) VALUES ('Laila', 'Woman', GETDATE(), 'Brest', 'Laila@gmail.com', 2);
     INSERT INTO UserTable(username, gender, birthDate, birthPlace, email, crewId) VALUES ('Marie-Thérèse', 'Woman', GETDATE(), 'Rennes', 'Marie-Thérèse@gmail.com',1);
   ```

2. Clone the repo
   ```sh
   git clone https://github.com/ebalcon/CrudApiWithDapper.git
   ```
3. Change the ConnectionString in appsettings.json
   ```sh
   "DefaultConnection" : "Data Source=YOUR_INSTANCE;Initial Catalog=YOUR_DATABASE;Integrated Security=True"
   ```

## Contact

[2]: https://www.linkedin.com/in/ebalcon

[![linkedin](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)][2]

Project Link: https://github.com/ebalcon/Crud-Api-With-Dapper
   
