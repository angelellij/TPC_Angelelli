using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Mensaje
    {
        public Go<Usuario> Emisor { get; set; }
        public Go<Usuario> Receptor { get; set; }
        public string Texto { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Mensaje() { }

        public Mensaje(FirebaseObject<Mensaje> Mensaje)
        {
            Emisor = new Go<Usuario>();
            Emisor = Mensaje.Object.Emisor;
            Receptor = new Go<Usuario>(Mensaje.Object.Receptor.Key,Mensaje.Object.Receptor.Object);
            Receptor = Mensaje.Object.Receptor;
            Texto = Mensaje.Object.Texto;
            Date = Mensaje.Object.Date;
            Deleted = Mensaje.Object.Deleted;
        }

        public Mensaje ReturnSmallMensaje()
        {
            return new Mensaje
            {
                Texto = Texto,
                Date = Date,
                Deleted = Deleted
            };
        }
    }
}
