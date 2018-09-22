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
        public string type = "none";

        public List<ItemAttribute> attributes = new List<ItemAttribute>();
        

        public Item()
        {
            
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
