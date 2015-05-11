using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public class OpenAddressedHashtable : Dictionary
    {
        int m;
        int oa_size;
        double A = (Math.Sqrt(5) - 1) / 2;
        int MultHash(string s)
        {
            int sum = 0;
            foreach (char c in s)
                sum += (int)c;
            double result = m * (sum * A % 1.0);
            return (int)result;
        }
        public string o_key { get; set; }
        public object val { get; set; }

        OpenAddressedHashtable[] array;

        public OpenAddressedHashtable(int size)
        {
            OpenAddressedHashtable[] array = new OpenAddressedHashtable[size];
            m = size;
            oa_size = 0;
        }
        public override void Store(string key, object value) 
        {
            for(int i = 0; i<m; i++){
                int j = (MultHash(key)+i)%m;
                if (array[j].o_key == key)
                {
                    array[j].val = value;
                    oa_size++;
                    return;
                }
                else if(array[j].o_key == null){
                    array[j].o_key = key;
                    array[j].val = value;
                    oa_size++;
                    return;
                }
            }
            throw new HashtableFullException();
        }
        public override object Lookup(string key)
        {
            throw new NotImplementedException();

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
