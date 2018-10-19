using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class Creature : ICloneable
    {
        public string name;
        public string description;
        public int templateindex;
        public int level;
        public int hp;
        public int maxhp;
        public int attack;
        public int defense;
        public int gold;
        public int exp;
        public bool hostile;
        public string faction;
        public List<CreatureAttribute> cattributes;
        public List<Item> citems;



        public Creature()
        {
            name = "hobgoblin";
            description = "a small hobgoblin looks like it's going to cry.";
            level = 1;
            hp = 10;
            maxhp = 10;
            attack = 3;
            defense = 1;
            gold = 8;
            exp = 12;
            hostile = false;
            faction = "basic";
            cattributes = new List<CreatureAttribute>();
            citems = new List<Item>();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Creature(string cname, string cdesc)
        {
            name = cname;
            description = cdesc;
        }

        public void DisplayStats()
        {
            Utility.Colorize("Name: " + name + "\tIndex: " + templateindex + "\tLevel: " + level);
            Utility.Colorize("Desc: " + description);
            Utility.Colorize("HP: #r" + hp + "#w/#r" + maxhp);
            Utility.Colorize("Damage: #r" + attack + "\t#wDef: #r" + defense);
            Utility.Colorize("Gold: #y" + gold + "\t#wExp: #c" + exp);
            Utility.Colorize("Hostile: #r" + hostile + "\t#wFaction: #b" + faction);
            foreach (CreatureAttribute ca in cattributes)
            {
                Utility.Colorize("Attribute: #y" + ca.atname + "\t#wScore: #b" + ca.atvalue);
            }
            foreach (Item ci in citems)
            {
                Utility.Colorize("Item: #y" + ci.name + "\t#wType: #b" + ci.type + "\t#wVal: #r" + ((ci.attributes.Any()) ? ci.attributes.First().atvalue.ToString() : "N/A"));
            }
        }
        public void DisplayAttributes()
        {
            foreach (CreatureAttribute ca in cattributes)
            {
                Utility.Colorize("Attribute: #y" + ca.atname + "\t#wScore: #b" + ca.atvalue);
            }
        }

        public Creature(string cname, string cdesc, int clev, int chp, int cattk, int cdef, int cgold, int cexp)
        {
            name = cname;
            description = cdesc;
            level = clev;
            hp = chp;
            maxhp = chp;
            attack = cattk;
            defense = cdef;
            gold = cgold;
            exp = cexp;
            hostile = false;
            faction = "basic";
            cattributes = new List<CreatureAttribute>();

        }

        public string Condition()
        {
            if (hp > 0)
            {
                float cond = (float)maxhp / (float)hp;
                if (cond == 1)
                {
                    return " is in perfect condition.";
                }
                else if (cond < 2)
                {
                    return " is slightly injured";
                }
                else if (cond < 5)
                {
                    return " is badly wounded";
                }
                else
                {
                    return " is all fucked up";
                }
            }
            else
            {
                return " is dead";
            }

        }
    }


}
