CREATE DATABASE PersonasDb
GO
USE PersonasDb
GO
CREATE TABLE Personas
(
	PersonaId int primary key identity(1,1),
	Nombres varchar,
	Cedula varchar,
	Fecha DateTime,
	Telefono varchar,
	Direccion varchar,
	Comentarios varchar
);
