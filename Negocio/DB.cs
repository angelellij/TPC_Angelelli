using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Database.Query;
using Dominio;

namespace Negocio
{
    public class Db
    {
        // Your web app's Firebase configuration

        public string ApiKey { get; } = "AIzaSyAm-ccnoaXQ3XtvHpVQ5KL_rYBDqs27hNk";
        public string AuthDomain { get; } = "comuni-82642.firebaseapp.com";
        public string DatabaseURL { get; } = "https://comuni-82642.firebaseio.com";
        /* private string projectId = "comuni-82642";
         private string storageBucket = "comuni-82642.appspot.com";
         private string messagingSenderId = "540231429972";
         private string appId = "1:540231429972:web:5aeaa437c1e2f77e9de2c1";
         private string measurementId = "G-KRQXFG2YHV"; */

        public FirebaseClient Client()
        {
            return new FirebaseClient(DatabaseURL);
        }

        /*public async Task<List<GObject<object>>> GetAll(string url)
        {
            var data = await Client()
            .Child(url)
            .OnceAsync<Type>();

            List<GenericObject<string, object>> firebaseObjects = new List<GenericObject<string, object>();
            foreach (var x in data)
            {
                GenericObject<string, object> FirebaseObject = new GenericObject<string, object>();
                FirebaseObject.Key = x.Key;
                FirebaseObject.Object = x.Object;
                firebaseObjects.Add(FirebaseObject);
            }
            return firebaseObjects;
        }*/

        public async Task<FirebaseObject<object>> Create(object obj, string url)
        {
           var x = await Client()
          .Child(url)
          .PostAsync(obj);
            return x;
         }

        public async Task Update(object obj, string url)
        {
            await Client()
            .Child(url)
            .PutAsync(obj);
        }

        public async Task Delete(string url)
        {
            await Client()
            .Child(url)
            .DeleteAsync();
        }

        public async Task<FirebaseObject<object>> GetAllFromUrl(string Url, string Key)
        {
            var x = await Client()
              .Child(Url)
              .OrderByKey()
              .EqualTo(Key)
              .OnceSingleAsync<object>();
            return (FirebaseObject<object>) x;

        }


    }
}
