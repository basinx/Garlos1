using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class Item
    {
        public string name = "new object";
        public int value = 10;
        public string type = "none";
        public int templateindex = 0;

        public List<ItemAttribute> attributes = new List<ItemAttribute>();
        

        public Item()
        {
            
        }
        public Item(string n, string t)
        {
            name = n;
            type = t;
        }
        public Item(string n, string t, string a, int v)
        {
            name = n;
            type = t;
            attributes.Add(new Garlos.ItemAttribute(a, v));
            
        }

        public void AddProperty(string a, int v)
        {
            if (attributes.Exists(f => f.atname == a))
            {
                Console.WriteLine("Property " + a + " already exists on item.");
            }
            else
            {
                Console.WriteLine("Property " + a + " added to " + name);
                attributes.Add(new Garlos.ItemAttribute(a, v));
            }
        }

        public void DisplayItem()
        {
            Console.Write(name + " - " + type + "(");
            foreach(ItemAttribute a in attributes)
            {
                Console.Write(a.atvalue);
                if (!attributes.Last().Equals(a))
                {
                    Console.Write(",");
                }
            }
            Console.Write(")");
        }

        //for use in the editor
        public void DisplayStats()
        {
            Utility.Colorize("Item Index:#c" + templateindex);
            Utility.Colorize("Name:#c" + name + "\t#wType:#c" + type);
            Console.WriteLine("Attributes:");
            foreach (ItemAttribute a in attributes)
            {
                Utility.Colorize("#y" + a.atname + "\t#b" + a.atvalue + "#w");
            }
            
        }

        public void DisplayAttributes()
        {
            foreach (ItemAttribute ia in attributes)
            {
                Utility.Colorize("Attribute: #y" + ia.atname + "\t#wScore: #b" + ia.atvalue);
            }
        }

        public void FullDetails()
        {
            Console.Write(name + " - " + type + "\n");
            foreach (ItemAttribute a in attributes)
            {
                Utility.Colorize(a.atname + " = " + a.atvalue);

            }
        }

        public string ReturnItem()
        {
            string itemdet = name;
            if(attributes.Any())
            {
                itemdet += " (" + attributes.First().atvalue + ")";
            }
            return itemdet;
        }


    }
    /*
    public struct ItemAttribute
    {

        public string atname;
        public int atvalue;

        public ItemAttribute(string name, int value)
        {
            atname = name;
            atvalue = value;
        }
    }
    */

}
