using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Tag
    {
        public String id { get; set; }
        public Espacio espacio { get; set; }
        public string nombre { get; set; }
        public string colorLetra { get; set; }
        public string  colorBackground { get; set; }

    }
}
