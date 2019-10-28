﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Espacio
    {
        public string Id { get; set; }
        public string UrlEspacio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Usuario> Miembros { get;set; }
        public List<Usuario> Administradores { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public string getUrlEspacio()
        {
            string UrlEspacioFull = "Espacios";
            if (UrlEspacio != null){ UrlEspacioFull = UrlEspacioFull + "/" + UrlEspacio; }
            UrlEspacioFull = UrlEspacioFull + "/" + Id;
            return UrlEspacioFull;
        }
    }
}
