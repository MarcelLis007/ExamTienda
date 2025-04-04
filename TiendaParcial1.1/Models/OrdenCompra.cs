namespace TiendaParcial1._1.Models
{
    public class OrdenCompra
    {

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public ICollection<ProductoOrdenCompra> ProductosOrdenCompra { get; set; }
    }
}
