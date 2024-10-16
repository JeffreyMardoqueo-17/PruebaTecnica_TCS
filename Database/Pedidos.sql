---------------SP para crear un pedido

CREATE PROCEDURE SPCrearPedido
    @IdCliente INT,      ------------ El cliente que hace el pedido
    @IdProducto INT,     -------- El producto que quiere pedir
    @Cantidad INT        ------- La cantidad que quiere pedir
AS
BEGIN
    -- Verificar si la cantidad solicitada está disponible
    IF EXISTS (SELECT 1 FROM Producto WHERE Id = @IdProducto AND Cantidad >= @Cantidad)
    BEGIN
        -- Insertar el pedido en la tabla Pedido
        INSERT INTO Pedido (IdCliente, IdProducto, Cantidad)
        VALUES (@IdCliente, @IdProducto, @Cantidad);

        -- Actualizar la cantidad del producto
        UPDATE Producto
        SET Cantidad = Cantidad - @Cantidad
        WHERE Id = @IdProducto;

        -- Si la cantidad del producto llega a 0, marcar como "Agotado"
        UPDATE Producto
        SET IdEstadoProducto = 2  -- 2 = Agotado
        WHERE Id = @IdProducto AND Cantidad = 0;

        SELECT 'Pedido creado exitosamente';
    END
    ELSE
    BEGIN
        ------- Si no hay suficientes productos devolver un mensaje de error
        SELECT 'Cantidad insuficiente para el producto solicitado';
    END
END
GO

CREATE PROCEDURE SPGetPedidos
AS
BEGIN
    SELECT 
        P.Id AS PedidoId,                 
        C.Nombre + ' ' + C.Apellido AS Cliente,
        PR.Nombre AS Producto,          
        PR.Precio,                        
        P.Cantidad,                     
        (P.Cantidad * PR.Precio) AS Total,
        P.FechaPedido                     
    FROM Pedido P
    INNER JOIN Cliente C ON P.IdCliente = C.Id     
    INNER JOIN Producto PR ON P.IdProducto = PR.Id   
    ORDER BY P.FechaPedido DESC;
END
GO
