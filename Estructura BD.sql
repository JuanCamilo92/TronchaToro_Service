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