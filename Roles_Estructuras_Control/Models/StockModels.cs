
using Roles_Estructuras_Control2.Models;
using System.ComponentModel.DataAnnotations;

namespace Roles_Estructuras_Control.Models
{
    public class StockModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage ="El campo es requerido")]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha de Fabricación")]
        public DateOnly FechaFabricacion { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Date)]
        public DateOnly FechaCaducidad { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Date)]
        public DateOnly FechaRegistro { get; set; }



        public float precioUnitario { get; set; }
        public float precioVenta { get; set; }
        /////relaciones
        ///con producto
        public int ProductoModelsId { get; set; }
        public ProductoModels ProductoModels { get; set; }
        /////con proveedorr
        public int ProveedoresModelsId { get; set; }
        public ProveedoresModels ProveedoresModels { get; set; }
    }
}
