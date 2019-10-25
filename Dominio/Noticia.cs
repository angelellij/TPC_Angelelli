using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    class Noticia
    {
        public int Id { get; set; }
        public Espacio Espacio { get; set; }
        public Usuario Usuario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Date { get; set; }
        public bool deleted { get; set; }
    }
}
