--USE MASTER;

/*CREATE DATABASE PROPINA_CARRASCO;
GO*/

USE PROPINA_CARRASCO;
GO

DROP TABLE dbo.PLANILLAEMPLEADO;
DROP TABLE dbo.PLANILLA;
DROP TABLE dbo.ADMINISTRADOR;
DROP TABLE dbo.EMPLEADO;
DROP TABLE dbo.USUARIO;

CREATE TABLE dbo.USUARIO
(
	Id	INT  NOT NULL IDENTITY(1,1),	
	Nombre NVARCHAR(30) NOT NULL,
	Apellido NVARCHAR(30) NOT NULL,
	NombreUsuario NVARCHAR(50) NOT NULL,
	Contrasenia NVARCHAR(MAX) NOT NULL,
	Habilitado BIT NOT NULL,
	Tipo NVARCHAR(20) NOT NULL,
	
	Email NVARCHAR(50),
	Telefono NVARCHAR(20),
	Direccion NVARCHAR (100),

	CONSTRAINT PK_USUARIO PRIMARY KEY(Id),
	CONSTRAINT UK_NombreUsuario_USUARIO UNIQUE(NombreUsuario),
	CONSTRAINT CK_COL_Tipo_InValores_TAB_USUARIO CHECK (Tipo in('EMPLEADO', 'ADMINISTRADOR'))
);
GO

CREATE TABLE dbo.EMPLEADO
(
	EmpleadoId INT  NOT NULL,	
	NumeroEmpleado INT NOT NULL,

	CONSTRAINT PK_EMPLEADO PRIMARY KEY(EmpleadoId),
	CONSTRAINT UK_NumeroEmpleado_EMPLEADO UNIQUE(NumeroEmpleado),
	CONSTRAINT FK_UsuarioId_EMPLEADO FOREIGN KEY (EmpleadoId) REFERENCES dbo.USUARIO (Id)
);
GO

CREATE TABLE dbo.ADMINISTRADOR
(
	UsuarioId INT  NOT NULL,

	CONSTRAINT PK_ADMINISTRADOR PRIMARY KEY(UsuarioId),
	CONSTRAINT FK_UsuarioId_ADMINISTRADOR FOREIGN KEY (UsuarioId) REFERENCES dbo.USUARIO (Id)
);
GO

CREATE TABLE dbo.PLANILLA
(
	Id	INT  NOT NULL IDENTITY(1,1),
	Texto NVARCHAR(500) NOT NULL,
	FechaAlta DATETIME NOT NULL,
	IdEmpleado INT NOT NULL,
	Habilitada BIT NOT NULL,

	CONSTRAINT PK_PLANILLA PRIMARY KEY(Id),
	CONSTRAINT FK_EmpleadoId_PLANILLA FOREIGN KEY (IdEmpleado) REFERENCES dbo.EMPLEADO (EmpleadoId)
);
GO

CREATE TABLE dbo.PLANILLAEMPLEADO
(
	EmpleadoId INT  NOT NULL,
	PlanillaId INT  NOT NULL,

	CONSTRAINT PK_PLANILLAEMPLEADO PRIMARY KEY(EmpleadoId, PlanillaId),
	CONSTRAINT FK_EmpleadoId_PLANILLAEMPLEADO FOREIGN KEY (EmpleadoId) REFERENCES dbo.EMPLEADO (EmpleadoId),
	CONSTRAINT FK_PlanillaId_PLANILLAEMPLEADO FOREIGN KEY (PlanillaId) REFERENCES dbo.PLANILLA (Id)
);
GO

INSERT INTO dbo.USUARIO VALUES
/*1 - Admin*/('Admin1', 'Admin1', 'Admin1','123456789', 1, 'ADMINISTRADOR', 'Admin1@mailinator.com', '099999999', 'Admin dir'),
/*2 - Admin*/('Admin2', 'Admin2', 'Admin2','123456789', 1, 'ADMINISTRADOR', 'Admin2@mailinator.com', '099999999', 'Admin dir'),

/*3 - Empleado*/('Empleado1', 'Empleado1', 'Empleado1','123456789', 1, 'EMPLEADO', 'Empleado1@mailinator.com', '099999999', 'Empleado1 dir'),
/*4 - Empleado*/('Empleado2', 'Empleado2', 'Empleado2','123456789', 1, 'EMPLEADO', 'Empleado2@mailinator.com', '099999999', 'Empleado2 dir');

INSERT INTO dbo.ADMINISTRADOR VALUES
(1),
(2);

INSERT INTO dbo.EMPLEADO Values
(3,100),
(4,101);

INSERT INTO dbo.PLANILLA Values
('Texto 1',getdate(),3,1),
('Texto 2',getdate(),4,1);

SELECT * FROM USUARIO;
SELECT * FROM EMPLEADO;
SELECT * FROM ADMINISTRADOR;
SELECT * FROM PLANILLA;