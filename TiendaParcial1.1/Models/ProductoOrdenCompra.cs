namespace TiendaParcial1._1.Models
{
    public class ProductoOrdenCompra
    {

        public int Id { get; set; }
        public int OrdenCompraId { get; set; }
        public OrdenCompra OrdenCompra { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
