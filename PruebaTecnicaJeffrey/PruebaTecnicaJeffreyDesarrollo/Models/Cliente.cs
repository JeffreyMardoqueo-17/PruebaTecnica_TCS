namespace PruebaTecnicaJeffreyDesarrollo.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Pass { get; set; }
        public int IdEstadoCliente { get; set; }
    }
}
