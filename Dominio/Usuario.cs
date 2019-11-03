using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email{ get; set; }
        public string Contrasena { get; set; }
        public string FNacimiento { get; set; }
        public string UrlFoto { get; set; }
        public IDictionary<string, Espacio> EspaciosAdministrador { get; set; } = new Dictionary<string, Espacio>();
        public IDictionary<string, Espacio> EspaciosMiembro { get; set; } = new Dictionary<string, Espacio>();
        public string TimestampUtc { get; set; }
        public Usuario() { }
        public Usuario(Usuario usuario)
        {
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            Email = usuario.Email;
            Contrasena = usuario.Contrasena;
            UrlFoto = usuario.UrlFoto;
            FNacimiento = usuario.FNacimiento;
            TimestampUtc = usuario.TimestampUtc;
            EspaciosAdministrador = usuario.EspaciosAdministrador;
            EspaciosMiembro = usuario.EspaciosMiembro;
        }
        public Usuario(FirebaseObject<Usuario> usuario)
        {
            Nombre = usuario.Object.Nombre;
            Apellido = usuario.Object.Apellido;
            Email = usuario.Object.Email;
            Contrasena = usuario.Object.Contrasena;
            UrlFoto = usuario.Object.UrlFoto;
            FNacimiento = usuario.Object.FNacimiento;
            TimestampUtc = usuario.Object.TimestampUtc;
            EspaciosAdministrador = usuario.Object.EspaciosAdministrador;
            EspaciosMiembro = usuario.Object.EspaciosMiembro;
        }

        public Usuario ReturnSmallUsuario()
        {
           return new Usuario
            {
                Nombre = Nombre,
                Apellido = Apellido,
                UrlFoto = UrlFoto
            };
        }
        

    }
}
