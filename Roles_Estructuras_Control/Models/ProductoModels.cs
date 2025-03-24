using System.ComponentModel.DataAnnotations;

namespace Roles_Estructuras_Control2.Models
{
    public class ProductoModels
    {
        internal readonly object Precio;

        public int Id { get; set; }
        [Display(Name = "Nombre del Producto")]
        [MinLength(3)]
        [Required(ErrorMessage = "El campo es requerido")]
        public string NombreProducto { get; set; }
        [Display(Name = "Presentación del Producto")]
        [MinLength(3)]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Presentacion { get; set; }
        [Display(Name = "Codigo de Barras")]
        [MinLength(7)]

        public string CodigoBarras { get; set; }



 


    }

}
