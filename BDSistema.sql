CREATE DATABASE DesarrolloSoftware;
GO

USE DesarrolloSoftware;
GO

CREATE TABLE Desarrolladores (
    IdDesarrollador INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Nacionalidad NVARCHAR(100),
    Especialidad NVARCHAR(100),
    Email NVARCHAR(150)
);
GO

CREATE TABLE Proyectos (
    IdProyecto INT IDENTITY(1,1) PRIMARY KEY,
    NombreProyecto NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(500),
    FechaInicio DATE,
    FechaFin DATE,
    IdResponsable INT, -- FK con Desarrollador
    FOREIGN KEY (IdResponsable) REFERENCES Desarrolladores(IdDesarrollador)
);
GO

-- Insertar desarrollador
CREATE PROCEDURE InsertarDesarrollador
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Nacionalidad NVARCHAR(100),
    @Especialidad NVARCHAR(100),
    @Email NVARCHAR(150)
AS
BEGIN
    INSERT INTO Desarrolladores (Nombre, Apellido, Nacionalidad, Especialidad, Email)
    VALUES (@Nombre, @Apellido, @Nacionalidad, @Especialidad, @Email);
END;
GO

-- Actualizar desarrollador
CREATE PROCEDURE ActualizarDesarrollador
    @IdDesarrollador INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Nacionalidad NVARCHAR(100),
    @Especialidad NVARCHAR(100),
    @Email NVARCHAR(150)
AS
BEGIN
    UPDATE Desarrolladores
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Nacionalidad = @Nacionalidad,
        Especialidad = @Especialidad,
        Email = @Email
    WHERE IdDesarrollador = @IdDesarrollador;
END;
GO

-- Eliminar desarrollador
CREATE PROCEDURE EliminarDesarrollador
    @IdDesarrollador INT
AS
BEGIN
    DELETE FROM Desarrolladores
    WHERE IdDesarrollador = @IdDesarrollador;
END;
GO

-- Listar desarrolladores
CREATE PROCEDURE ConsultarDesarrolladores
AS
BEGIN
    SELECT * FROM Desarrolladores;
END;
GO

-- Insertar proyecto
CREATE PROCEDURE InsertarProyecto
    @NombreProyecto NVARCHAR(150),
    @Descripcion NVARCHAR(500),
    @FechaInicio DATE,
    @FechaFin DATE,
    @IdResponsable INT
AS
BEGIN
    INSERT INTO Proyectos (NombreProyecto, Descripcion, FechaInicio, FechaFin, IdResponsable)
    VALUES (@NombreProyecto, @Descripcion, @FechaInicio, @FechaFin, @IdResponsable);
END;
GO

-- Actualizar proyecto
CREATE PROCEDURE ActualizarProyecto
    @IdProyecto INT,
    @NombreProyecto NVARCHAR(150),
    @Descripcion NVARCHAR(500),
    @FechaInicio DATE,
    @FechaFin DATE,
    @IdResponsable INT
AS
BEGIN
    UPDATE Proyectos
    SET NombreProyecto = @NombreProyecto,
        Descripcion = @Descripcion,
        FechaInicio = @FechaInicio,
        FechaFin = @FechaFin,
        IdResponsable = @IdResponsable
    WHERE IdProyecto = @IdProyecto;
END;
GO

-- Eliminar proyecto
CREATE PROCEDURE EliminarProyecto
    @IdProyecto INT
AS
BEGIN
    DELETE FROM Proyectos
    WHERE IdProyecto = @IdProyecto;
END;
GO

-- Listar proyectos
CREATE PROCEDURE ConsultarProyectos
AS
BEGIN
    SELECT 
        P.IdProyecto, 
        P.NombreProyecto, 
        P.Descripcion, 
        P.FechaInicio, 
        P.FechaFin,
        D.Nombre + ' ' + D.Apellido AS Responsable
    FROM Proyectos P
    LEFT JOIN Desarrolladores D ON P.IdResponsable = D.IdDesarrollador;
END;
GO
