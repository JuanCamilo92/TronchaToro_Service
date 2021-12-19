Create Table Rol(
id int primary key identity,
Description varchar(100)
)
GO
Create Table Usuarios(
Email varchar(200) primary key,
Password varchar(4000),
Rol_id int,
Name varchar(100),
LastName varchar(100),
Age varchar(10),
Address varchar(4000),
PhoneNumber varchar(100),
imageId varchar(4000),
imageFullPath varchar(4000)
)

GO

CREATE TABLE Comidas(
Id int primary key identity,
Description varchar(100),
Price numeric(10,2)
)
GO
CREATE TABLE FotosComidas(
Id int primary key identity,
Food_Id int,
ImagePath Varchar(4000)
)