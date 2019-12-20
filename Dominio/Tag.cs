using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;

namespace Dominio
{
    public class Tag
    {
        public Go<Espacio> Espacio { get; set; }
        public string Nombre { get; set; }
        public string ColorLetra { get; set; }
        public string ColorBackground { get; set; }
        public Tag() { }
        public Tag(FirebaseObject<Tag> tag)
        {
                Nombre = tag.Object.Nombre;
                Espacio = new Go<Espacio>(tag.Object.Espacio);
                ColorLetra = tag.Object.ColorLetra;
                ColorBackground = tag.Object.ColorBackground;
        }

        public Tag ReturnSmallTag()
        {
            return new Tag
            {
                Nombre = Nombre,
                ColorLetra = ColorLetra,
                ColorBackground = ColorBackground
        };
        }
    }
}
