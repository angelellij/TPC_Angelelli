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
        public string Id { get; set; }
        public Post Post { get; set; }
        public Usuario Usuario { get; set; }
        public string Texto { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Comentario() { }

        public Comentario(FirebaseObject<Comentario> Comentario)
        {
            Id = Comentario.Key;
            Post = new Post();
            Post = Comentario.Object.Post;
            Usuario = new Usuario();
            Usuario = Comentario.Object.Usuario;
            Texto = Comentario.Object.Texto;
            Date = Comentario.Object.Date;
            Deleted = Comentario.Object.Deleted;
        }

        public Comentario ReturnSmallComentario()
        {
            return new Comentario
            {
                Id = Id,
                Texto = Texto,
                Date = Date,
                Deleted = Deleted,
                Post = Post.ReturnSmallPost(),
                Usuario = Usuario.ReturnSmallUsuario()
            };
        }
    }
}
