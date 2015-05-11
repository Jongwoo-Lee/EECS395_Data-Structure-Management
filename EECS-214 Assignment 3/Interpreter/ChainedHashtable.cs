using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public class ChainedHashtable : Dictionary
    {
        int m;
        int ch_size;
        int hk;

        double A = (Math.Sqrt(5) - 1) / 2;
        public string c_key { get; set; }
        public object val { get; set; }
        public ChainedHashtable next { get; set; }

        ChainedHashtable mid;
        ChainedHashtable[] hash;
        int MultHash(string s)
        {
            int sum = 0;
            foreach (char c in s)
                sum += (int)c;
            double result = m*(sum*A % 1.0);
            return (int) result;
        }
        public ChainedHashtable(int size)
        {
            ChainedHashtable[] hash = new ChainedHashtable[size];
            m = size;
            ch_size = 0;
        }

        public override void Store(string key, object value)
        {
            hk = MultHash(key);
            if (hash[hk] == null)
            {
                hash[hk].c_key = key;
                hash[hk].val = value;
                ch_size++;
                return;
            }
            else if (key == hash[hk].c_key)
            {
                hash[hk].val = value;
                return;
            }
            else
            {
                if (hash[hk].next == null)
                {
                    ChainedHashtable newNode = hash[hk].next;
                    newNode.c_key = key;
                    newNode.val = value;
                    hash[hk].next = newNode;
                }
                else
                {
                    ChainedHashtable newNode = hash[hk];

                    while (newNode.next != null)
                    {
                        newNode = newNode.next;
                        if (newNode.c_key == key)
                        {
                            newNode.val = value;
                            return;
                        }
                    }
                    newNode.c_key = key;
                    newNode.val = value;
                }
            }
        }
        public override object Lookup(string key)
        {
            throw new NotImplementedException();

        }
        public override int Count
        {
            get
            {
                return ch_size;
;
            }
        }
        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }



    }
}
