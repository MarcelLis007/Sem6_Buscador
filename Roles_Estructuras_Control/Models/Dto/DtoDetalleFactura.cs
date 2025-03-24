namespace Roles_Estructuras_Control.Models.Dto
{
    public class DtoDetalleFactura
    {
        public int Id { get; set; }
        public string Nombre_Producto { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
        public float Total { get; set; }
    }
}
