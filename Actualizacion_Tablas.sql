USE [proyecto_ingenieria];
GO

-- 1. Borrar tablas existentes (primero Bitacora por la dependencia)
DROP TABLE IF EXISTS Bitacora;
DROP TABLE IF EXISTS Usuarios;
GO

-- 2. Crear tabla Usuarios con la nueva estructura
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Apellido NVARCHAR(50) NOT NULL,
    DNI NVARCHAR(20) NOT NULL,
    Contraseña NVARCHAR(200) NOT NULL,
    Rol NVARCHAR(20) NOT NULL,     -- Reemplaza al viejo PerfilId
    Email NVARCHAR(100),
    Activo BIT NOT NULL DEFAULT 1,
    IntentosFallidos INT NOT NULL DEFAULT 0,
    PrimerLogin BIT NOT NULL DEFAULT 1, -- Control de cambio de clave
    
    -- Restricción para evitar cuentas clonadas
    CONSTRAINT UQ_DNI UNIQUE (DNI) 
);
GO

-- 3. Crear tabla Bitacora sincronizada con el código
CREATE TABLE Bitacora (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    FechaHora DATETIME NOT NULL DEFAULT GETDATE(),
    Accion NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    
    -- Volvemos a crear la relación con la nueva tabla de Usuarios
    CONSTRAINT FK_Bitacora_Usuarios FOREIGN KEY (UsuarioId) 
    REFERENCES Usuarios(Id)
);
GO