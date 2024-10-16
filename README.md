
## Tecnologías utilizadas:
- **ASP.NET Core (MVC)**
- **Store Procedures (SP)**
- **SQL Server**
- **NuGet: SQL Server y SQL Client**

## Descripción del proyecto:
Este proyecto fue desarrollado usando ASP.NET Core MVC y se conecta a una base de datos SQL Server a través de procedimientos almacenados (SP) para realizar las operaciones de inserción, actualización, eliminación y consulta de datos.

### Tablas principales:
1. **EstadoProducto**
2. **EstadoCliente**
3. **Producto**
4. **Cliente**
5. **Pedido**

Cada tabla está diseñada para gestionar un aspecto específico de la aplicación:
- **EstadoProducto**: Define el estado de un producto, como `Disponible` o `Agotado`.
- **EstadoCliente**: Define el estado de un cliente, como `Activo`, `Inactivo` o `Bloqueado`.
- **Producto**: Contiene información sobre los productos disponibles para los clientes.
- **Cliente**: Almacena información de los clientes registrados en el sistema.
- **Pedido**: Gestiona los pedidos realizados por los clientes, vinculando productos y restando la cantidad del inventario.

## Instrucciones de ejecución:
1. **Ejecutar los scripts**: 
   Antes de ejecutar la aplicación, primero solo ejecutar el script SQL que cree las tablas y los procedimientos almacenados (SP). Estos scripts están ubicados en archivos separados. 
   
    ejecute **todos los SP y las tablas** para que el proyecto funcione correctamente.

2. **Configurar la conexión a la base de datos**:
   Vaya al archivo `appsettings.json` y modifuque la cadena de conexión (`ConnectionString`) con los detalles de su servidor de SQL Server.
