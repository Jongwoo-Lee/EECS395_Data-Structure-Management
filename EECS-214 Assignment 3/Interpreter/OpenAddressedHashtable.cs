using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public class OpenAddressedHashtable : Dictionary
    {
        public int m;
        public int oa_size;
        public double A = (Math.Sqrt(5) - 1) / 2;
        public string[] keyy;
        public object[] valuee;

        int MultHash(string s)
        {
            int sum = 0;
            foreach (char c in s)
                sum += (int)c;
            double result = m * (sum * A % 1.0);
            return (int)result;
        }

        public OpenAddressedHashtable(int size)
        {
            keyy = new string[size];
            valuee = new object[size];
            m = size;
            oa_size = 0;
        }
        public override void Store(string key, object value) 
        {
            for(int i = 0; i<m; i++){
                int j = (MultHash(key)+i)%m;
                if (keyy[j] == key)
                {
                    valuee[j] = value;
                    return;
                }
                else if(keyy[j] == null){
                    keyy[j] = key;
                    valuee[j] = value;
                    oa_size++;
                    return;
                }
            }
            throw new HashtableFullException();
        }
        public override object Lookup(string key)
        {
            for (int k = 0; k < m; k++)
            {
                int l = (MultHash(key) + k) % m;
                if (keyy[l] == key)
                {
                    return valuee[l];
                }
            }
            throw new DictionaryKeyNotFoundException(key);
        }
        public override int Count
        {
            get
            {
                return oa_size;
            }
        }
        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
