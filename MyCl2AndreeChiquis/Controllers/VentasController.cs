using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// librerias necesarias a utilizar
using MyCl2AndreeChiquis.DAO;
using MyCl2AndreeChiquis.Models;
using Newtonsoft.Json;

namespace MyCl2AndreeChiquis.Controllers
{
    public class VentasController : Controller
    {
        // definir las variables privadas y de solo lectura
        // que vayamos a usar
        private readonly VentasDAO dao;

        public VentasController(VentasDAO _dao)
        {
            dao = _dao;
        }

        // Acciones para el carrito de Compra

        // GET: VentasController
        public ActionResult listarArticulos(string nom = "")
        {
            // comprobar la existencia de la variable de session
            // crear la variable de session carrito si es que no existe (==null)
           if (HttpContext.Session.GetString("carrito") == null)
            {
                HttpContext.Session.SetString("carrito", 
                    // serializando a json la lista del carrito de compra
                    JsonConvert.SerializeObject(
                        new List<CarritoModel>()
                    )
                );
            }
            
            var listado = dao.GetArticulos(nom);
            ViewBag.NOM = nom;
             return View(listado);
        }


        // GET: VentasController/Details/5
        public ActionResult AgregarCarrito(string id)
        {
            return View(dao.BuscarArticulo(id));
        }

        // agregar un action de tipo POST AgregarCarrito
        [HttpPost]
        public ActionResult AgregarCarrito(string id, int ncantidad)
        {
            // buscar los datos del producto seleccionado (codigo ==> id)
            ArticuloModel objprod = dao.BuscarArticulo(id);

            // asignar los datos del producto a un objeto carrito de compra
            CarritoModel cm = new CarritoModel() { 
                codigo = objprod.cod_art,
                nombre = objprod.nom_art,
                precio = objprod.pre_art,
                cantidad = ncantidad,
                //importe = precio * cantidad
            };

            // recuperar de la session la lista genérica que almacena los
            // productos del carrito (deserializar la session)
            var lista_carrito =
                JsonConvert.DeserializeObject<List<CarritoModel>>(
                    HttpContext.Session.GetString("carrito")
                );

            var buscado = lista_carrito.Find(
                    p => p.codigo.Equals(cm.codigo)
                );

            // si el producto nuevo para agregar al carrito de compra
            // no existe en el carrito entonces lo agregamos
            if (buscado == null)
            {
                // agregar el nuevo objeto del carrito a la lista generica
                lista_carrito.Add(cm);
                ViewBag.MENSAJE = "Producto Agregado al Carrito de Compra";
            }
            else // si existe en el carrito, actualizaremos su cantidad
            {
                buscado.cantidad += cm.cantidad;
                ViewBag.MENSAJE = "Cantidad Actualizada en el Carrito de Compra";
            }

            // guardar la lista genérica en la variable de session
            HttpContext.Session.SetString("carrito",
                JsonConvert.SerializeObject(
                    lista_carrito)
                );

            return View(objprod);
        }


        // GET: VerCarrito
        public ActionResult VerCarrito()
        {
            List<CarritoModel> lista = new List<CarritoModel>();
            // recuperamos desde la session carrito la lista generica
            // del carrito de compra
            if (HttpContext.Session.GetString("carrito") != null)
            {
                lista = JsonConvert.DeserializeObject<List<CarritoModel>>(
                    HttpContext.Session.GetString("carrito")
                    );
            }
            //
            ViewBag.TOTAL = lista.Sum( c => c.importe );
            //
            return View(lista);
        }


        [HttpPost]
        public ActionResult EliminarProducto(string id)
        { 
            // recuperar la lista de la session
            var lista = JsonConvert.DeserializeObject<List<CarritoModel>>(
                    HttpContext.Session.GetString("carrito")
                    );

            // buscar el producto por su codigo ("id") para eliminarlo
            CarritoModel buscado = lista.Find( c => c.codigo.Equals(id));
            
            lista.Remove(buscado);

            // grabar la session
            HttpContext.Session.SetString("carrito",
                JsonConvert.SerializeObject(lista)
            );

            // finalmente nos dirigimos a la vista VerCarrito
            return RedirectToAction("VerCarrito");
        }






        // GET: VentasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

     }
}
