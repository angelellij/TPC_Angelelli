﻿using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Noticia
    {
        public string Id { get; set; }
        public Espacio Espacio { get; set; }
        public Usuario Usuario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Noticia() { }
        public Noticia(FirebaseObject<Noticia> noticia)
        {
            Id = noticia.Key;
            Titulo = noticia.Object.Titulo;
            Descripcion = noticia.Object.Descripcion;
            Date = noticia.Object.Date;
            Deleted = noticia.Object.Deleted;
            Usuario = noticia.Object.Usuario;
            Espacio = noticia.Object.Espacio;
        }

        public Noticia ReturnNoticiaFire()
        {
            Noticia NoticiaFire = new Noticia
            {
                Id = Id,
                Titulo = Titulo,
                Descripcion = Descripcion,
                Date = Date,
                Deleted = Deleted,
                Usuario = Usuario.ReturnSmallUsuario()
        };
            return NoticiaFire;
        }
    }
}
