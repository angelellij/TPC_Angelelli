using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    class Mensaje
    {
        public int Id { get; set; }
        public Usuario Emisor { get; set; }
        public Usuario Receptor { get; set; }
        public string Texto { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }
    }
}
