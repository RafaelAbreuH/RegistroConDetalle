CREATE DATABASE GruposDb
go
use GruposDb
go
CREATE TABLE Grupos
(
	GrupoId int primary key identity(1,1),
	Fecha DateTime,
	Descripcion varchar,
	Cantidad int,
	Grupo int,
	Integrantes int,

);
