using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    class Combat
    {
        Random rng = new Random();
        bool woncombat = false;
        int damage;
        int target = 0;
        public Character cplayer;
        public List<Creature> creatures;
        public List<Item> equippeditems = new List<Item>();
        string situation;

        public Combat(Character ccharin, List<Creature> creaturesin, string sitch = "normal")
        {

            cplayer = ccharin;
            creatures = creaturesin;
            situation = sitch;
            equippeditems.Add(cplayer.weapon);
            equippeditems.Add(cplayer.armor);
            equippeditems.Add(cplayer.shield);
        }

        private int DTarget()
        {
            return target + 1;
        }

        public int CalcDamage(Item cw, Creature ct)
        {
            int dam = 0;
            int rngattack = 0;
            int rngdefense = 0;
            int cdam = 0;
            foreach(ItemAttribute a in cw.attributes)
            {
                if(a.atname == "damage")
                {
                    rngattack = rng.Next(1, a.atvalue + (cplayer.str / 2));
                    rngdefense = rng.Next(0, ct.defense);
                    dam = rngattack - rngdefense;
                    if(dam < 1)
                    {
                        dam = 1;
                    }
                    Console.WriteLine("Basic Damage roll:" + rngattack + " vs Defense roll:" + rngdefense + " = " + dam + " damage");
                    System.Threading.Thread.Sleep(50);
                    cdam += dam;
                }
                if(a.atname == "#rflaming")
                {
                    rngattack = rng.Next(1, a.atvalue);
                    Utility.Colorize("#rFlaming#w Damage roll:" + rngattack);
                    System.Threading.Thread.Sleep(50);
                    cdam += rngattack;
                }
            }

            Utility.Colorize("Total Damage: #y" + cdam);
            System.Threading.Thread.Sleep(200);
            return cdam;
        }

        public int CalcEnemyAttack(Creature ct)
        {
            int dam = 0;
            
            int rngdefense = 0;

            bool deflected = false;

            dam = rng.Next(1, ct.attack);
            Console.WriteLine(ct.name + " attacks with a damage roll of " + dam);
            System.Threading.Thread.Sleep(50);
            foreach (Item i in equippeditems)
            {
                foreach(ItemAttribute a in i.attributes)
                {
                    if(a.atname == "defense")
                    {
                        rngdefense = rng.Next(1, a.atvalue + (cplayer.dex / 4));
                        dam -= rngdefense;
                        if(dam <= 0) { dam = 1; }
                        Console.WriteLine(i.name + " reduced damage by " + rngdefense + " : " + dam + " damage");
                        System.Threading.Thread.Sleep(50);
                    }
                    if(a.atname == "deflect")
                    {
                        rngdefense = rng.Next(1, 100);
                        if(rngdefense < a.atvalue)
                        {
                            Console.WriteLine("Attack was deflected by " + i.name);
                            System.Threading.Thread.Sleep(50);
                            deflected = true;
                        }
                    }
                }
            }
            System.Threading.Thread.Sleep(200);
            if (deflected == true)
            {
                return 0;
            }
            else
            {
                return dam;
            }
        }
        
        public bool Battle()
        {
            bool incombat = true;
            string choice;
            
            bool defeated = false;
            bool doneturn = false;
            bool picked = false;
            Console.Write("\n");
            Console.WriteLine("You have entered battle!");
            while (incombat)
            {
                //player takes his turn
                while (!doneturn)
                {
                    Console.WriteLine("The situation is " + situation);
                    cplayer.DisplayStats();
                    foreach (Creature c in creatures)
                    {
                        Console.WriteLine((creatures.IndexOf(c) + 1) + ") " + c.name + " stands before you!  It" + c.Condition());
                    }
                    Console.WriteLine("You are targetting enemy " + DTarget() + " - " + creatures[target].name);

                    Console.Write("\n");

                    Console.Write("Your move sir.  Attack or change target?");
                    choice = Console.ReadLine();
                    if (Utility.WordMatch(choice, "attack", picked))
                    {
                        Console.WriteLine("You swing and attack " + creatures[target].name + " with " + cplayer.weapon.name);
                        damage = CalcDamage(cplayer.weapon, creatures[target]);
                        creatures[target].hp -= damage;
                        
                        picked = true;
                        doneturn = true;
                    }
                    if (Utility.WordMatch(choice, "change", picked))
                    {
                        bool foundenemy = false;
                        foreach (Creature c in creatures)
                        {
                            if (Utility.KeyWord(choice, (creatures.IndexOf(c) + 1).ToString()) && foundenemy == false)
                            {
                                target = creatures.IndexOf(c);
                                Console.WriteLine("Now targetting enemy " + DTarget() + " - " + c.name);
                                foundenemy = true;
                            }

                        }
                        if (foundenemy == false)
                        {
                            foreach (Creature c in creatures)
                            {
                                if (Utility.KeyWord(choice, c.name) && foundenemy == false)
                                {
                                    target = creatures.IndexOf(c);
                                    Console.WriteLine("Now targetting enemy " + DTarget() + " - " + c.name);
                                    foundenemy = true;
                                }

                            }
                        }
                        if (foundenemy == false)
                        {
                            Console.WriteLine(Utility.GetKeyWord(choice) + " not found");
                        }
                        
                        picked = true;

                    }
                    picked = false;
                }
                //Enemys take their turn
                foreach (Creature c in creatures)
                {
                    if (c.hp > 0)
                    {
                        Console.WriteLine(c.name + " is attacking!");
                        damage = CalcEnemyAttack(c);
                        cplayer.hp -= damage;
                    }

                }
                if (cplayer.hp > 0)
                {
                    if (creatures[target].hp <= 0)
                    {
                        bool newtarget = false;
                        Console.WriteLine("You have slain " + creatures[target].name);
                        foreach (Creature c in creatures)
                        {
                            if (c.hp > 0)
                            {
                                target = creatures.IndexOf(c);
                                
                                newtarget = true;
                                break;
                            }
                        }

                        if (newtarget)
                        {
                            Console.WriteLine("Now targetting " + creatures[target].name);
                        }
                        else
                        {
                            Console.WriteLine("You have defeated all opponents!");
                            woncombat = true;
                            incombat = false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You have died...");
                    defeated = true;
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                doneturn = false;
                picked = false;
            }
            return woncombat;
        }
    }
}
