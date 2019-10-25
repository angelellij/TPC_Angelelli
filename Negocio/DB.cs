using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;

namespace Negocio
{
    public class Db
    {
        // Your web app's Firebase configuration

        public string apiKey = "AIzaSyAm-ccnoaXQ3XtvHpVQ5KL_rYBDqs27hNk";
        public string authDomain = "comuni-82642.firebaseapp.com";
        public string databaseURL = "https://comuni-82642.firebaseio.com";
        public string projectId = "comuni-82642";
        public string storageBucket = "comuni-82642.appspot.com";
        public string messagingSenderId = "540231429972";
        public string appId = "1:540231429972:web:5aeaa437c1e2f77e9de2c1";
        public string measurementId = "G-KRQXFG2YHV";    

        public FirebaseClient Client()
        {
        return new FirebaseClient(databaseURL);
        }
        

    }
}
