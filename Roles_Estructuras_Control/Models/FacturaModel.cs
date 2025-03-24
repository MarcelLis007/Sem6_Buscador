using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roles_Estructuras_Control.Models
{
    public class FacturaModel
    {
        public int Id { get; set; }
        public DateOnly FechaIngreso { get; set; }
        [Display(Name ="Numero de Factura")]
        public int NumeroFacrtura { get; set; }

        ////////relacion
        ///con cliente
      
        public int ClientesModelId { get; set; }
        public ClientesModel ClientesModel { get; set; }
    }
}
