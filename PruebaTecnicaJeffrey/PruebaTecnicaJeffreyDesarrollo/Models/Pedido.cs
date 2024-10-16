namespace PruebaTecnicaJeffreyDesarrollo.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaPedido { get; set; }

        // Propiedades adicionales para facilitar la visualización en la vista
        public string ClienteNombre { get; set; }  
        public string ProductoNombre { get; set; } 
        public decimal Precio { get; set; }  
        public decimal Total { get; set; }  
    }
}
