using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Espacio
    {
        public string Id { get; set; }
        public List<string> UrlEspacio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Usuario> Miembros { get;set; }
        public List<Usuario> Usuarios { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }
    }
}
