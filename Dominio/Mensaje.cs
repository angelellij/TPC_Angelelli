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
        public string Id { get; set; }
        public Usuario Emisor { get; set; }
        public Usuario Receptor { get; set; }
        public string Texto { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Mensaje() { }

        public Mensaje(FirebaseObject<Mensaje> Mensaje)
        {
            Id = Mensaje.Key;
            Emisor = new Usuario();
            Emisor = Mensaje.Object.Emisor;
            Receptor = new Usuario();
            Receptor = Mensaje.Object.Receptor;
            Texto = Mensaje.Object.Texto;
            Date = Mensaje.Object.Date;
            Deleted = Mensaje.Object.Deleted;
        }

        public Mensaje ReturnSmallMensaje()
        {
            return new Mensaje
            {
                Id = Id,
                Texto = Texto,
                Date = Date,
                Deleted = Deleted
            };
        }
    }
}
