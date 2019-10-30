using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;

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

        public string GetUrlEspacio()
        {
            string UrlEspacioFull = "Espacios";
            if (UrlEspacio != null){ UrlEspacioFull = UrlEspacioFull + "/" + UrlEspacio; }
            UrlEspacioFull = UrlEspacioFull + "/" + Id;
            return UrlEspacioFull;
        }
        public Espacio() { }
        public Espacio(FirebaseObject<Espacio> espacio)
        {
            Id = espacio.Key;
            Nombre = espacio.Object.Nombre;
            Descripcion = espacio.Object.Descripcion;
            UrlEspacio = espacio.Object.UrlEspacio;
            Miembros = espacio.Object.Miembros;
            Administradores = espacio.Object.Administradores;
            Date = espacio.Object.Date;
            Deleted = espacio.Object.Deleted;
        }

        public Espacio ReturnSmallEspacio()
        {
            return new Espacio
            {
                Id = this.Id,
                UrlEspacio = this.UrlEspacio,
                Nombre = this.Nombre
            };
        }
    }
}
