using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class CreatureAttribute
    {
        public string atname;
        public int atvalue;

        public CreatureAttribute()
        {
            atname = "basic";
            atvalue = 1;
        }
        public CreatureAttribute(string name, int value)
        {
            atname = name;
            atvalue = value;
        }
    }
}
