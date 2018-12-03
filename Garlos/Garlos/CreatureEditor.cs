using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Garlos
{
    public class CreatureEditor
    {


        List<Item> items = new List<Item>();
        Item citem;
        int itemindex;
        string itemfile;
        


        List<Creature> creatures = new List<Creature>();
        Creature ccreature;
        int count;
        int currentindex;
        int valuew;
        int valuei;
        string choice;
        string filename;
        string tryfile;
        string keyw;
        bool picked;
        bool finished;
        SaveMaker csaver =  new SaveMaker();



        public CreatureEditor()
        {

        }


        public void MainMenu()
        {
            finished = false;
            while(!finished)
            {
                DisplayDeets();
                choice = Console.ReadLine();
                picked = false;
                if(Utility.NotBlank(choice))
                {
                    if(Utility.WordMatch(choice, "done", picked))
                    {
                        File.Delete(filename);
                        csaver.SaveCreatures(creatures, filename);
                        Console.WriteLine(filename + " saved.");
                        picked = true;
                        finished = true;
                    }
                    if (Utility.WordMatch(choice, "next", picked) || Utility.WordMatch(choice, "forward", picked) || Utility.WordMatch(choice, ".", picked) || Utility.WordMatch(choice, ">", picked))
                    {
                        valuew = creatures.IndexOf(ccreature) + 1;
                        if (creatures.ElementAtOrDefault(valuew) != null)
                        {
                            ccreature = creatures.ElementAt(valuew);
                            currentindex = valuew;

                            Console.WriteLine("Switching to next creature, index " + valuew);
                        }
                        else
                        {
                            Console.WriteLine("No more creatures.");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "name", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if(Utility.NotBlank(keyw))
                        {
                            ccreature.name = keyw;
                            Console.WriteLine("Creatures name changed to " + keyw);
                        }
                        else
                        {
                            Console.WriteLine("No name specified");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "faction", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            ccreature.faction = keyw;
                            Console.WriteLine("Creatures faction changed to " + keyw);
                        }
                        else
                        {
                            Console.WriteLine("No name specified");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "description", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            ccreature.description = keyw;
                            Console.WriteLine("Creatures description changed to " + keyw);
                        }
                        else
                        {
                            Console.WriteLine("No description specified");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "hostile", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if(Utility.WordMatch(keyw, "true", picked))
                            {
                                ccreature.hostile = true;
                                Console.WriteLine("Creature set to hostile");
                            }
                            else if(Utility.WordMatch(keyw, "false", picked))
                            {
                                ccreature.hostile = false;
                                Console.WriteLine("Creature set to passive");
                            }
                            else
                            {
                                Console.WriteLine("Invalid selection (use true/false)");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "equip", picked))
                    {
                        if (citem != null)
                        {
                            ccreature.citems.Add(citem);
                        }
                        else
                        {
                            Console.WriteLine("No item");
                        }
                    }
                    if (Utility.WordMatch(choice, "remove", picked))
                    {
                        string 
                        keyw = Utility.GetKeyWord(choice);
                        keyw.ToLower();
                        if(ccreature.citems.Find(x => x.name.ToLower().Contains(keyw)) != null)
                        {
                            ccreature.citems.Remove(ccreature.citems.Find(x => x.name.ToLower().Contains(keyw)));
                            Console.WriteLine("Item Removed");
                        }
                        else
                        {
                            Console.WriteLine("Creature not carrying an item by that name");
                        }
                    }
                    if (Utility.WordMatch(choice, "[", picked) || Utility.WordMatch(choice, "{", picked))
                    {
                        if (citem != null)
                        {
                            valuei = items.IndexOf(citem) - 1;
                            if (items.ElementAtOrDefault(valuei) != null)
                            {
                                citem = items.ElementAt(valuei);
                                itemindex = valuei;

                                Console.WriteLine("Switching to previous item, index " + valuei);
                            }
                            else
                            {
                                Console.WriteLine("No more items.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No item file");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "]", picked) || Utility.WordMatch(choice, "}", picked))
                    {
                        if (citem != null)
                        {
                            valuei = items.IndexOf(citem) + 1;
                            if (items.ElementAtOrDefault(valuei) != null)
                            {
                                citem = items.ElementAt(valuei);
                                itemindex = valuei;

                                Console.WriteLine("Switching to next item, index " + valuei);
                            }
                            else
                            {
                                Console.WriteLine("No more items.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No item file");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "ifile", picked) || Utility.WordMatch(choice, "itemfile", picked))
                    {
                        if (Utility.NotBlank(Utility.GetKeyWord(choice)))
                        {
                            itemfile = Utility.GetKeyWord(choice);
                            itemfile += ".items";
                        }
                        else
                        {
                            Console.Write("\nWhat item file will you load?");
                            itemfile = Console.ReadLine();
                            itemfile += ".items";
                        }

                        if (File.Exists(itemfile))
                        {
                            try
                            {
                                items = csaver.LoadItems(items, itemfile);
                                citem = items.First();
                                itemindex = items.IndexOf(citem);
                                Console.WriteLine("Item file " + itemfile + " loaded.  Currently viewing " + citem.name + " at index " + citem.templateindex);
                                picked = true;
                            }
                            catch
                            {
                                Console.WriteLine(itemfile + " is not a valid item file!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("File " + itemfile + " does not exist");
                        }
                    } 
                    if (Utility.WordMatch(choice, "attributes", picked))
                    {
                        string newat;
                        int newvalue;
                        while (!Utility.WordMatch(choice, "2", false))
                        {
                            ccreature.DisplayAttributes();
                            Console.WriteLine("Enter new attribute (attribute #), '1' to clear or '2' for done");
                            choice = Console.ReadLine();
                            if (Utility.WordMatch(choice, "1", picked))
                            {
                                ccreature.cattributes.Clear();
                            }
                            else
                            {
                                if (Utility.NotBlank(choice))
                                {
                                    newat = Utility.GetFirstWord(choice);
                                    keyw = Utility.GetKeyWord(choice);
                                    if(Utility.NotBlank(keyw) && Int32.TryParse(keyw, out newvalue))
                                    {
                                        Console.WriteLine("Adding attribute " + newat + " with value " + newvalue);
                                        ccreature.cattributes.Add(new CreatureAttribute(newat, newvalue));
                                    }
                                    else
                                    {
                                        Console.WriteLine("No value assigned or value not numeric");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please make a selection");
                                }
                                
                            }

                                
                            
                        }
                        picked = true;
                    }
                    if(Utility.WordMatch(choice, "help", picked))
                    {
                        Console.WriteLine("level = change creature level");
                        Console.WriteLine("name = change creature name");
                        Console.WriteLine("description = change creature description");
                        Console.WriteLine("hp = change creature hp");
                        Console.WriteLine("gold = change creature gold");
                        Console.WriteLine("exp = change creature exp");
                        Console.WriteLine("damage = change creature damage");
                        Console.WriteLine("defense = change creature defense");
                        Console.WriteLine("attributes = change creature attributes");
                    }
                    if (Utility.WordMatch(choice, "level", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if (Int32.TryParse(keyw, out valuew))
                            {
                                ccreature.level = valuew;
                                Console.WriteLine("Creatures level set to " + valuew);

                            }
                            else
                            {
                                Console.WriteLine("Not a valid number!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value entered!");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "hp", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if (Int32.TryParse(keyw, out valuew))
                            {
                                ccreature.hp = valuew;
                                ccreature.maxhp = valuew;
                                Console.WriteLine("Creatures hp set to " + valuew);

                            }
                            else
                            {
                                Console.WriteLine("Not a valid number!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value entered!");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "damage", picked) || Utility.WordMatch(choice, "attack", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if (Int32.TryParse(keyw, out valuew))
                            {
                                ccreature.attack = valuew;
                                Console.WriteLine("Creatures attack set to " + valuew);

                            }
                            else
                            {
                                Console.WriteLine("Not a valid number!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value entered!");
                        }
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "defense", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if (Int32.TryParse(keyw, out valuew))
                            {
                                ccreature.defense = valuew;
                                Console.WriteLine("Creatures defense set to " + valuew);

                            }
                            else
                            {
                                Console.WriteLine("Not a valid number!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value entered!");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "gold", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if (Int32.TryParse(keyw, out valuew))
                            {
                                ccreature.gold = valuew;
                                Console.WriteLine("Creatures gold set to " + valuew);

                            }
                            else
                            {
                                Console.WriteLine("Not a valid number!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value entered!");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "exp", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if (Int32.TryParse(keyw, out valuew))
                            {
                                ccreature.exp = valuew;
                                Console.WriteLine("Creatures experience reward set to " + valuew);

                            }
                            else
                            {
                                Console.WriteLine("Not a valid number!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value entered!");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "index", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if (Int32.TryParse(keyw, out valuew))
                            {
                                
                                if(creatures.ElementAtOrDefault(valuew) != null)
                                {
                                    
                                    ccreature = creatures.ElementAt(valuew);
                                    currentindex = valuew;

                                    Console.WriteLine("Switching to creature at index " + valuew);
                                }
                                else
                                {
                                    Console.WriteLine("No creature at that index");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Not a valid number!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value entered!");
                        }
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "new", picked))
                    {
                        creatures.Add(new Creature());
                        ccreature = creatures.Last();

                        currentindex = creatures.IndexOf(ccreature);
                        ccreature.templateindex = currentindex;
                        Console.WriteLine("New creature created at index " + currentindex);
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "last", picked) || Utility.WordMatch(choice, "previous", picked) || Utility.WordMatch(choice, "back", picked) || Utility.WordMatch(choice, ",", picked) || Utility.WordMatch(choice, "<", picked))
                    {
                        valuew = creatures.IndexOf(ccreature) - 1;
                        if (creatures.ElementAtOrDefault(valuew) != null)
                        {
                            ccreature = creatures.ElementAt(valuew);
                            currentindex = valuew;

                            Console.WriteLine("Switching to previous creature, index " + valuew);
                        }
                        else
                        {
                            Console.WriteLine("No more creatures.");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "save", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            tryfile = keyw;
                            tryfile += ".creatures";
                            if (File.Exists(tryfile))
                            {
                                Console.Write("\nFile " + tryfile + " already exists.  Overwrite?");
                                choice = Console.ReadLine();
                                if (Utility.WordMatch(choice, "yes", picked))
                                {
                                    File.Delete(tryfile);
                                    csaver.SaveCreatures(creatures, tryfile);
                                    Console.WriteLine("New file " + tryfile + " overwritten.");
                                    
                                }
                                else
                                {
                                    Console.WriteLine("File not saved.");
                                }
                            }
                            else
                            {
                                csaver.SaveCreatures(creatures, tryfile);
                                Console.WriteLine("New file " + tryfile + " created.");
                                filename = tryfile;
                            }
                        }
                        else
                        {
                            File.Delete(filename);
                            csaver.SaveCreatures(creatures, filename);
                            Console.WriteLine(filename + " overwritten.");
                        }
                        picked = true;
                    }
                }

                Console.Write("\n");
            }

        }
        public void DisplayDeets()
        {

            
            
            

            Parser parse = new Parser();
            parse.splitsize = (int)(Console.WindowWidth / 2.2);


            parse.AddCol1("#cEditing creature file: #w" + filename);
            parse.AddCol1("Name: " + ccreature.name + "ƒIndex: " + ccreature.templateindex + "ƒLevel: " + ccreature.level);
            parse.AddCol1("Desc: " + ccreature.description);
            parse.AddCol1("HP:#r " + ccreature.hp + "#wƒMP:#r " + ccreature.maxhp);
            parse.AddCol1("Damage: #r" + ccreature.attack + "ƒ#wDef: #r" + ccreature.defense);
            parse.AddCol1("Gold: #y" + ccreature.gold + "ƒ#wExp: #c" + ccreature.exp);
            parse.AddCol1("Hostile: #r" + ccreature.hostile + "ƒ#wFaction: #b" + ccreature.faction);
            foreach (CreatureAttribute ca in ccreature.cattributes)
            {
                parse.AddCol1("Attribute: #y" + ca.atname + "ƒ#wScore: #b" + ca.atvalue);
            }
            parse.AddCol1("Items:");
            foreach (Item ci in ccreature.citems)
            {
                parse.AddCol1("#y" + ci.name + "ƒ#wType: #b" + ci.type + "ƒ#wVal: #r" + ((ci.attributes.Any()) ? ci.attributes.First().atvalue.ToString() : "N/A"));
            }
            parse.AddCol1("< or > to change creature, 'help' for help, What do you want to do?");
            



            parse.AddCol2("#cItem file: #w" + itemfile);
            if (items.Any())
            {
                parse.AddCol2("Item Index:#c" + citem.templateindex);
                parse.AddCol2("Name:#c" + citem.name + "ƒ#wType:#c" + citem.type);
                parse.AddCol2("Desc:#c" + citem.desc);
                parse.AddCol2("Attributes:");
                foreach (ItemAttribute a in citem.attributes)
                {
                    parse.AddCol2("#y" + a.atname + "ƒ#b" + a.atvalue + "#w");
                }

            }
            parse.AddCol2("[ or ] to change, 'equip' or 'remove', 'ifile' to choose item file");

            parse.Display();


            

        }
        public void LaunchEditor()
        {

            picked = false;
            while (!picked)
            {
                Console.Write("\nWelcome to the creature editor.  NEW file or LOAD file?  EXIT to exit.");
                choice = Console.ReadLine();
                if (Utility.NotBlank(choice))
                {
                    if (Utility.WordMatch(choice, "new", picked))
                    {
                        Console.Write("\nWhat file name?");
                        choice = Console.ReadLine();
                        if(Utility.NotBlank(choice))
                        {
                            filename = choice;
                            filename += ".creatures";
                            if (File.Exists(filename))
                            {
                                Console.Write("\nFile " + filename + " already exists.  Overwrite?");
                                choice = Console.ReadLine();
                                if(Utility.WordMatch(choice, "yes", picked))
                                {
                                    creatures.Clear();
                                    creatures.Add(new Garlos.Creature("goblin", "a small goblin fuddles about", 1, 5, 5, 5, 3, 2));
                                    ccreature = creatures.Last();
                                    
                                    currentindex = creatures.IndexOf(ccreature);
                                    ccreature.templateindex = currentindex;
                                    csaver.SaveCreatures(creatures, filename);
                                    Console.WriteLine("New file " + filename + " created.  Currently editing " + ccreature.name + " at index " + ccreature.templateindex);
                                    picked = true;
                                }
                                else
                                {
                                    Console.WriteLine("New file not created");
                                }
                                
                            }
                            else
                            {
                                creatures.Clear();
                                creatures.Add(new Garlos.Creature("goblin", "a small goblin fuddles about", 1, 5, 5, 5, 3, 2));
                                ccreature = creatures.Last();

                                currentindex = creatures.IndexOf(ccreature);
                                ccreature.templateindex = currentindex;
                                csaver.SaveCreatures(creatures, filename);
                                Console.WriteLine("New file " + filename + " created.  Currently editing " + ccreature.name + " at index " + ccreature.templateindex);
                                picked = true;
                            }

                        }
                        else
                        {
                            Console.WriteLine("No file selected");
                        }
                        
                        
                    }
                    if (Utility.WordMatch(choice, "load", picked))
                    {
                        Console.Write("\nWhat file name?");
                        choice = Console.ReadLine();
                        if (Utility.NotBlank(choice))
                        {
                            filename = choice;
                            filename += ".creatures";
                            if (File.Exists(filename))
                            {
                                try
                                {
                                    creatures = csaver.LoadCreatures(creatures, filename);
                                    ccreature = creatures.First();
                                    currentindex = creatures.IndexOf(ccreature);
                                    Console.WriteLine("Creature file " + filename + " loaded.  Currently editing " + ccreature.name + " at index " + ccreature.templateindex);
                                    picked = true;
                                }
                                catch
                                {
                                    Console.WriteLine(filename + " is not a valid creature file!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("File " + filename + " does not exist");
                            }
                        }
                    }
                    if (Utility.WordMatch(choice, "exit", picked))
                    {
                        picked = true;
                    }
                }
            }
        }
    }
}
