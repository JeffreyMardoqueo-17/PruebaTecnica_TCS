
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

### Tablas principales:
1. **Un helper**: Hay unos metodos en la carpeta Helper que lo que hacen son eejecutar los Sp, para no repetir el mismo codigo una y otra vez

### Caapturas de las vistas:
1. **Home**:
   ![image](https://github.com/user-attachments/assets/cdc9f400-8a6b-4f96-9704-38762f0b3695)
2. **ESTADO PRODUCTO**:
 ![image](https://github.com/user-attachments/assets/9974833b-7d6b-471b-b31f-7049072442d6)
 ![image](https://github.com/user-attachments/assets/e739f2ed-2ffc-45d1-b2c2-e740a3cc3578)
 ![image](https://github.com/user-attachments/assets/29cd8cce-4e60-40ab-b3ae-bfb0057c1d86)
![image](https://github.com/user-attachments/assets/2588be49-5bab-450c-82fd-3399a718cef4)

3. **ESTADO CLIENTE**:
   ![image](https://github.com/user-attachments/assets/a468a943-b647-4a4e-84b0-091a89ed4b73)
   ![image](https://github.com/user-attachments/assets/d9de9c90-a897-422e-9808-d8c0a6789193)
   ![image](https://github.com/user-attachments/assets/6183df32-0a4e-42d8-88ff-aa8151881212)
   ![image](https://github.com/user-attachments/assets/666674a1-d090-478d-a287-9d665f1622dc)
4. **Cliente**:
   ![image](https://github.com/user-attachments/assets/cebe5fcf-29a7-4405-be5e-5a9132fffae9)
   ![image](https://github.com/user-attachments/assets/7717886d-5cbd-40e1-a829-366a38f103a1)
   ![image](https://github.com/user-attachments/assets/8cb649b6-7e2c-405d-a6e5-a47e0bc690f9)
   ![image](https://github.com/user-attachments/assets/01d4498a-fdc3-46eb-9ffd-f48b6e123989)

5. **Productos**:
   Aqui puedo filtrar, y me dice si es que hay o no hay procuts disponibles, ademas mas adelante si hago un peiddo me dice cuantos hay
   ![image](https://github.com/user-attachments/assets/24861a83-6708-4af0-8973-015d4155c2e3)
   ![image](https://github.com/user-attachments/assets/41a2416e-7f48-443c-b0f6-c7726c97d45e)
   ![image](https://github.com/user-attachments/assets/03117ddf-5ab4-41bc-9d14-d8880d0857f9)
   ![image](https://github.com/user-attachments/assets/b24c726e-2850-45fc-8acf-0264e0b905a9)
   ![image](https://github.com/user-attachments/assets/764273c1-3ea5-418d-a691-d5ee3c8fa86d)
   si yo guardo un producto con valor cero, de una vez se gguarda como agotado, ademas se filtran
   ![image](https://github.com/user-attachments/assets/e69e4649-6fa8-48d9-9ed4-578de2b05a04)
   ![image](https://github.com/user-attachments/assets/3d31a910-6048-4fd1-8a28-d031db01d54b)
6. **Pedidos**:
   En pedidos, selecciono e usuario y el producto y si selcciono una cantidad, le resto a la cantidad de productos
   ![image](https://github.com/user-attachments/assets/29b043ab-4779-4d21-bf84-4b4bf3d30af4)
   ![image](https://github.com/user-attachments/assets/53742eba-1c75-4d3e-8855-ec0fd8024f54)
   ahi me aparece que engo solo 2 dispoibles que cisiden con la tabla de productos, si ahora pido uno pues quedara solo uno en la de productos
   ![image](https://github.com/user-attachments/assets/fac44b8b-1bd1-431d-8424-05ffffb905c2)
   ![image](https://github.com/user-attachments/assets/0e11c920-fad3-49ab-9272-6c2d2fec6136)
   y ahora aprece ya en 1 nada mas disponibles
   ![Uploading image.png…]()


