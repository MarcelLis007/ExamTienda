namespace TiendaParcial1._1.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
