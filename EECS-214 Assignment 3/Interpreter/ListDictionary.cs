using System;
using System.Collections.Generic;

namespace Interpreter
{
    public class ListDictionary : Dictionary
    {
        public string name { get; set; }
        public object val { get; set; }
        public ListDictionary child { get; set; }

        public int ldsize;
        public ListDictionary newkey;
        public ListDictionary first;

        public ListDictionary()
        {
            ldsize = 0;
            first = null;
        }

        public override void Store(string key, object value)
        {
            ListDictionary node = new ListDictionary() { name = key, val = value };

            if (first == null)
            {
                first = node;
                ldsize++;
            }
            else
            {
                int i;
                ListDictionary LD = first;
                for (i = 0; i < ldsize; i++)
                {
                    if (LD.name == key)
                    {
                        LD.val = value;
                        return;
                    }
                    LD = LD.child;
                }
                newkey.child = node;
                ldsize++;

            }
            newkey = node;
        }

        public override object Lookup(string key)
        {
            ListDictionary LU = first;
            for (int j = 0; j < Count; j++)
            {
                if (LU.name == key)
                {
                    return LU.val;
                }
                LU = LU.child;
            }
            throw new DictionaryKeyNotFoundException(key);
        }

        public override int Count
        {
            get
            {
                return ldsize;
            }
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            // Fill this in with something like
            for (var cell = newkey; cell != null; cell = cell.child)
                yield return new KeyValuePair<string, object>(cell.name, cell.val);
        }
    }
}