using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Tag
    {
        public string Id { get; set; }
        public Espacio Espacio { get; set; }
        public string Nombre { get; set; }
        public string ColorLetra { get; set; }
        public string ColorBackground { get; set; }

        public Tag ReturnSmallTag()
        {
            Tag smallTag = new Tag();
            smallTag.Nombre = Nombre;
            smallTag.Espacio = new Espacio();
            smallTag.Espacio.Id = Espacio.Id;
            smallTag.Espacio.UrlEspacio = Espacio.UrlEspacio;
            return smallTag;
        }
    }
}
