using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email{ get; set; }
        public string Contrasena { get; set; }
        public string FNacimiento { get; set; }
        public string Espacios { get; set; }
        public string UrlFoto { get; set; }
        public string TimestampUtc { get; set; }

    }
}
