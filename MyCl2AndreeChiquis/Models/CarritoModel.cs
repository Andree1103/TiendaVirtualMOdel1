namespace MyCl2AndreeChiquis.Models
{
    public class CarritoModel
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal importe
        {
            get {
                return precio * cantidad;
            } 
        }
    }
}
