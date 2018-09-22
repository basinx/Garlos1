using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    
    public class Room
    {
        public string name;
        public string description;
        public int roomID;
        public int northexit;
        public int southexit;
        public int eastexit;
        public int westexit;
        public List<Item> items;
        public List<Creature> creatures;
        public List<int> spawnindex;
        

        public Room()
        {
            items = new List<Item>();
            creatures = new List<Creature>();
            spawnindex = new List<int>();
            
            name = "New Room";
            description = "Default Description";
            roomID = 0;
            northexit = -1;
            southexit = -1;
            eastexit = -1;
            westexit = -1;
        }

        public Room(string rname = "New Room", string rdesc = "Default Description", int rID = 0, int nexit = -1, int sexit = -1, int eexit = -1, int wexit = -1)
        {
            items = new List<Item>();
            creatures = new List<Creature>();
            spawnindex = new List<int>();
            name = rname;
            description = rdesc;
            roomID = rID;
            northexit = nexit;
            southexit = sexit;
            eastexit = eexit;
            westexit = wexit;
        }

        public string roomexits()
        {
            string exits = "";
            if(northexit != -1)
            {
                exits += "N";
            }
            if (eastexit != -1)
            {
                exits += "E";
            }
            if (southexit != -1)
            {
                exits += "S";
            }
            if (westexit != -1)
            {
                exits += "W";
            }
            return exits;
        }
    }


}
