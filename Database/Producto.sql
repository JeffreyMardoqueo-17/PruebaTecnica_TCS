
-------------CRUD PARA PRODUCTOS
CREATE PROCEDURE SPCreateProducto
	@Nombre VARCHAR(100),
	@Descripcion VARCHAR(MAX),
	@Precio DECIMAL(10,2),
	@Cantidad INT,
	@IdEstadoProducto INT  -- Estado del producto: 1 = Disponible, 2 = Agotado
AS
BEGIN
	INSERT INTO Producto (Nombre, Descripcion, Precio, Cantidad, IdEstadoProducto)
	VALUES (@Nombre, @Descripcion, @Precio, @Cantidad, @IdEstadoProducto);

	------Esto de aqui retornara el Id de del prodcuto creado
	SELECT SCOPE_IDENTITY() AS Id;
END
GO


---------------- obtener todos los productos
CREATE PROCEDURE SPGetProductos
    @Id INT = NULL  -- Si el ID es NULL, devuelve todos los productos
AS
BEGIN
    IF @Id IS NULL -- Esto es para saber si la consulta será para todos o solo para uno
    BEGIN
        -- Para todos
        SELECT P.Id, P.Nombre, P.Descripcion, P.Precio, P.Cantidad, P.IdEstadoProducto, E.Nombre AS Estado
        FROM Producto P
        INNER JOIN EstadoProducto E ON P.IdEstadoProducto = E.Id;
    END
    ELSE
    BEGIN
        -- Por si es para un solo producto por Id
        SELECT P.Id, P.Nombre, P.Descripcion, P.Precio, P.Cantidad, P.IdEstadoProducto, E.Nombre AS Estado
        FROM Producto P
        INNER JOIN EstadoProducto E ON P.IdEstadoProducto = E.Id
        WHERE P.Id = @Id;
    END
END
GO


----------------OBTNER LOS PRODUCTOS DISPONIBLES
CREATE PROCEDURE SPGetProductosDisponibles
AS
BEGIN
    SELECT P.Id, P.Nombre, P.Descripcion, P.Precio, P.Cantidad, P.IdEstadoProducto, E.Nombre AS Estado
    FROM Producto P
    INNER JOIN EstadoProducto E ON P.IdEstadoProducto = E.Id
    WHERE P.IdEstadoProducto = 1;  -- 1 = Disponible
END
GO

-------------------------- obtenr todos los productos agotados
CREATE PROCEDURE SPGetProductosAgotados
AS
BEGIN
    SELECT P.Id, P.Nombre, P.Descripcion, P.Precio, P.Cantidad, P.IdEstadoProducto, E.Nombre AS Estado
    FROM Producto P
    INNER JOIN EstadoProducto E ON P.IdEstadoProducto = E.Id
    WHERE P.IdEstadoProducto = 2;  -- 2 = Agotado
END
GO


---------------------ACTUALIZAR UN PRODUCTO POR MEDIO DE SU ID

CREATE PROCEDURE SPUpdateProducto
	@Id INT,
	@Nombre VARCHAR(100),
	@Descripcion VARCHAR(MAX),
	@Precio DECIMAL(10,2),
	@Cantidad INT,
	@IdEstadoProducto INT  -- Estado del producto: 1 = Disponible, 2 = Agotado
AS
BEGIN
	UPDATE Producto
	SET 
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		Precio = @Precio,
		Cantidad = @Cantidad,
		IdEstadoProducto = @IdEstadoProducto
	WHERE Id = @Id;
END
GO

--------------- para eliminar un producto
CREATE PROCEDURE SPDeleteProducto
	@Id INT
AS
BEGIN
	DELETE FROM Producto WHERE Id = @Id;
END
GO
