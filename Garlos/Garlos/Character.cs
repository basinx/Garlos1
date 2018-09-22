using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class Character
    {
        public string name;
        public int hp = 30;
        public int maxhp;
        public int mp;
        public int maxmp;
        public int str;
        public int dex;
        public int wis;
        public int con;
        public int attack;
        public int defense;

        public int level;
        public int exp;
        public int next;
        public int gold;

        public int statpoints;
        public List<Item> inventory = new List<Item>();
        public Item weapon;
        public Item armor;
        public Item shield;
        
        //{
        //get;
        //set;
        // }
        public Character()
        {
            name = "Johose";
            maxhp = 30;
            hp = 30;
            maxmp = 5;
            mp = 5;
            str = 6;
            dex = 6;
            wis = 6;
            con = 6;
            statpoints = 8;
            exp = 0;
            next = 100;
            gold = 10;
            calcstats();
        }
        public Character(String namein)
        {
            name = namein;
            maxhp = 30;
            hp = 30;
            maxmp = 5;
            mp = 5;
            str = 6;
            dex = 6;
            wis = 6;
            con = 6;
            statpoints = 8;
            exp = 0;
            next = 100;
            gold = 10;
            calcstats();
        }

        public void displayinv()
        {
            foreach (Item item in inventory)
            {
                item.DisplayItem();
                if(item.Equals(weapon) || item.Equals(armor) || item.Equals(shield))
                {
                    Console.Write(" EQ\n");
                }
                else
                {
                    Console.Write("\n");
                }
            }
        }
        public void equip(Item i)
        {
            if (i.type == "weapon")
            {
                if (i.Equals(weapon))
                {
                    Console.WriteLine("This weapon is already equipped.");
                }
                else
                {
                    weapon = i;
                    Console.WriteLine(i.name + " has been equipped");
                }   
            }
            if (i.type == "armor")
            {
                if (i.Equals(armor))
                {
                    Console.WriteLine("This armor is already equipped.");
                }
                else
                {
                    armor = i;
                    Console.WriteLine(i.name + " has been equipped");
                }
            }
            if (i.type == "shield")
            {
                if (i.Equals(shield))
                {
                    Console.WriteLine("This shield is already equiped.");
                }
                else
                {
                    shield = i;
                    Console.WriteLine(i.name + " has been equipped");
                }
            }
            calcstats();
            DisplayStats();
        }
        public void startingequip()
        {
            inventory.Add(new Item("dagger", "weapon", "damage", 4));
            weapon = inventory.Last();
            inventory.Add(new Item("cloin", "armor", "defense", 2));
            armor = inventory.Last();
            inventory.Add(new Item("buckler", "shield", "defense", 1));
            inventory.Last().AddProperty("deflect", 20);
            shield = inventory.Last();
            inventory.Add(new Item("flame sabre", "weapon", "damage", 13));
            inventory.Last().AddProperty("#rflaming", 5);


        }
        public void reset()
        {
            maxhp = 30;
            hp = 30;
            maxmp = 5;
            mp = 5;
            str = 6;
            dex = 6;
            wis = 6;
            con = 6;
            statpoints = 8;
            exp = 0;
            next = 100;
            gold = 10;
        }

        public void DisplayStats()
        {
            System.Threading.Thread.Sleep(50);
            Console.WriteLine("\nYour current stats are:\n");
            System.Threading.Thread.Sleep(50);
            Console.WriteLine("Name: " + name);
            System.Threading.Thread.Sleep(50);
            Console.WriteLine("HP: " + hp + "/" + maxhp);
            System.Threading.Thread.Sleep(50);
            Console.WriteLine("MP: " + mp + "/" + maxmp);
            System.Threading.Thread.Sleep(50);
            if(weapon != null) { Console.Write(weapon.ReturnItem() + "\t"); }
            if (armor != null) { Console.Write(armor.ReturnItem() + "\t"); }
            if (shield != null) { Console.Write(shield.ReturnItem()); }

            System.Threading.Thread.Sleep(50);
            Console.Write("\nStr:\t" + str + "\tDex:\t" + dex);
            System.Threading.Thread.Sleep(50);
            Console.Write("\nWis:\t" + wis + "\tCon:\t" + con);
            System.Threading.Thread.Sleep(50);
            Console.Write("\nAtt:\t" + attack + "\tDef:\t" + defense);
            System.Threading.Thread.Sleep(50);
            Console.Write("\nExp:\t" + exp + "/" + next + "\tGold:\t" + gold);
            System.Threading.Thread.Sleep(50);
            Console.Write("\n");
        }

        public void NewCharacter()
        {
            inventory.Clear();
            weapon = null;
            armor = null;
            shield = null;
            SaveMaker saver = new SaveMaker();
            string choice;
            bool picked = false;
            string mychar = "";
            bool confirm = false;
            while (string.IsNullOrWhiteSpace(mychar))
            {
                Console.Write("Welcome to Garlos, what is yon name? ");
                mychar = Console.ReadLine();
            }
            name = mychar;
            //Character character = new Character();
            reset();
            Console.WriteLine("Welcome " + name + "\nYour starting stats are as follows:\n");
            while (!confirm)
            {
                while (statpoints > 0)
                {
                    Console.WriteLine("HP: " + hp + "/" + maxhp);
                    Console.WriteLine("MP: " + mp + "/" + maxmp);
                    Console.WriteLine("Strength:     " + str);
                    Console.WriteLine("Dexterity:    " + dex);
                    Console.WriteLine("Wisdom:       " + wis);
                    Console.WriteLine("Constitution: " + con);
                    Console.WriteLine("\nYou currently have " + statpoints + " stat points to distribute\n");
                    Console.Write("What stat will you improve (hp gains by 5 and mp gains by 2)? ");
                    choice = Console.ReadLine();
                    
                    picked = false;
                    if (Utility.NotBlank(choice))
                    {
                        if (Utility.WordMatch(choice, "dexterity", picked))
                        {
                            Console.WriteLine("You chose dexterity!");
                            dex += 1;
                            picked = true;
                        }
                        if (Utility.WordMatch(choice, "strength", picked))
                        {
                            Console.WriteLine("You chose strength!");
                            str += 1;
                            picked = true;
                        }
                        if (Utility.WordMatch(choice, "wisdom", picked))
                        {
                            Console.WriteLine("You chose wisdom!");
                            wis += 1;
                            picked = true;
                        }
                        if (Utility.WordMatch(choice, "constitution", picked))
                        {
                            Console.WriteLine("You chose constitution!");
                            con += 1;
                            picked = true;
                        }
                        if (Utility.WordMatch(choice, "hp", picked))
                        {
                            Console.WriteLine("You chose hp!");
                            hp += 5;
                            maxhp += 5;
                            picked = true;
                        }
                        if (Utility.WordMatch(choice, "mp", picked))
                        {
                            Console.WriteLine("You chose mp!");
                            mp += 2;
                            maxmp += 2;
                            picked = true;
                        }
                        if (picked)
                        {
                            statpoints--;
                        }
                        else
                        {
                            Console.WriteLine("That did not register, please try again.\n");
                        }

                    }


                }
                calcstats();
                Console.WriteLine("You have finished character creation.  Your final stats are as follows:\n");
                Console.WriteLine("HP: " + hp + "/" + maxhp);
                Console.WriteLine("MP: " + mp + "/" + maxmp);
                Console.Write("\nStr:\t" + str + "\tDex:\t" + dex);
                Console.Write("\nWis:\t" + wis + "\tCon:\t" + con);
                Console.Write("\nAtt:\t" + attack + "\tDef:\t" + defense);


                picked = false;
                while (!picked)
                {
                    Console.Write("\nIs this acceptable (y/n)?");
                    choice = Console.ReadLine();
                    
                    if (Utility.WordMatch(choice, "yes", picked))
                    {
                        startingequip();
                        confirm = true;
                        picked = false;
                        while (!picked)
                        {
                            Console.Write("\nWill you save (y/n)?");
                            choice = Console.ReadLine();
                            if (Utility.WordMatch(choice, "yes", picked))
                            {
                                picked = true;
                                saver.SaveData(this, "savedata.xml");
                                Console.Write("\nData Saved.\n");
                                DisplayStats();
                                

                            }
                            if (Utility.WordMatch(choice, "no", picked))
                            {
                                picked = true;
                                DisplayStats();

                            }
                        }
                    }
                    if (Utility.WordMatch(choice, "no", picked))
                    {
                        picked = true;
                        reset();
                    }
                }

            }
            calcstats();
        }

        public void calcstats()
        {
            List<Item> eqinv = new List<Item>();
            attack = str;
            defense = dex;
            if (weapon != null) { eqinv.Add(weapon); }
            if (armor != null) { eqinv.Add(armor); }
            if (shield != null) { eqinv.Add(shield); }
            foreach (Item i in eqinv)
            {
                foreach(ItemAttribute a in i.attributes)
                {
                    if(a.atname == "damage") { attack += a.atvalue;  }
                    if(a.atname == "#rflaming") { attack += a.atvalue;  }
                    if(a.atname == "defense") { defense += a.atvalue; }
                }
            }

        }

    }
}
