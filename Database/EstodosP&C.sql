
-------------CRUD DE SP PARA TODOS LOS ESTADOS

---------- Crear Estado de Cliente
CREATE PROCEDURE SPCreateEstadoCliente
	@Nombre VARCHAR(30)
AS
BEGIN
	INSERT INTO EstadoCliente (Nombre)
	VALUES (@Nombre);
END
GO

----------- Obtener Estado de Cliente (Todos o por ID)
CREATE PROCEDURE SPGetEstadoCliente
	@Id INT = NULL  -----Este es solo si es que el usuario quiere que lo filtre por el id o traer tos
AS
BEGIN
	IF @Id IS NULL
	BEGIN
		SELECT * FROM EstadoCliente;
	END
	ELSE
	BEGIN
		SELECT * FROM EstadoCliente WHERE Id = @Id;
	END
END
GO

--------------- Eliminar Estado de Cliente
CREATE PROCEDURE SPDeleteEstadoCliente
	@Id INT
AS
BEGIN
	DELETE FROM EstadoCliente WHERE Id = @Id;
END
GO

------------------- CRUD PARS LOS ESTADOS DE PRODUCTOS

---------- Crear 
CREATE PROCEDURE SPCreateEstadoProducto
	@Nombre VARCHAR(30)
AS
BEGIN
	INSERT INTO EstadoProducto (Nombre)
	VALUES (@Nombre);
END
GO

-- --------Obtener Estado 
CREATE PROCEDURE SPGetEstadoProducto
	@Id INT = NULL  -- gual si quiere solo por i otodos
AS
BEGIN
	IF @Id IS NULL
	BEGIN
		SELECT * FROM EstadoProducto;
	END
	ELSE
	BEGIN
		SELECT * FROM EstadoProducto WHERE Id = @Id;
	END
END
GO

-------------Eliminar Estado 
CREATE PROCEDURE SPDeleteEstadoProducto
	@Id INT
AS
BEGIN
	DELETE FROM EstadoProducto WHERE Id = @Id;
END
GO