﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Database.Query;

namespace Negocio
{
    public class Db
    {
        // Your web app's Firebase configuration

        private string apiKey = "AIzaSyAm-ccnoaXQ3XtvHpVQ5KL_rYBDqs27hNk";
        public string authDomain = "comuni-82642.firebaseapp.com";
        public string  databaseURL = "https://comuni-82642.firebaseio.com";
        private string projectId = "comuni-82642";
        private string storageBucket = "comuni-82642.appspot.com";
        private string messagingSenderId = "540231429972";
        private string appId = "1:540231429972:web:5aeaa437c1e2f77e9de2c1";
        private string measurementId = "G-KRQXFG2YHV";    

        public FirebaseClient Client()
        {
        return new FirebaseClient(databaseURL);
        }
        public FirebaseAuthProvider FirebaseNewUser()
        {
            return new FirebaseAuthProvider(new FirebaseConfig(this.apiKey));
        }
        
        public async Task CreateInUrl(object obj, string url)
        {
            var result = await Client()
          .Child(url)
          .PostAsync(obj);
            return;
         }
        
    }
}
