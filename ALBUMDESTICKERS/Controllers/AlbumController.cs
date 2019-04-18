using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALBUMDESTICKERS.Declaradores;
using ALBUMDESTICKERS.Models;

namespace ALBUMDESTICKERS.Controllers
{
    public class AlbumController : Controller
    {
        // GET: Album
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VerEquipos()
        {
            return View(Data.Instance.AlbumUCL._Equipos);
        }
        public ActionResult DetailsEquipo(string id)
        {
            return View(Data.Instance.AlbumUCL.Equipos[id].Jugadores);
        }
        public ActionResult EditarJugador(string id)
        {
            return View(Data.Instance.AlbumUCL.General[id]);
        }
        [HttpPost]
        public ActionResult EditarJugador(string id, FormCollection collection)
        {
            bool obtenida = false;
            var aux = collection["check"].ToUpper();
            if (aux == "SI")
            {
                obtenida = true;
            }
            else if (aux == "NO")
            {
                obtenida = false;
            }
            var repetida = int.Parse(collection["Repetida"]);
            Data.Instance.AlbumUCL.General[id].Obtenida = obtenida;
            Data.Instance.AlbumUCL.General[id].Repetida = repetida;
            Predicate<Jugador> BuscadorJugador = (Jugador jugador) => { return jugador.Nombre == id; };
            Data.Instance.AlbumUCL._General.Find(BuscadorJugador).Obtenida = obtenida;
            Data.Instance.AlbumUCL._General.Find(BuscadorJugador).Repetida = repetida;
            foreach (var item in Data.Instance.AlbumUCL.Equipos)
            {
                try
                {
                    item.Value.Jugadores.Find(BuscadorJugador).Obtenida = obtenida;
                    item.Value.Jugadores.Find(BuscadorJugador).Repetida = repetida;
                    var name = item.Key;
                    Predicate<Equipo> BuscadorEquipo = (Equipo equipo) => { return equipo.NombreEquipo == name; };
                    Data.Instance.AlbumUCL._Equipos.Find(BuscadorEquipo).Jugadores.Find(BuscadorJugador).Obtenida = obtenida;
                    Data.Instance.AlbumUCL._Equipos.Find(BuscadorEquipo).Jugadores.Find(BuscadorJugador).Repetida = repetida;
                    Data.Instance.AlbumUCL._Equipos.Find(BuscadorEquipo).Calcular();
                }
                catch (Exception e)
                {

                }
            }
            return RedirectToAction("VerEquipos");
        }
        public ActionResult VerGeneral()
        {
            return View(Data.Instance.AlbumUCL._General);
        }

        public ActionResult DetallesGeneral(string id)
        {
            var ID = id;
            return RedirectToAction("EditarJugador", "Album", new { id = ID });
        }
    }
}