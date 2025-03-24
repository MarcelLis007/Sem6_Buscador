using Roles_Estructuras_Control2.Models;

namespace Roles_Estructuras_Control.Models
{
    public class DetalleFacturaModel
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public float valor { get; set; }

        ///relaciones 
        ///Productos
        public int ProductoModelsId { get; set; }
        public ProductoModels ProductoModels { get; set; }


        //factura cabecera de la factura
        public int FacturaModelId { get; set; }
        public FacturaModel FacturaModel { get; set; }

        //relacion con stock

        public int StockModelsId { get; set; }
        public StockModels StockModels { get; set; }

    }
}
