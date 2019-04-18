using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALBUMDESTICKERS.Models
{
    public class Album
    {
        public Dictionary<string, Equipo> Equipos = new Dictionary<string, Equipo>();
        public List<Equipo> _Equipos = new List<Equipo>();
        public Dictionary<string, Jugador> General = new Dictionary<string, Jugador>();
        public List<Jugador> _General = new List<Jugador>();
        public Album()
        {

        }
    }
}