using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class GameData
    {
        public string filename;
        public List<Room> rooms;
        public List<Creature> creatures;
        public string creaturefile;
        public Creature ccreature;
        public int creatureindex;


        public GameData()
        {
            rooms = new List<Room>();
            creatures = new List<Creature>();
            creaturefile = "none";
            creatureindex = 0;
        }




    }
}
