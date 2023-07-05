using MyCl2AndreeChiquis.Models;
using System.Data.SqlClient;

namespace MyCl2AndreeChiquis.DAO
{
    public class VentasDAO
    {
        // crear la variable string para obtener la
        // cadena de conexion desde al appsettings.json
        private readonly string cad_conexion;

        // desde el constructor que recibe una variable del
        // tipo IConfiguration obtendremos la cadena de conexion
        public VentasDAO(IConfiguration configuration)
        {
            cad_conexion = configuration.GetConnectionString("cn1");
        }

        // Listar los Productos
        public List<ArticuloModel> GetArticulos(string nom) {
            List<ArticuloModel> lista = new List<ArticuloModel>();
            //
            SqlDataReader dr = SqlHelper.ExecuteReader(
                   cad_conexion, "PA_LISTARXNOMBRE", nom);
            //
            while (dr.Read())
            {
                lista.Add(new ArticuloModel() {
                    cod_art = dr.GetString(0),
                    nom_art = dr.GetString(1),
                    uni_med = dr.GetString(2),
                    pre_art = dr.GetDecimal(3),
                    stk_art = dr.GetInt32(4)
                });
            }
            dr.Close();
            //
            return lista;
        }
    

        // BuscarProducto
        public ArticuloModel BuscarArticulo(string cod_art)
        {
            ArticuloModel? buscado =
                GetArticulos("").Find(a => a.cod_art.Equals(cod_art));

            if (buscado == null)
                buscado = new ArticuloModel();

            return buscado;
        }


    }
}
