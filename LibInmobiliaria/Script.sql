CREATE DATABASE db_inmobiliaria;
GO

USE db_inmobiliaria;
GO



CREATE TABLE Roles
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Usuarios
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) NOT NULL UNIQUE,
    ClaveHash NVARCHAR(MAX) NOT NULL,
    ClaveSalt NVARCHAR(MAX) NOT NULL,
    Rol INT NOT NULL,

    CONSTRAINT FK_Usuarios_Roles
    FOREIGN KEY (Rol) REFERENCES Roles(Id)
);

CREATE TABLE [Historicos]
(
  [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,

  [Usuario] NVARCHAR(100) NULL,
  [Tabla] NVARCHAR(100) NULL,
  [Accion] NVARCHAR(50) NULL,
  [RegistroId] INT NULL,

  [Descripcion] NVARCHAR(500) NOT NULL,
  [Cambios] NVARCHAR(500) NULL,
  [ValorAnterior] NVARCHAR(1000) NULL,
  [ValorNuevo] NVARCHAR(1000) NULL,

  [Origen] NVARCHAR(100) NULL,
  [Exitoso] BIT NOT NULL DEFAULT 1,
  [Error] NVARCHAR(1000) NULL,

  [Fecha] SMALLDATETIME NOT NULL
);
GO

-- =========================================
-- TABLAS PADRES
-- =========================================

CREATE TABLE [EstadosCiviles]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE [Nacionalidades]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE [TiposContratos]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE [TiposPropiedades]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE [Departamentos]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(150) NOT NULL,
    [Estado] BIT NOT NULL,
    [FechaCreacion] DATE NOT NULL,
    [Poblacion] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE [Ciudades]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(150) NOT NULL,
    [Estado] BIT NOT NULL,
    [Poblacion] NVARCHAR(50) NOT NULL,
    [FechaCreacion] DATE NOT NULL,
    [CodigoPostal] NVARCHAR(10) NOT NULL,

    [Departamento] INT NOT NULL REFERENCES [Departamentos]([Id])
);
GO

CREATE TABLE [Sectores]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(200) NOT NULL,
    [Estado] BIT NOT NULL,
    [FechaCreacion] DATE NOT NULL,

    [Ciudad] INT NOT NULL REFERENCES [Ciudades]([Id])
);
GO

-- =========================================
-- PERSONAS: TABLA BASE
-- =========================================

CREATE TABLE [Personas]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Cedula] NVARCHAR(30) NOT NULL,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Apellido] NVARCHAR(100) NOT NULL,
    [Genero] NCHAR(1) NOT NULL,
    [Correo] NVARCHAR(150) NOT NULL,
    [FechaNacimiento] DATE NOT NULL,
    [FechaRegistro] DATETIME2 NOT NULL,

    [EstadoCivil] INT NOT NULL REFERENCES [EstadosCiviles]([Id]),
    [Nacionalidad] INT NOT NULL REFERENCES [Nacionalidades]([Id])
);
GO

-- =========================================
-- CAMPOS DE TRABAJADORES
-- =========================================

CREATE TABLE [Trabajadores]
(
    [Persona] INT PRIMARY KEY REFERENCES [Personas]([Id]),
    [Sueldo] DECIMAL(18,2) NOT NULL,
    [Estado] BIT NOT NULL,
    [Jornada] NVARCHAR(50) NOT NULL
);
GO

-- =========================================
-- CAMPOS DE AdministradoresDepartamentos
-- =========================================

CREATE TABLE [AdministradoresDepartamentos]
(
    [Persona] INT PRIMARY KEY REFERENCES [Trabajadores]([Persona]),
    [PresupuestoDepartamento] DECIMAL(18,2) NOT NULL,
    [Departamento] INT NOT NULL REFERENCES [Departamentos]([Id])
);
GO

CREATE UNIQUE INDEX [UX_AdministradoresDepartamentos_Departamento]
ON [AdministradoresDepartamentos]([Departamento]);
GO

-- =========================================
-- CAMPOS DE JefesSectores
-- =========================================

CREATE TABLE [JefesSectores]
(
    [Persona] INT PRIMARY KEY REFERENCES [Trabajadores]([Persona]),
    [PresupuestoSector] DECIMAL(18,2) NOT NULL,
    [AdministradorDepartamento] INT NOT NULL REFERENCES [AdministradoresDepartamentos]([Persona]),
    [Sector] INT NOT NULL REFERENCES [Sectores]([Id])
);
GO

CREATE UNIQUE INDEX [UX_JefesSectores_Sector]
ON [JefesSectores]([Sector]);
GO

-- =========================================
-- CAMPOS DE EmpleadosSectores
-- =========================================

CREATE TABLE [EmpleadosSectores]
(
    [Persona] INT PRIMARY KEY REFERENCES [Trabajadores]([Persona]),
    [JefeSector] INT NOT NULL REFERENCES [JefesSectores]([Persona]),
    [TipoContrato] INT NOT NULL REFERENCES [TiposContratos]([Id]),
    [Sector] INT NOT NULL REFERENCES [Sectores]([Id])
);
GO

-- =========================================
-- CAMPOS DE Clientes
-- =========================================

CREATE TABLE [Clientes]
(
    [Persona] INT PRIMARY KEY REFERENCES [Personas]([Id]),
    [PorcentajeComision] DECIMAL(18,2) NOT NULL,
    [CantidadContratos] INT NOT NULL,
    [PrioridadCliente] NVARCHAR(100) NOT NULL,
    [MotivoVenta] NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE [Codeudores]
(
    [Persona] INT PRIMARY KEY REFERENCES [Personas]([Id])
);
GO

CREATE TABLE [Compradores]
(
    [Persona] INT PRIMARY KEY REFERENCES [Personas]([Id])
);
GO

-- =========================================
-- TABLAS DEPENDIENTES DE PERSONAS
-- =========================================

CREATE TABLE [Telefonos]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Numero] NVARCHAR(30) NOT NULL,
    [Prefijo] NVARCHAR(10) NOT NULL,

    [Persona] INT NOT NULL REFERENCES [Personas]([Id])
);
GO

CREATE TABLE [Direcciones]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [TipoVia] NVARCHAR(50) NOT NULL,
    [Numero] NVARCHAR(50) NOT NULL,
    [Complemento] NVARCHAR(255) NOT NULL,

    [Persona] INT NOT NULL REFERENCES [Personas]([Id])
);
GO

CREATE TABLE [ExpedientesLaborales]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [NombreEmpresa] NVARCHAR(150) NOT NULL,
    [Cargo] NVARCHAR(100) NOT NULL,
    [SalarioPagado] DECIMAL(18,2) NOT NULL,
    [FechaIngreso] DATE NOT NULL,
    [FechaEgreso] DATE NOT NULL,
    [Desempeno] NVARCHAR(100) NOT NULL,
    [MotivoSalida] NVARCHAR(255) NOT NULL,

    [Persona] INT NOT NULL REFERENCES [Personas]([Id])
);
GO

CREATE TABLE [ExpedientesFinancieros]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Persona] INT NOT NULL REFERENCES [Clientes]([Persona])
);
GO

CREATE UNIQUE INDEX [UX_ExpedientesFinancieros_Persona]
ON [ExpedientesFinancieros]([Persona]);
GO

-- =========================================
-- BIENES: TABLA BASE
-- =========================================

CREATE TABLE [Bienes]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(255) NOT NULL,
    [FechaAdquisicion] DATE NOT NULL,
    [PrecioCompra] DECIMAL(18,2) NOT NULL,
    [ValorActual] DECIMAL(18,2) NOT NULL,

    [ExpedienteFinanciero] INT NOT NULL REFERENCES [ExpedientesFinancieros]([Id])
);
GO

-- =========================================
-- BienesMuebles
-- =========================================

CREATE TABLE [BienesMuebles]
(
    [Bien] INT PRIMARY KEY REFERENCES [Bienes]([Id]),
    [Tipo] NVARCHAR(100) NOT NULL,
    [Modelo] NVARCHAR(150) NOT NULL,
    [UbicacionActual] NVARCHAR(150) NOT NULL,
    [Garantia] NVARCHAR(100) NOT NULL,
    [Estado] NVARCHAR(100) NOT NULL
);
GO

-- =========================================
-- BienesInmuebles
-- =========================================

CREATE TABLE [BienesInmuebles]
(
    [Bien] INT PRIMARY KEY REFERENCES [Bienes]([Id]),
    [MetrosCuadrados] DECIMAL(18,2) NOT NULL,
    [Direccion] NVARCHAR(200) NOT NULL,
    [EstadoConservacion] NVARCHAR(100) NOT NULL,
    [Estrato] NVARCHAR(20) NOT NULL,
    [NumeroHabitaciones] NVARCHAR(20) NOT NULL,
    [NumeroBanos] NVARCHAR(20) NOT NULL,
    [CodigoCUC] NVARCHAR(50) NOT NULL,
    [EncargosDeudas] NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE [ActivosFinancieros]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [Nombre] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(255) NOT NULL,
    [FechaAdquisicion] DATE NOT NULL,
    [Precio] DECIMAL(18,2) NOT NULL,

    [ExpedienteFinanciero] INT NOT NULL REFERENCES [ExpedientesFinancieros]([Id])
);
GO

-- =========================================
-- PROPIEDADES Y CONTRATOS
-- =========================================

CREATE TABLE [Propiedades]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [NumeroHabitaciones] INT NOT NULL,
    [NumeroBanos] INT NOT NULL,
    [Patio] BIT NOT NULL,
    [Entradas] INT NOT NULL,
    [Pisos] INT NOT NULL,
    [AnioConstruccion] DATE NOT NULL,
    [ValorPropiedad] DECIMAL(18,2) NOT NULL,
    [ValorArriendo] DECIMAL(18,2) NOT NULL,
    [Estado] NVARCHAR(100) NOT NULL,
     -- Campos para mapa, pueden ser NULL
    [Direccion] NVARCHAR(200) NULL,
    [Latitud] DECIMAL(10,7) NULL,
    [Longitud] DECIMAL(10,7) NULL,

     -- Campo para imagen, puede ser NULL
    [Imagen] NVARCHAR(300) NULL,

    [Cliente] INT NOT NULL REFERENCES [Clientes]([Persona]),
    [TipoPropiedad] INT NOT NULL REFERENCES [TiposPropiedades]([Id])
);
GO

CREATE TABLE [Contratos]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [FechaContrato] DATE NOT NULL,
    [FechaFinalizacion] DATE NOT NULL,
    [Observaciones] NVARCHAR(255) NOT NULL,
    [PrecioAcordado] DECIMAL(18,2) NOT NULL,
    [ArriendoVenta] NVARCHAR(50) NOT NULL,

    [Cliente] INT NOT NULL REFERENCES [Clientes]([Persona]),
    [Propiedad] INT NOT NULL REFERENCES [Propiedades]([Id]),
    [Comprador] INT NOT NULL REFERENCES [Compradores]([Persona]),
    [JefeSector] INT NOT NULL REFERENCES [JefesSectores]([Persona])
);
GO

-- =========================================
-- TABLAS INTERMEDIAS N:N
-- =========================================

CREATE TABLE [EmpleadosCompradores]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [FechaAsesoramiento] DATE NOT NULL,

    [Comprador] INT NOT NULL REFERENCES [Compradores]([Persona]),
    [Empleado] INT NOT NULL REFERENCES [EmpleadosSectores]([Persona])
);
GO

CREATE TABLE [ContratosEmpleados]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [FechaCierre] DATE NOT NULL,
    [PrecioAcordado] DECIMAL(18,2) NOT NULL,
    [VendidaArrendada] NVARCHAR(50) NOT NULL,

    [Empleado] INT NOT NULL REFERENCES [EmpleadosSectores]([Persona]),
    [Contrato] INT NOT NULL REFERENCES [Contratos]([Id])
);
GO

CREATE TABLE [ContratosCodeudores]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [FechaCierre] DATE NOT NULL,
    [PrecioAcordado] DECIMAL(18,2) NOT NULL,
    [VendidaArrendada] NVARCHAR(50) NOT NULL,

    [Codeudor] INT NOT NULL REFERENCES [Codeudores]([Persona]),
    [Contrato] INT NOT NULL REFERENCES [Contratos]([Id])
);
GO

CREATE TABLE [CodeudoresCompradores]
(
    [Id] INT PRIMARY KEY IDENTITY (1,1),
    [FechaUnion] DATE NOT NULL,
    [Relacion] NVARCHAR(100) NOT NULL,

    [Comprador] INT NOT NULL REFERENCES [Compradores]([Persona]),
    [Codeudor] INT NOT NULL REFERENCES [Codeudores]([Persona])
);
GO

-- =========================================
-- TABLAS ROLES
-- =========================================

INSERT INTO Roles (Nombre) VALUES ('Admin');
INSERT INTO Roles (Nombre) VALUES ('Ejecutivo');
INSERT INTO Roles (Nombre) VALUES ('Guest');


-- =========================================
-- TABLAS MAESTRAS
-- =========================================

SET IDENTITY_INSERT [EstadosCiviles] ON;
INSERT INTO [EstadosCiviles] ([Id], [Nombre]) VALUES
(1, 'Soltero'),
(2, 'Casado'),
(3, 'Divorciado'),
(4, 'Viudo'),
(5, 'Union Libre');
SET IDENTITY_INSERT [EstadosCiviles] OFF;
GO

SET IDENTITY_INSERT [Nacionalidades] ON;
INSERT INTO [Nacionalidades] ([Id], [Nombre]) VALUES
(1, 'Colombiana'),
(2, 'Venezolana'),
(3, 'Peruana'),
(4, 'Mexicana'),
(5, 'Estadounidense');
SET IDENTITY_INSERT [Nacionalidades] OFF;
GO

SET IDENTITY_INSERT [TiposContratos] ON;
INSERT INTO [TiposContratos] ([Id], [Nombre]) VALUES
(1, 'Fijo'),
(2, 'Indefinido'),
(3, 'Obra o labor'),
(4, 'Prestacion de servicios'),
(5, 'Aprendizaje');
SET IDENTITY_INSERT [TiposContratos] OFF;
GO

SET IDENTITY_INSERT [TiposPropiedades] ON;
INSERT INTO [TiposPropiedades] ([Id], [Nombre]) VALUES
(1, 'Casa'),
(2, 'Apartamento'),
(3, 'Bodega'),
(4, 'Oficina'),
(5, 'Local comercial');
SET IDENTITY_INSERT [TiposPropiedades] OFF;
GO

-- =========================================
-- UBICACIONES
-- =========================================

SET IDENTITY_INSERT [Departamentos] ON;
INSERT INTO [Departamentos] ([Id], [Nombre], [Estado], [FechaCreacion], [Poblacion]) VALUES
(1, 'Antioquia', 1, '1826-06-25', '6912000'),
(2, 'Cundinamarca', 1, '1886-08-05', '3320000'),
(3, 'Valle del Cauca', 1, '1910-04-16', '4790000'),
(4, 'Atlantico', 1, '1905-04-11', '2860000'),
(5, 'Santander', 1, '1886-08-05', '2300000');
SET IDENTITY_INSERT [Departamentos] OFF;
GO

SET IDENTITY_INSERT [Ciudades] ON;
INSERT INTO [Ciudades] ([Id], [Nombre], [Estado], [Poblacion], [FechaCreacion], [CodigoPostal], [Departamento]) VALUES
(1, 'Medellin', 1, '2572000', '1616-03-02', '050001', 1),
(2, 'Bogota', 1, '7968000', '1538-08-06', '110111', 2),
(3, 'Cali', 1, '2228000', '1536-07-25', '760001', 3),
(4, 'Barranquilla', 1, '1327000', '1813-04-07', '080001', 4),
(5, 'Bucaramanga', 1, '607000', '1622-12-22', '680001', 5);
SET IDENTITY_INSERT [Ciudades] OFF;
GO

SET IDENTITY_INSERT [Sectores] ON;
INSERT INTO [Sectores] ([Id], [Nombre], [Estado], [FechaCreacion], [Ciudad]) VALUES
(1, 'Ventas residenciales Antioquia', 1, '2018-01-15', 1),
(2, 'Negocios corporativos Bogota', 1, '2018-03-20', 2),
(3, 'Inversion y arriendos Valle', 1, '2019-02-11', 3),
(4, 'Expansion comercial Caribe', 1, '2020-07-09', 4),
(5, 'Patrimonio y vivienda Santander', 1, '2021-05-30', 5);
SET IDENTITY_INSERT [Sectores] OFF;
GO

-- =========================================
-- PERSONAS: TABLA BASE
-- IDs:
-- 1-5   AdministradoresDepartamentos
-- 6-10  JefesSectores
-- 11-15 EmpleadosSectores
-- 16-20 Clientes
-- 21-25 Codeudores
-- 26-30 Compradores
-- =========================================

SET IDENTITY_INSERT [Personas] ON;
INSERT INTO [Personas]
(
    [Id], [Cedula], [Nombre], [Apellido], [Genero], [Correo], [FechaNacimiento], [FechaRegistro],
    [EstadoCivil], [Nacionalidad]
)
VALUES
(1, '1012345671', 'Carlos Andres', 'Restrepo Gomez', 'M', 'carlos.restrepo@inmocol.com', '1984-02-11', GETDATE(), 2, 1),
(2, '1012345672', 'Marcela', 'Rojas Alvarez', 'F', 'marcela.rojas@inmocol.com', '1987-09-05', GETDATE(), 2, 1),
(3, '1012345673', 'Javier', 'Montoya Ruiz', 'M', 'javier.montoya@inmocol.com', '1982-06-21', GETDATE(), 1, 1),
(4, '1012345674', 'Paola Andrea', 'Velez Mejia', 'F', 'paola.velez@inmocol.com', '1989-01-18', GETDATE(), 5, 1),
(5, '1012345675', 'Luis Fernando', 'Quintero Rios', 'M', 'luis.quintero@inmocol.com', '1985-11-30', GETDATE(), 2, 1),

(6, '1023456781', 'Ricardo', 'Cano Herrera', 'M', 'ricardo.cano@inmocol.com', '1988-03-14', GETDATE(), 2, 1),
(7, '1023456782', 'Diana Carolina', 'Morales Peña', 'F', 'diana.morales@inmocol.com', '1990-07-26', GETDATE(), 1, 1),
(8, '1023456783', 'Oscar Mauricio', 'Buitrago Silva', 'M', 'oscar.buitrago@inmocol.com', '1986-12-03', GETDATE(), 2, 1),
(9, '1023456784', 'Tatiana', 'Pineda Castro', 'F', 'tatiana.pineda@inmocol.com', '1991-05-09', GETDATE(), 5, 1),
(10, '1023456785', 'Andres Felipe', 'Jaimes Duarte', 'M', 'andres.jaimes@inmocol.com', '1987-08-17', GETDATE(), 2, 1),

(11, '1034567891', 'Natalia', 'Suarez Henao', 'F', 'natalia.suarez@inmocol.com', '1996-04-12', GETDATE(), 1, 1),
(12, '1034567892', 'Juan Camilo', 'Pardo Torres', 'M', 'juan.pardo@inmocol.com', '1995-10-22', GETDATE(), 1, 1),
(13, '1034567893', 'Melissa', 'Cardona Arias', 'F', 'melissa.cardona@inmocol.com', '1997-01-19', GETDATE(), 5, 1),
(14, '1034567894', 'Kevin', 'Salazar Lopez', 'M', 'kevin.salazar@inmocol.com', '1998-02-03', GETDATE(), 1, 1),
(15, '1034567895', 'Laura Sofia', 'Meneses Gil', 'F', 'laura.meneses@inmocol.com', '1999-09-28', GETDATE(), 1, 1),

(16, '1045678901', 'Marta Lucia', 'Gomez Arango', 'F', 'marta.gomez@gmail.com', '1979-05-14', GETDATE(), 2, 1),
(17, '1045678902', 'Hernando', 'Perez Ospina', 'M', 'hernando.perez@gmail.com', '1975-08-22', GETDATE(), 2, 1),
(18, '1045678903', 'Juliana', 'Torres Ruiz', 'F', 'juliana.torres@gmail.com', '1988-03-30', GETDATE(), 5, 1),
(19, '1045678904', 'Santiago', 'Molina Franco', 'M', 'santiago.molina@gmail.com', '1983-11-09', GETDATE(), 1, 1),
(20, '1045678905', 'Claudia Patricia', 'Ramirez Soto', 'F', 'claudia.ramirez@gmail.com', '1990-01-27', GETDATE(), 2, 1),

(21, '1056789011', 'Jose Manuel', 'Rivera Salcedo', 'M', 'jose.rivera@gmail.com', '1981-04-08', GETDATE(), 2, 1),
(22, '1056789012', 'Paula', 'Londoño Jaramillo', 'F', 'paula.londono@gmail.com', '1989-09-12', GETDATE(), 2, 1),
(23, '1056789013', 'Camilo', 'Galvis Serrano', 'M', 'camilo.galvis@gmail.com', '1992-07-15', GETDATE(), 1, 1),
(24, '1056789014', 'Valeria', 'Acosta Mejia', 'F', 'valeria.acosta@gmail.com', '1994-12-01', GETDATE(), 5, 2),
(25, '1056789015', 'Miguel Angel', 'Cabrera Rojas', 'M', 'miguel.cabrera@gmail.com', '1986-10-10', GETDATE(), 2, 5),

(26, '1067890121', 'Sebastian', 'Jimenez Cardona', 'M', 'sebastian.jimenez@gmail.com', '1991-02-13', GETDATE(), 1, 1),
(27, '1067890122', 'Daniela', 'Murillo Peña', 'F', 'daniela.murillo@gmail.com', '1993-06-25', GETDATE(), 1, 1),
(28, '1067890123', 'Alejandro', 'Gaviria Ruiz', 'M', 'alejandro.gaviria@gmail.com', '1987-11-18', GETDATE(), 2, 1),
(29, '1067890124', 'Carolina', 'Sierra Lopez', 'F', 'carolina.sierra@gmail.com', '1995-04-21', GETDATE(), 5, 3),
(30, '1067890125', 'Thomas', 'Johnson Miller', 'M', 'thomas.johnson@gmail.com', '1988-09-04', GETDATE(), 2, 5);
SET IDENTITY_INSERT [Personas] OFF;
GO

-- =========================================
-- CAMPOS DE TRABAJADORES
-- =========================================

INSERT INTO [Trabajadores] ([Persona], [Sueldo], [Estado], [Jornada]) VALUES
(1, 8500000, 1, 'Tiempo completo'),
(2, 9200000, 1, 'Tiempo completo'),
(3, 8800000, 1, 'Tiempo completo'),
(4, 9000000, 1, 'Tiempo completo'),
(5, 8700000, 1, 'Tiempo completo'),
(6, 5200000, 1, 'Tiempo completo'),
(7, 5400000, 1, 'Tiempo completo'),
(8, 5100000, 1, 'Tiempo completo'),
(9, 5300000, 1, 'Tiempo completo'),
(10, 5000000, 1, 'Tiempo completo'),
(11, 2600000, 1, 'Mixta'),
(12, 2550000, 1, 'Diurna'),
(13, 2450000, 1, 'Mixta'),
(14, 2500000, 1, 'Diurna'),
(15, 2400000, 1, 'Diurna');
GO

-- =========================================
-- CAMPOS DE AdministradoresDepartamentos
-- =========================================

INSERT INTO [AdministradoresDepartamentos] ([Persona], [PresupuestoDepartamento], [Departamento]) VALUES
(1, 250000000.00, 1),
(2, 320000000.00, 2),
(3, 210000000.00, 3),
(4, 175000000.00, 4),
(5, 150000000.00, 5);
GO

-- =========================================
-- CAMPOS DE JefesSectores
-- =========================================

INSERT INTO [JefesSectores] ([Persona], [PresupuestoSector], [AdministradorDepartamento], [Sector]) VALUES
(6, 80000000.00, 1, 1),
(7, 120000000.00, 2, 2),
(8, 76000000.00, 3, 3),
(9, 68000000.00, 4, 4),
(10, 59000000.00, 5, 5);
GO

-- =========================================
-- CAMPOS DE EmpleadosSectores
-- =========================================

INSERT INTO [EmpleadosSectores] ([Persona], [JefeSector], [TipoContrato], [Sector]) VALUES
(11, 6, 2, 1),
(12, 7, 1, 2),
(13, 8, 3, 3),
(14, 9, 4, 4),
(15, 10, 5, 5);
GO

-- =========================================
-- CAMPOS DE Clientes
-- =========================================

INSERT INTO [Clientes] ([Persona], [PorcentajeComision], [CantidadContratos], [PrioridadCliente], [MotivoVenta]) VALUES
(16, 3.00, 2, 'Alta', 'Venta de apartamento para mudanza familiar'),
(17, 2.80, 3, 'Media', 'Cambio de ciudad por trabajo'),
(18, 3.20, 1, 'Alta', 'Inversion en vivienda urbana'),
(19, 2.50, 4, 'Baja', 'Necesidad de liquidez'),
(20, 3.10, 2, 'Alta', 'Compra de vivienda mas grande');
GO

INSERT INTO [Codeudores] ([Persona]) VALUES
(21), (22), (23), (24), (25);
GO

INSERT INTO [Compradores] ([Persona]) VALUES
(26), (27), (28), (29), (30);
GO

-- =========================================
-- TABLAS DEPENDIENTES DE PERSONAS
-- =========================================

SET IDENTITY_INSERT [Telefonos] ON;
INSERT INTO [Telefonos] ([Id], [Numero], [Prefijo], [Persona]) VALUES
(1, '3001234567', '+57', 1),
(2, '3012345678', '+57', 2),
(3, '3023456789', '+57', 3),
(4, '3034567890', '+57', 4),
(5, '3045678901', '+57', 5),
(6, '3056789012', '+57', 16),
(7, '3067890123', '+57', 17),
(8, '3078901234', '+57', 18),
(9, '987654321', '+51', 24),
(10, '5558675309', '+1', 30);
SET IDENTITY_INSERT [Telefonos] OFF;
GO

SET IDENTITY_INSERT [Direcciones] ON;
INSERT INTO [Direcciones] ([Id], [TipoVia], [Numero], [Complemento], [Persona]) VALUES
(1, 'Carrera', '43A #12-45', 'Apartamento 802', 1),
(2, 'Calle', '116 #18-32', 'Oficina 401', 2),
(3, 'Avenida', '6N #28-15', 'Torre empresarial', 3),
(4, 'Carrera', '52 #74-90', 'Casa esquinera', 4),
(5, 'Calle', '36 #21-14', 'Edificio residencial', 5),
(6, 'Carrera', '70 #10-22', 'Apartamento 504', 16),
(7, 'Calle', '140 #12-56', 'Casa 7 conjunto cerrado', 17),
(8, 'Avenida', '3 Oeste #15-21', 'Apartamento 1203', 18),
(9, 'Carrera', '45 #93-18', 'Apartamento 906', 19),
(10, 'Calle', '56 #31-08', 'Casa bifamiliar', 20);
SET IDENTITY_INSERT [Direcciones] OFF;
GO

SET IDENTITY_INSERT [ExpedientesLaborales] ON;
INSERT INTO [ExpedientesLaborales]
(
    [Id], [NombreEmpresa], [Cargo], [SalarioPagado], [FechaIngreso], [FechaEgreso], [Desempeno], [MotivoSalida], [Persona]
)
VALUES
(1, 'Grupo Sura', 'Analista comercial', 3200000, '2018-02-01', '2020-12-15', 'Sobresaliente', 'Mejora profesional', 1),
(2, 'Constructora Bolivar', 'Coordinadora de ventas', 4100000, '2019-03-10', '2022-05-30', 'Excelente', 'Ascenso en otra empresa', 2),
(3, 'Argos', 'Lider de proyectos', 5000000, '2017-01-15', '2021-11-20', 'Muy bueno', 'Cambio de sector', 3),
(4, 'Tecnoquimicas', 'Jefe regional', 4700000, '2020-06-01', '2023-04-28', 'Excelente', 'Cambio de residencia', 4),
(5, 'Bancolombia', 'Ejecutivo pyme', 3900000, '2018-08-13', '2022-09-10', 'Bueno', 'Nuevo reto laboral', 5);
SET IDENTITY_INSERT [ExpedientesLaborales] OFF;
GO

SET IDENTITY_INSERT [ExpedientesFinancieros] ON;
INSERT INTO [ExpedientesFinancieros] ([Id], [Persona]) VALUES
(1, 16),
(2, 17),
(3, 18),
(4, 19),
(5, 20);
SET IDENTITY_INSERT [ExpedientesFinancieros] OFF;
GO

-- =========================================
-- BIENES Y ACTIVOS
-- =========================================

SET IDENTITY_INSERT [Bienes] ON;
INSERT INTO [Bienes]
(
    [Id], [Nombre], [Descripcion], [FechaAdquisicion], [PrecioCompra], [ValorActual], [ExpedienteFinanciero]
)
VALUES
(1, 'Camioneta Mazda CX-5', 'Vehiculo familiar usado para desplazamientos en Medellin', '2021-06-15', 115000000, 98000000, 1),
(2, 'Portafolio oficina Lenovo', 'Equipo de computo para gestion de cartera', '2023-02-10', 4200000, 3300000, 2),
(3, 'Apartamento Laureles', 'Inmueble residencial de inversion en Medellin', '2019-09-22', 380000000, 520000000, 3),
(4, 'Local en Cabecera', 'Local comercial en Bucaramanga', '2020-04-18', 290000000, 360000000, 4),
(5, 'Bodega Yumbo', 'Bodega industrial para almacenamiento', '2018-11-05', 720000000, 910000000, 5);
SET IDENTITY_INSERT [Bienes] OFF;
GO

INSERT INTO [BienesMuebles] ([Bien], [Tipo], [Modelo], [UbicacionActual], [Garantia], [Estado]) VALUES
(1, 'Vehiculo', 'Mazda CX-5 Grand Touring', 'Parqueadero privado Medellin', 'Vencida', 'Operativo'),
(2, 'Tecnologico', 'Lenovo ThinkPad E15', 'Oficina principal Bogota', '1 año', 'Bueno');
GO

INSERT INTO [BienesInmuebles]
(
    [Bien], [MetrosCuadrados], [Direccion], [EstadoConservacion], [Estrato], [NumeroHabitaciones], [NumeroBanos], [CodigoCUC], [EncargosDeudas]
)
VALUES
(3, 96.50, 'Carrera 76 #33A-45, Medellin', 'Excelente', '5', '3', '2', 'CUC-ANT-001', 'Libre de gravamen'),
(4, 54.00, 'Carrera 34 #52-18, Bucaramanga', 'Bueno', '5', 'N/A', '1', 'CUC-SAN-002', 'Hipoteca vigente'),
(5, 420.00, 'Zona industrial de Yumbo, lote 12', 'Bueno', '3', 'N/A', '2', 'CUC-VAL-003', 'Sin afectaciones');
GO

SET IDENTITY_INSERT [ActivosFinancieros] ON;
INSERT INTO [ActivosFinancieros]
(
    [Id], [Nombre], [Descripcion], [FechaAdquisicion], [Precio], [ExpedienteFinanciero]
)
VALUES
(1, 'CDT Bancolombia', 'Inversion a termino fijo en entidad bancaria colombiana', '2023-01-12', 25000000, 1),
(2, 'Fondo de inversion colectiva', 'Participacion en portafolio moderado', '2022-08-05', 18000000, 2),
(3, 'TES Colombia', 'Titulos de deuda publica nacional', '2021-06-20', 40000000, 3),
(4, 'ETF regional', 'Fondo indexado de mercado latinoamericano', '2024-02-10', 12000000, 4),
(5, 'Cuenta de ahorro programado', 'Reserva de liquidez para compra de vivienda', '2023-09-15', 9000000, 5);
SET IDENTITY_INSERT [ActivosFinancieros] OFF;
GO

-- =========================================
-- PROPIEDADES Y CONTRATOS
-- =========================================

SET IDENTITY_INSERT [Propiedades] ON;
INSERT INTO [Propiedades]
(
    [Id], [NumeroHabitaciones], [NumeroBanos], [Patio], [Entradas], [Pisos], [AnioConstruccion],
    [ValorPropiedad], [ValorArriendo], [Estado], [Cliente], [TipoPropiedad]
)
VALUES
(1, 3, 2, 0, 1, 1, '2016-01-01', 420000000, 2400000, 'Disponible', 16, 2),
(2, 4, 3, 1, 2, 2, '2012-01-01', 680000000, 3500000, 'En negociacion', 17, 1),
(3, 0, 1, 0, 1, 1, '2019-01-01', 310000000, 2800000, 'Disponible', 18, 5),
(4, 2, 2, 0, 1, 1, '2020-01-01', 390000000, 2200000, 'Reservada', 19, 2),
(5, 0, 1, 0, 2, 1, '2015-01-01', 950000000, 6200000, 'Disponible', 20, 3);
SET IDENTITY_INSERT [Propiedades] OFF;
GO

SET IDENTITY_INSERT [Contratos] ON;
INSERT INTO [Contratos]
(
    [Id], [FechaContrato], [FechaFinalizacion], [Observaciones], [PrecioAcordado], [ArriendoVenta],
    [Cliente], [Propiedad], [Comprador], [JefeSector]
)
VALUES
(1, '2024-02-15', '2025-02-14', 'Arriendo de apartamento en excelente zona residencial', 2400000, 'Arriendo', 16, 1, 26, 6),
(2, '2024-05-10', '2024-12-31', 'Promesa de compraventa de casa familiar en Bogota', 655000000, 'Venta', 17, 2, 27, 7),
(3, '2024-03-08', '2025-03-07', 'Contrato de local comercial con opcion de renovacion', 2750000, 'Arriendo', 18, 3, 28, 8),
(4, '2024-07-01', '2025-06-30', 'Arriendo de apartamento para inversionista joven', 2200000, 'Arriendo', 19, 4, 29, 9),
(5, '2024-01-20', '2024-11-30', 'Negociacion de bodega para operacion logistica', 930000000, 'Venta', 20, 5, 30, 10);
SET IDENTITY_INSERT [Contratos] OFF;
GO

-- =========================================
-- TABLAS INTERMEDIAS N:N
-- =========================================

SET IDENTITY_INSERT [EmpleadosCompradores] ON;
INSERT INTO [EmpleadosCompradores] ([Id], [FechaAsesoramiento], [Comprador], [Empleado]) VALUES
(1, '2024-01-18', 26, 11),
(2, '2024-02-22', 27, 12),
(3, '2024-03-01', 28, 13),
(4, '2024-04-14', 29, 14),
(5, '2024-05-09', 30, 15);
SET IDENTITY_INSERT [EmpleadosCompradores] OFF;
GO

SET IDENTITY_INSERT [ContratosEmpleados] ON;
INSERT INTO [ContratosEmpleados] ([Id], [FechaCierre], [PrecioAcordado], [VendidaArrendada], [Empleado], [Contrato]) VALUES
(1, '2024-02-15', 2400000, 'Arrendada', 11, 1),
(2, '2024-05-10', 655000000, 'Vendida', 12, 2),
(3, '2024-03-08', 2750000, 'Arrendada', 13, 3),
(4, '2024-07-01', 2200000, 'Arrendada', 14, 4),
(5, '2024-01-20', 930000000, 'Vendida', 15, 5);
SET IDENTITY_INSERT [ContratosEmpleados] OFF;
GO

SET IDENTITY_INSERT [ContratosCodeudores] ON;
INSERT INTO [ContratosCodeudores] ([Id], [FechaCierre], [PrecioAcordado], [VendidaArrendada], [Codeudor], [Contrato]) VALUES
(1, '2024-02-15', 2400000, 'Arrendada', 21, 1),
(2, '2024-05-10', 655000000, 'Vendida', 22, 2),
(3, '2024-03-08', 2750000, 'Arrendada', 23, 3),
(4, '2024-07-01', 2200000, 'Arrendada', 24, 4),
(5, '2024-01-20', 930000000, 'Vendida', 25, 5);
SET IDENTITY_INSERT [ContratosCodeudores] OFF;
GO

SET IDENTITY_INSERT [CodeudoresCompradores] ON;
INSERT INTO [CodeudoresCompradores] ([Id], [FechaUnion], [Relacion], [Comprador], [Codeudor]) VALUES
(1, '2024-01-10', 'Hermano', 26, 21),
(2, '2024-02-01', 'Conyuge', 27, 22),
(3, '2024-02-20', 'Socio', 28, 23),
(4, '2024-03-15', 'Prima', 29, 24),
(5, '2024-04-10', 'Amigo', 30, 25);
SET IDENTITY_INSERT [CodeudoresCompradores] OFF;
GO

SELECT COUNT(*) AS Personas FROM Personas;
SELECT COUNT(*) AS Trabajadores FROM Trabajadores;
SELECT COUNT(*) AS Clientes FROM Clientes;
SELECT COUNT(*) AS Compradores FROM Compradores;
SELECT COUNT(*) AS Codeudores FROM Codeudores;
SELECT COUNT(*) AS Bienes FROM Bienes;
SELECT COUNT(*) AS BienesMuebles FROM BienesMuebles;
SELECT COUNT(*) AS BienesInmuebles FROM BienesInmuebles;
SELECT COUNT(*) AS Contratos FROM Contratos;

