using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Garlos
{
    public class ItemEditor
    {
        List<Item> items = new List<Item>();
        Item citem;
        int count;
        int currentindex;
        int valuew;
        string choice;
        string filename;
        string tryfile;
        string keyw;
        bool picked;
        bool finished;
        SaveMaker csaver = new SaveMaker();

        public ItemEditor()
        {

        }

        public void MainMenu()
        {
            finished = false;
            while (!finished)
            {
                Console.WriteLine("Editing item file: " + filename);
                citem.DisplayStats();
                Console.Write("\n< > to change item, 'help' for help, What do you want to do?");
                choice = Console.ReadLine();
                picked = false;
                if (Utility.NotBlank(choice))
                {
                    if (Utility.WordMatch(choice, "done", picked))
                    {
                        File.Delete(filename);
                        csaver.SaveItems(items, filename);
                        Console.WriteLine(filename + " saved.");
                        picked = true;
                        finished = true;
                    }
                    if (Utility.WordMatch(choice, "next", picked) || Utility.WordMatch(choice, "forward", picked) || Utility.WordMatch(choice, ".", picked) || Utility.WordMatch(choice, ">", picked))
                    {
                        valuew = items.IndexOf(citem) + 1;
                        if (items.ElementAtOrDefault(valuew) != null)
                        {
                            citem = items.ElementAt(valuew);
                            currentindex = valuew;

                            Console.WriteLine("Switching to next item, index " + valuew);
                        }
                        else
                        {
                            Console.WriteLine("No more items.");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "name", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            citem.name = keyw;
                            Console.WriteLine("Items name changed to " + keyw);
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
                            citem.desc = keyw;
                            Console.WriteLine("Items description changed to " + keyw);
                        }
                        else
                        {
                            Console.WriteLine("No description specified");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "type", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            citem.type = keyw;
                            Console.WriteLine("Items type changed to " + keyw);
                        }
                        else
                        {
                            Console.WriteLine("No type specified");
                        }
                        picked = true;
                    }

                    /*might add item descriptions later
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
                    */
                    if (Utility.WordMatch(choice, "attributes", picked))
                    {
                        string newat;
                        int newvalue;
                        while (!Utility.WordMatch(choice, "2", false))
                        {
                            citem.DisplayAttributes();
                            Console.WriteLine("Enter new attribute (attribute #), '1' to clear or '2' for done");
                            choice = Console.ReadLine();
                            if (Utility.WordMatch(choice, "1", picked))
                            {
                                citem.attributes.Clear();
                            }
                            else
                            {
                                if (Utility.NotBlank(choice))
                                {
                                    newat = Utility.GetFirstWord(choice);
                                    keyw = Utility.GetKeyWord(choice);
                                    if (Utility.NotBlank(keyw) && Int32.TryParse(keyw, out newvalue))
                                    {
                                        Console.WriteLine("Adding attribute " + newat + " with value " + newvalue);
                                        citem.attributes.Add(new ItemAttribute(newat, newvalue));
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
                    if (Utility.WordMatch(choice, "help", picked))
                    {
                        Console.WriteLine("name = change item name");
                        //Console.WriteLine("description = change item description");
                        Console.WriteLine("type = change item type");
                        Console.WriteLine("attributes = change creature attributes");
                        Console.WriteLine("Usable item types: shield, weapon, armor");
                        Console.WriteLine("Valid item attributes: damage, defense, deflect (% chance), #rflaming");
                        Console.WriteLine("Yes, flaming needs to be red for it to work");
                    }

/* might add item levels later too
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
*/

                    if (Utility.WordMatch(choice, "value", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            if (Int32.TryParse(keyw, out valuew))
                            {
                                citem.value = valuew;
                                Console.WriteLine("Item value set to " + valuew);
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
                                
                                if (items.ElementAtOrDefault(valuew) != null)
                                {

                                    citem = items.ElementAt(valuew);
                                    currentindex = valuew;

                                    Console.WriteLine("Switching to item at index " + valuew);
                                }
                                else
                                {
                                    Console.WriteLine("No item at that index");
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
                        items.Add(new Item());
                        citem = items.Last();

                        currentindex = items.IndexOf(citem);
                        citem.templateindex = currentindex;
                        Console.WriteLine("New item created at index " + currentindex);
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "last", picked) || Utility.WordMatch(choice, "previous", picked) || Utility.WordMatch(choice, "back", picked) || Utility.WordMatch(choice, ",", picked) || Utility.WordMatch(choice, "<", picked))
                    {
                        valuew = items.IndexOf(citem) - 1;
                        if (items.ElementAtOrDefault(valuew) != null)
                        {
                            citem = items.ElementAt(valuew);
                            currentindex = valuew;
                            Console.WriteLine("Switching to previous item at index " + valuew);
                        }
                        else
                        {
                            Console.WriteLine("No more items.");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "save", picked))
                    {
                        keyw = Utility.GetKeyWord(choice);
                        if (Utility.NotBlank(keyw))
                        {
                            tryfile = keyw;
                            tryfile += ".items";
                            if (File.Exists(tryfile))
                            {
                                Console.Write("\nFile " + tryfile + " already exists.  Overwrite?");
                                choice = Console.ReadLine();
                                if (Utility.WordMatch(choice, "yes", picked))
                                {
                                    File.Delete(tryfile);
                                    csaver.SaveItems(items, tryfile);
                                    Console.WriteLine("New file " + tryfile + " overwritten.");

                                }
                                else
                                {
                                    Console.WriteLine("File not saved.");
                                }
                            }
                            else
                            {
                                csaver.SaveItems(items, tryfile);
                                Console.WriteLine("New file " + tryfile + " created.");
                                filename = tryfile;
                            }
                        }
                        else
                        {
                            File.Delete(filename);
                            csaver.SaveItems(items, filename);
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
                Console.Write("\nWelcome to the item editor.  NEW or LOAD file?  EXIT to exit.");
                choice = Console.ReadLine();
                if (Utility.NotBlank(choice))
                {
                    if (Utility.WordMatch(choice, "new", picked))
                    {
                        Console.Write("\nWhat file name?");
                        choice = Console.ReadLine();
                        if (Utility.NotBlank(choice))
                        {
                            filename = choice;
                            filename += ".items";
                            if (File.Exists(filename))
                            {
                                Console.Write("\nFile " + filename + " already exists.  Overwrite?");
                                choice = Console.ReadLine();
                                if (Utility.WordMatch(choice, "yes", picked))
                                {
                                    items.Clear();
                                    items.Add(new Garlos.Item("True Lies DVD", "basic"));
                                    citem = items.Last();

                                    currentindex = items.IndexOf(citem);
                                    citem.templateindex = currentindex;
                                    csaver.SaveItems(items, filename);
                                    Console.WriteLine("New file " + filename + " created.  Currently editing " + citem.name + " at index " + citem.templateindex);
                                    picked = true;
                                }
                                else
                                {
                                    Console.WriteLine("New file not created");
                                }

                            }
                            else
                            {
                                items.Clear();
                                items.Add(new Garlos.Item("True Lies DVD", "basic"));
                                citem = items.Last();

                                currentindex = items.IndexOf(citem);
                                citem.templateindex = currentindex;
                                csaver.SaveItems(items, filename);
                                Console.WriteLine("New file " + filename + " created.  Currently editing " + citem.name + " at index " + citem.templateindex);
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
                            filename += ".items";
                            if (File.Exists(filename))
                            {
                                try
                                {
                                    items = csaver.LoadItems(items, filename);
                                    citem = items.First();
                                    currentindex = items.IndexOf(citem);
                                    Console.WriteLine("Creature file " + filename + " loaded.  Currently editing " + citem.name + " at index " + citem.templateindex);
                                    picked = true;
                                }
                                catch
                                {
                                    Console.WriteLine(filename + " is not a valid item file!");
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
