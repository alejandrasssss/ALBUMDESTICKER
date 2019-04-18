using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ALBUMDESTICKERS.Declaradores;
using ALBUMDESTICKERS.Models;

namespace ALBUMDESTICKERS.Controllers
{
    public class ManejoArchivoController : Controller
    {
        public ActionResult CargarArchivo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CargarArchivo(HttpPostedFileBase postedFile)
        {
            var FilePath = string.Empty;

            if (postedFile != null)
            {
                var path = Server.MapPath("~/CargaCSV/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FilePath = path + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(FilePath);
                var CsvData = System.IO.File.ReadAllText(FilePath);
                foreach (var fila in CsvData.Split('\n'))
                {
                    var _fila = fila.Trim();
                    if (!string.IsNullOrEmpty(fila))
                    {
                        var linea = _fila.Split(',');
                        var listaJugadores = new List<int>();
                        foreach (var num in linea)
                        {
                            int No;

                            if (int.TryParse(num, out No))
                            {
                                listaJugadores.Add(int.Parse(num));
                            }
                        }
                        listaJugadores.Sort();
                        var equipo = new Equipo(linea[0], linea[1], listaJugadores);
                        equipo.Calcular();
                        Data.Instance.AlbumUCL._Equipos.Add(equipo);
                        Data.Instance.AlbumUCL._Equipos.Sort();
                        Data.Instance.AlbumUCL.Equipos.Add(equipo.NombreEquipo, equipo);

                        foreach (var j in equipo.Jugadores)
                        {
                            Data.Instance.AlbumUCL.General.Add(j.Nombre, j);
                            Data.Instance.AlbumUCL._General.Add(j);
                        }

                    }
                }
                System.IO.File.Delete(FilePath);
                Directory.Delete(path);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

