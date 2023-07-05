using System.ComponentModel.DataAnnotations;

namespace MyCl2AndreeChiquis.Models
{
    public class ArticuloModel
    {
        [Display(Name = "Codigo")]
        public string cod_art { get; set; }

        [Display(Name = "Nombre del Articulo")]
        public string nom_art { get; set; }

        [Display(Name = "Unidad de Medida")]
        public string uni_med { get; set; }

        [Display(Name = "Precio")]
        public decimal pre_art { get; set; }

        [Display(Name = "Stock")]
        public int stk_art { get; set; }
    }
}
