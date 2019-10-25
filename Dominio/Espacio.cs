using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Espacio
    {
        public int Id { get; set; }
        public Espacio EspacioPadre { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Date { get; set; }
        public bool deleted { get; set; }
    }
}
