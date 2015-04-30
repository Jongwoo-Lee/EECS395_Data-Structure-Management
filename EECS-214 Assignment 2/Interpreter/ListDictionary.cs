using System;
using System.Collections.Generic;

namespace Interpreter
{
    public class ListDictionary : Dictionary
    {
        public string name { get; set; }
        public object val { get; set; }
        public ListDictionary()
        {
            ListDictionary child;
            this.name  = null;
            this.val = null;
        }

        public override void Store(string key, object value)
        {
            this.name = key;
            this.val = value;

        }

        public override object Lookup(string key)
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            // Fill this in with something like
            //for (var cell = start; cell != null; cell = cell.next)
            //     yield return new KeyValuePair<string, object>(cell.Key, cell.Value);
            // And remove this:
            throw new NotImplementedException();
        }

        
    }
}
