using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class CreatureSpawn
    {
        public int cindex;
        public int count;
        public CreatureSpawn()
        {
            cindex = 0;
            count = 0;
        }

        public CreatureSpawn(int ci, int co)
        {
            cindex = ci;
            count = co;
        }
    }
}
