using System;
using System.Collections.Generic;

namespace Interpreter
{
    public class ListDictionary : Dictionary
    {
        public string name { get; set; }
        public object val { get; set; }
        public ListDictionary next { get; set; }

        ListDictionary first;
        ListDictionary last;
        int ld_size = 0;
        public ListDictionary()
        {
            throw new NotImplementedException();
        }

        public override void Store(string key, object value)
        {
            ListDictionary newNode = new ListDictionary();
            if (ld_size == 0)
            {
                first.name = key;
                first.val = value;
                first.next = newNode;
                first = newNode;
            }
            else
            {
                last = newNode;
                last.name = key;
                last.val = value;
                last.next = newNode;
                first = newNode;
            }
            ld_size++;
        }

        public override object Lookup(string key)
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get
            {
                return ld_size;
            }
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            // Fill this in with something like
            //for (var cell = start; cell != null; cell = cell.next)
            //     yield return new KeyValuePair<string, object>(cell.Key, cell.Value);
            // And remove this:
            ListDictionary c = last;
            for (var cell = last; cell != null; cell = cell.next )
            {
                yield return new KeyValuePair<string, object>(cell.name, cell.val);
            }
        }
    }
}
