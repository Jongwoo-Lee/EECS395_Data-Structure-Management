using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public class ChainedHashtable : Dictionary
    {
        public int m;
        public int ch_size;
        public int hk;

        double A = (Math.Sqrt(5) - 1) / 2;

        ListDictionary[] hash;
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
            hash = new ListDictionary[size];
            m = size;
            ch_size = 0;
        }

        public override void Store(string key, object value)
        {
            hk = MultHash(key);
            if (hash[hk] == null)
            {
                hash[hk] = new ListDictionary();
                hash[hk].Store(key, value);
                ch_size++;
            }
            else
            {
                hash[hk].Store(key, value);
            }
        }
        public override object Lookup(string key)
        {
            hk = MultHash(key);
            try { hash[hk].Lookup(key); }
            catch { throw new DictionaryKeyNotFoundException(key); }
            return hash[hk].Lookup(key);
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
