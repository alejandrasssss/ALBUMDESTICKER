using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALBUMDESTICKERS.Models
{
    public class Equipo : IComparable
    {
        [Display(Name = "Equipo")]
        public string NombreEquipo { get; set; }
        [Display(Name = "País")]
        public string Pais { get; set; }
        public List<Jugador> Jugadores { get; set; }
        [Display(Name = "Faltantes")]
        public int Obtenidas { get; set; }
        [Display(Name = "Obtenidas")]
        public int Faltantes { get; set; }
        public Equipo(string nombreEquipo, string pais, List<int> players)
        {
            NombreEquipo = nombreEquipo;
            Pais = pais;
            Jugadores = new List<Jugador>(25);
            for (int i = 0; i < Jugadores.Capacity; i++)
            {
                var j = new Jugador(NombreEquipo + (i + 1));
                Jugadores.Add(j);
            }
            foreach (var item in players)
            {
                var nombre = NombreEquipo + item;

                for (int i = 0; i < Jugadores.Count; i++)
                {
                    if (Jugadores[i].Nombre == nombre)
                    {
                        if (Jugadores[i].Obtenida == true)
                        {
                            Jugadores[i].Repetida++;
                        }
                        else
                        {
                            Jugadores[i].Obtenida = true;
                        }

                    }
                }
            }
        }
        public void Calcular()
        {
            Obtenidas = 0;
            Faltantes = 0;
            foreach (var item in Jugadores)
            {
                if (item.Obtenida)
                {
                    Obtenidas++;
                }
                else
                {
                    Faltantes++;
                }
            }
        }
        public int CompareTo(object obj)
        {
            var comparer = (Equipo)obj;
            return NombreEquipo.CompareTo(comparer.NombreEquipo);
        }
    }
}