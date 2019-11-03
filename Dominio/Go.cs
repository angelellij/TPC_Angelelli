using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    //Go = Generic Object
    public class Go<TObject> where TObject : new()
    {
        public string Key { get; set; } 
        public TObject Object { get; set; }

        public Go()
        {
            Object = new TObject();
        }
        public Go(TObject Object)
        {
            this.Object = new TObject();
            this.Object = Object;
        }
        public Go(string Key)
        {
            this.Key = Key;
            this.Object = new TObject();
        }
        public Go(string Key, TObject Object)
        {
            this.Key = Key;
            this.Object = new TObject();
            this.Object = Object;
        }
        public Go(Go<TObject> GObject)
        {
            this.Key = GObject.Key;
            this.Object = new TObject();
            this.Object = GObject.Object;
        }
        public Go(FirebaseObject<TObject> Object)
        {
            Key = Object.Key;
            this.Object = new TObject();
            this.Object = Object.Object;
        }

        public IDictionary<string, TObject> ToDictionary(IDictionary<string, TObject> Diction) {
            Diction.Add(Key, Object);
            return Diction;
        }
    }
}
