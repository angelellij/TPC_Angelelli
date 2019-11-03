using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comentario
    {
        public Go<Post> Post { get; set; }
        public Go<Usuario> Usuario { get; set; }
        public string Texto { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Comentario() { }

        public Comentario(FirebaseObject<Comentario> Comentario)
        {
            Post = new Go<Post>();
            Post = Comentario.Object.Post;
            Usuario = new Go<Usuario>();
            Usuario = Comentario.Object.Usuario;
            Texto = Comentario.Object.Texto;
            Date = Comentario.Object.Date;
            Deleted = Comentario.Object.Deleted;
        }

        public Comentario ReturnSmallComentario()
        {
            return new Comentario()
            {
                Texto = Texto,
                Date = Date,
                Deleted = Deleted,
                Post = Post,
                Usuario = Usuario
            };
        }
    }
}
