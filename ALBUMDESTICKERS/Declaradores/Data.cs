using ALBUMDESTICKERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALBUMDESTICKERS.Declaradores
{
    public class Data
    {
        private static Data _instance = null;
        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Data();
                }
                return _instance;
            }
        }
        public Album AlbumUCL = new Album();
    }
}