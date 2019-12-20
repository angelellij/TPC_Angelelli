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
        public string UrlEspacio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public IDictionary<string, Usuario> Miembros { get; set; } = new Dictionary<string, Usuario>();
        public IDictionary<string, Usuario> Administradores { get; set; } = new Dictionary<string, Usuario>();
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Espacio() { }
        public Espacio(Espacio espacio) {
            Nombre = espacio.Nombre;
            Descripcion = espacio.Descripcion;
            UrlEspacio = espacio.UrlEspacio;
            Miembros = espacio.Miembros;
            Administradores = espacio.Administradores;
            Date = espacio.Date;
            Deleted = espacio.Deleted;
        }
        public Espacio(FirebaseObject<Espacio> espacio)
        {
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
                UrlEspacio = this.UrlEspacio,
                Nombre = this.Nombre,
                Descripcion = this.Descripcion
            };
        }
    }
}
