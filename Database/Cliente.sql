------------------SP PARA LAA TABLA CLIENTES

CREATE PROCEDURE SPCreateCliente
	@Nombre VARCHAR(100),
	@Apellido VARCHAR(100),
	@Telefono VARCHAR(25),
	@Correo VARCHAR(100),
	@Pass VARCHAR(MAX),
	@IdEstadoCliente INT = 1  -- Por defecto es Activo (1)
AS
BEGIN
	INSERT INTO Cliente (Nombre, Apellido, Telefono, Correo, Pass, IdEstadoCliente)
	VALUES (@Nombre, @Apellido, @Telefono, @Correo, @Pass, @IdEstadoCliente);

	-- Retorna el ID del cliente recién creado
	SELECT SCOPE_IDENTITY() AS Id;
END
GO

CREATE PROCEDURE SPGetCliente
	@Id INT = NULL  --por sis quiere solo un cliente
AS
BEGIN
	IF @Id IS NULL
	BEGIN
		---------los traigo todos
		SELECT C.Id, C.Nombre, C.Apellido, C.Telefono, C.Correo, C.Pass, E.Nombre AS Estado
		FROM Cliente C
		INNER JOIN EstadoCliente E ON C.IdEstadoCliente = E.Id;
	END
	ELSE
	BEGIN
		-- o solo uno, que es el del Id
		SELECT C.Id, C.Nombre, C.Apellido, C.Telefono, C.Correo, C.Pass, E.Nombre AS Estado
		FROM Cliente C
		INNER JOIN EstadoCliente E ON C.IdEstadoCliente = E.Id
		WHERE C.Id = @Id;
	END
END
GO

-----------------SP PARA OBTNER POR LOS ESTADOS, EL Id, puede cambiar en caso que el valor del estado sea diferente en la base de datos eso es todo, con eso es el cuidado

CREATE PROCEDURE SPGetClientesActivos
AS
BEGIN
	SELECT C.Id, C.Nombre, C.Apellido, C.Telefono, C.Correo, C.Pass, E.Nombre AS Estado
	FROM Cliente C
	INNER JOIN EstadoCliente E ON C.IdEstadoCliente = E.Id
	WHERE C.IdEstadoCliente = 1;  -- Activo
END
GO

CREATE PROCEDURE SPGetClientesInactivos
AS
BEGIN
	SELECT C.Id, C.Nombre, C.Apellido, C.Telefono, C.Correo, C.Pass, E.Nombre AS Estado
	FROM Cliente C
	INNER JOIN EstadoCliente E ON C.IdEstadoCliente = E.Id
	WHERE C.IdEstadoCliente = 2;  -- Inactivo
END
GO

CREATE PROCEDURE SPGetClientesBloqueados
AS
BEGIN
	SELECT C.Id, C.Nombre, C.Apellido, C.Telefono, C.Correo, C.Pass, E.Nombre AS Estado
	FROM Cliente C
	INNER JOIN EstadoCliente E ON C.IdEstadoCliente = E.Id
	WHERE C.IdEstadoCliente = 3;  -- Bloqueado
END
GO

----------------------PARA ACTUALIZAR LA INFORMACION DE UN CLIENTE
CREATE PROCEDURE SPUpdateCliente
	@Id INT,
	@Nombre VARCHAR(100),
	@Apellido VARCHAR(100),
	@Telefono VARCHAR(25),
	@Correo VARCHAR(100),
	@Pass VARCHAR(MAX),
	@IdEstadoCliente INT
AS
BEGIN
	UPDATE Cliente
	SET 
		Nombre = @Nombre,
		Apellido = @Apellido,
		Telefono = @Telefono,
		Correo = @Correo,
		Pass = @Pass,
		IdEstadoCliente = @IdEstadoCliente
	WHERE Id = @Id;
END
GO

------------Para elimianr un cliente
CREATE PROCEDURE SPDeleteCliente
	@Id INT
AS
BEGIN
	DELETE FROM Cliente WHERE Id = @Id;
END
GO

