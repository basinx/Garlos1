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
        List<Creature> creatures = new List<Creature>();
        Creature ccreature;
        int count;
        int currentindex;
        int valuew;
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
                Console.WriteLine("Editing creature file: " + filename);
                ccreature.DisplayStats();
                Console.Write("\n\nWhat do you want to do?");
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
                    if (Utility.WordMatch(choice, "next", picked) || Utility.WordMatch(choice, "forward", picked))
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
                            Console.WriteLine("No description specified");
                        }
                        picked = true;
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
                                ccreature.exp = valuew;
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

                    if (Utility.WordMatch(choice, "last", picked) || Utility.WordMatch(choice, "previous", picked) || Utility.WordMatch(choice, "back", picked))
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
