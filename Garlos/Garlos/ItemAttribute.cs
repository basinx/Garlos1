using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class ItemAttribute
    {

        public string atname;
        public int atvalue;

        public ItemAttribute()
        {
            atname = "newattribute";
            atvalue = 1;
        }
        public ItemAttribute(string name, int value)
        {
            atname = name;
            atvalue = value;
        }
    }
}
