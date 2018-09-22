using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Garlos
{
    
    public class RoomEditor
    {
        public GameData game = new GameData();
        
        public int currentroom;
        public int nextroom;
        public string choice;
        public string filename;
        public SaveMaker saver;
        public string tempcfile;
        public string temp;
        public bool found;
        public RoomEditor()
        {
            game.rooms = new List<Room>();
            game.creatures = new List<Creature>();
            game.rooms.Add(new Room("First Room", "First Room Description", 0, -1, -1, -1, -1));
            currentroom = 0;
            nextroom = 0;
            saver = new SaveMaker();
            
        }

        public void LoadFile()
        {

            bool filechosen = false;
            string gamefilename;
            int strlen;
            
            while (!filechosen)
            {
                Console.Write("\nGame File Name?");

                gamefilename = Console.ReadLine();
                gamefilename += ".game";
                if (Utility.NotBlank(gamefilename))
                {
                    if (File.Exists(gamefilename))
                    {
                        try
                        {
                            game = saver.LoadGameData(game, gamefilename);
                            filename = gamefilename;
                            filechosen = true;
                        }
                        catch
                        {
                            Console.Write("\nNot a valid game file!");
                        }

                    }
                    else
                    {
                        Console.Write("\nFile does not exist.  Create new file?");
                        choice = Console.ReadLine();
                        strlen = choice.Length;


                        if (Utility.WordMatch(choice, "yes", false))
                        {

                            filename = gamefilename;
                            game.filename = gamefilename;
                            saver.SaveGameData(game, filename);
                            Console.WriteLine("New rooms file " + gamefilename + " created\n");
                            filechosen = true;
                        }
                        else
                        {
                            Console.WriteLine("File not created.");
                        }
                        
                    }
                }
            }
        }

        public void MainMenu()
        {
            bool finished = false;
            
            string choice;
            bool picked = false;

            while (!finished)
            {
                DisplayRoom();
                Console.Write("\nWhat do you want to edit?  'help' for commands, 'done' to save & exit.");
                choice = Console.ReadLine();
                
                picked = false;
                if (Utility.NotBlank(choice))
                {

                    if (Utility.WordMatch(choice, "north", picked))
                    {
                        nextroom = game.rooms[currentroom].northexit;
                        if (nextroom == -1)
                        {
                            //Make new room, moving to it in the create/link subroutine if requested?
                            Console.Write("\nThere is not a room to the north.  'new' room or 'link' to a roomID?");
                            choice = Console.ReadLine();
                            picked = false;
                            if (Utility.WordMatch(choice, "new", picked))
                            {
                                CreateRoom("north");
                            }
                            if (Utility.WordMatch(choice, "link", picked))
                            {
                                LinkRoom("north");
                            }
                        }
                        else
                        {
                            //Move to this room.
                            currentroom = nextroom;
                            Console.WriteLine("OK\n");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "east", picked))
                    {
                        nextroom = game.rooms[currentroom].eastexit;
                        if (nextroom == -1)
                        {
                            //Make new room, moving to it in the create/link subroutine if requested?
                            Console.Write("\nThere is not a room to the east.  'new' room or 'link' to a roomID?");
                            choice = Console.ReadLine();
                            picked = false;
                            if (Utility.WordMatch(choice, "new", picked))
                            {
                                CreateRoom("east");
                            }
                            if (Utility.WordMatch(choice, "link", picked))
                            {
                                LinkRoom("east");
                            }
                        }
                        else
                        {
                            //Move to this room.
                            currentroom = nextroom;
                            Console.WriteLine("OK\n");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "west", picked))
                    {
                        nextroom = game.rooms[currentroom].westexit;
                        if (nextroom == -1)
                        {
                            //Make new room, moving to it in the create/link subroutine if requested?
                            Console.Write("\nThere is not a room to the west.  'new' room or 'link' to a roomID?");
                            choice = Console.ReadLine();
                            picked = false;
                            if (Utility.WordMatch(choice, "new", picked))
                            {
                                CreateRoom("west");
                            }
                            if (Utility.WordMatch(choice, "link", picked))
                            {
                                LinkRoom("west");
                            }
                        }
                        else
                        {
                            //Move to this room.
                            currentroom = nextroom;
                            Console.WriteLine("OK\n");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "south", picked))
                    {
                        nextroom = game.rooms[currentroom].southexit;
                        if (nextroom == -1)
                        {
                            //Make new room, moving to it in the create/link subroutine if requested?
                            Console.Write("\nThere is not a room to the south.  'new' room or 'link' to a roomID?");
                            choice = Console.ReadLine();
                            picked = false;
                            if (Utility.WordMatch(choice, "new", picked))
                            {
                                CreateRoom("south");
                            }
                            if (Utility.WordMatch(choice, "link", picked))
                            {
                                LinkRoom("south");
                            }
                        }
                        else
                        {
                            //Move to this room.
                            currentroom = nextroom;
                            Console.WriteLine("OK\n");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, ".", picked)  || Utility.WordMatch(choice, ">", picked)) //Next Creature
                    {
                        if (game.creatureindex + 1 >= game.creatures.Count())
                        {
                            game.ccreature = game.creatures.First();
                            game.creatureindex = game.creatures.IndexOf(game.ccreature);                 
                        }
                        else
                        {
                            game.creatureindex += 1;
                            game.ccreature = game.creatures.ElementAt(game.creatureindex);
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, ",", picked) || Utility.WordMatch(choice, "<", picked)) //Last Creature
                    {
                        if (game.creatureindex - 1 < 0)
                        {
                            game.ccreature = game.creatures.Last();
                            game.creatureindex = game.creatures.IndexOf(game.ccreature);
                        }
                        else
                        {
                            game.creatureindex -= 1;
                            game.ccreature = game.creatures.ElementAt(game.creatureindex);
                        }
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "place", picked))
                    {
                        Console.WriteLine("Placing " + game.ccreature.name + " in room");
                        game.rooms[currentroom].creatures.Add((Creature)game.ccreature.Clone());
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "spawn", picked))
                    {
                        game.rooms[currentroom].spawnindex.Add(game.ccreature.templateindex);
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "cspawn", picked))
                    {
                        game.rooms[currentroom].spawnindex.Clear();
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "kill", picked))
                    {

                        if (Utility.NotBlank(Utility.GetKeyWord(choice)))
                        {
                            temp = Utility.GetKeyWord(choice);

                        }
                        else
                        {
                            Console.WriteLine("Kill what?");
                            temp = Console.ReadLine();
                        }
                        found = false;
                        for(int i = 0; i < game.rooms[currentroom].creatures.Count(); i++)
                        {
                            if(Utility.AnyMatch(temp, game.rooms[currentroom].creatures[i].name, picked))
                            {
                                Console.WriteLine("Killing " + game.rooms[currentroom].creatures[i].name);
                                game.rooms[currentroom].creatures.Remove(game.rooms[currentroom].creatures[i]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine(game.rooms[currentroom].creatures[i].name + " does not match " + temp);
                            }
                        }
                        

                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "cfile", picked))
                    {
                        if(Utility.NotBlank(Utility.GetKeyWord(choice)))
                        {
                            tempcfile = Utility.GetKeyWord(choice);
                            tempcfile += ".creatures";
                        }
                        else
                        {
                            Console.Write("\nWhat creature file will you load?");
                            tempcfile = Console.ReadLine();
                            tempcfile += ".creatures";
                        }

                        if (File.Exists(tempcfile))
                        {
                            try
                            {
                                game.creatures = saver.LoadCreatures(game.creatures, tempcfile);
                                game.ccreature = game.creatures.First();
                                game.creatureindex = game.creatures.IndexOf(game.ccreature);
                                game.creaturefile = tempcfile;
                                Console.WriteLine("Creature file " + game.creaturefile + " loaded.  Currently viewing " + game.ccreature.name + " at index " + game.ccreature.templateindex);
                                
                            }
                            catch
                            {
                                Console.WriteLine(filename + " is not a valid creature file!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("File does not exist");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "name", picked))
                    {
                        if (Utility.NotBlank(Utility.GetKeyWord(choice)))
                        {
                            game.rooms[currentroom].name = Utility.GetKeyWord(choice);
                        }
                        else
                        {
                            Console.Write("\nWhat should the rooms name be?");
                            game.rooms[currentroom].name = Console.ReadLine();
                            
                        }
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "description", picked))
                    {
                        if(game.rooms[currentroom].description == "New Room Description")
                        {
                            game.rooms[currentroom].description = "";
                        }
                        Console.Write(game.rooms[currentroom].description);
                        Console.Write("\nType lines for the rooms description?  use /d for done and /c to clear.");
                        choice = Console.ReadLine();
                        while (choice != "/d")
                        {
                            if(choice == "/c")
                            {
                                game.rooms[currentroom].description = "";
                            }
                            else
                            {
                                if(game.rooms[currentroom].description == "")
                                {
                                    game.rooms[currentroom].description += choice;
                                }
                                else
                                {
                                    game.rooms[currentroom].description += "\n" + choice;
                                }
                                
                            }
                            Console.Write(game.rooms[currentroom].description);
                            Console.Write("\nType lines for the rooms description?  use /d for done and /c to clear.");
                            choice = Console.ReadLine();
                        }
                        picked = true;

                    }
                    if (Utility.WordMatch(choice, "help", picked))
                    {
                        Console.WriteLine("Save = save game file");
                        Console.WriteLine("Name = change room name");
                        Console.WriteLine("Desc = change room description");
                        Console.WriteLine("North, East, West, South = More to adjascent room with option to create new");
                    }
                    if (Utility.WordMatch(choice, "save", picked))
                    {
                        if (Utility.NotBlank(Utility.GetKeyWord(choice)))
                        {
                            filename = Utility.GetKeyWord(choice) + ".game";
                            game.filename = Utility.GetKeyWord(choice) + ".game";
                        }

                        saver.SaveGameData(game, filename);
                        Console.WriteLine("Game File " + filename + " saved!");
                    }
                    if (Utility.WordMatch(choice, "done", picked))
                    {

                        Console.Write("\nBe done with you!.\n");
                        picked = true;
                        finished = true;
                    }

                    if (picked)
                    {

                    }
                    else
                    {
                        Console.WriteLine("That did not register, please try again.\n");
                    }
                }
            }
            saver.SaveGameData(game, filename);
        }

        public void DisplayRoom()
        {
            
                
                
            Parser parse = new Parser();
            parse.splitsize = (int)(Console.WindowWidth / 2.2);

            parse.AddCol1("#cWorking on game file: " + game.filename);
            
            parse.AddCol1("Room Name:" + game.rooms[currentroom].name);
            parse.AddCol1("Room Description:\n" + game.rooms[currentroom].description);
            parse.AddCol1("Room ID:" + game.rooms[currentroom].roomID);
            parse.AddCol1("Room North:" + game.rooms[currentroom].northexit);
            parse.AddCol1("Room South:" + game.rooms[currentroom].southexit);
            parse.AddCol1("Room East:" + game.rooms[currentroom].eastexit);
            parse.AddCol1("Room West:" + game.rooms[currentroom].westexit);
            parse.AddCol1("Exits:" + game.rooms[currentroom].roomexits());
            parse.AddCol1("#cThis room contains:");
            parse.AddCol2("#cCreature file: " + game.creaturefile);
            if (game.creatures.Any())
            {
                parse.AddCol2("Current Creature:" + game.ccreature.name);
                parse.AddCol2("Creature Description: " + game.ccreature.description);
                parse.AddCol2("Creature Index:" + game.ccreature.templateindex);
                parse.AddCol2("Creature HP:" + game.ccreature.maxhp);
                parse.AddCol2("Creature Attack: " + game.ccreature.attack);
                parse.AddCol2("Creature Gold: " + game.ccreature.gold);
                parse.AddCol2("Creature Exp: " + game.ccreature.exp);
                parse.AddCol2("Creature Faction: " + game.ccreature.faction);
                parse.AddCol2("< or > to change 'place', 'kill', 'spawn' to set a spawn, 'cspawn' to clear spawns, 'cf' to choose creature file");
            }
            for (int i = 0; i < game.rooms[currentroom].items.Count(); i++)
            {
                parse.AddCol1(game.rooms[currentroom].items[i].name);
            }
            parse.AddCol1("#cCreatures in room:");
            for (int i = 0; i < game.rooms[currentroom].creatures.Count(); i++)
            {
                parse.AddCol1(game.rooms[currentroom].creatures[i].name);
            }
            parse.AddCol1("#cSpawns in room:");
            foreach(int i in game.rooms[currentroom].spawnindex)
            {
                if (game.creatures.Count() > i)
                {
                    parse.AddCol1(game.creatures[i].name);
                }
            }
            parse.Display();
            /*
            
            Console.WriteLine("Room Name:" + game.rooms[currentroom].name);
            //Console.WriteLine("Room Description:\n" + game.rooms[currentroom].description);
            Utility.Colorize("Room Description:\n" + game.rooms[currentroom].description);
            Console.WriteLine("Room ID:" + game.rooms[currentroom].roomID);
            Console.WriteLine("Room North:" + game.rooms[currentroom].northexit);
            Console.WriteLine("Room South:" + game.rooms[currentroom].southexit);
            Console.WriteLine("Room East:" + game.rooms[currentroom].eastexit);
            Console.WriteLine("Room West:" + game.rooms[currentroom].westexit);
            Console.WriteLine("Exits:" + game.rooms[currentroom].roomexits());
            Console.WriteLine("This room contains:");
            for (int i = 0; i < game.rooms[currentroom].items.Count(); i++)
            {
                Console.WriteLine(game.rooms[currentroom].items[i].name);
            }
            Console.WriteLine("Creatures in room:");
            for (int i = 0; i < game.rooms[currentroom].creatures.Count(); i++)
            {
                Console.WriteLine(game.rooms[currentroom].creatures[i].description);
            }
            */


        }

        public void LinkRoom(string exit)
        {


            
            Console.Write("\nEnter the ID of the room you wish to link to:");
            choice = Console.ReadLine();
            int newroomID = new int();
            if (Int32.TryParse(choice, out newroomID))
            {

                    bool canlink = false;
                    if (game.rooms.ElementAtOrDefault(newroomID) == null)
                    {
                        Utility.Colorize("\n#cNo room at ID " + newroomID + "\n");
                    /*
                        choice = Console.ReadLine();
                        if (Utility.WordMatch(choice, "yes", false))
                        {
                            game.rooms[newroomID] = new Room("New Room", "New Room Description", newroomID, -1, -1, -1, -1);
                            Console.WriteLine("\nNew room has been created.");
                            canlink = true;
                        }
                        else
                        {
                            Console.WriteLine("\nNew room not created.");
                        }
                        */
                    }
                    else
                    {
                        canlink = true;
                    }
                    if (canlink)
                    {
                        if (exit == "north")
                        {
                            game.rooms[currentroom].northexit = newroomID;
                            
                            Console.WriteLine("\nRoom" + newroomID + " linked to north exit");
                        }
                        if (exit == "east")
                        {
                            game.rooms[currentroom].eastexit = newroomID;
                            
                            Console.WriteLine("\nRoom" + newroomID + " linked to east exit");
                        }
                        if (exit == "west")
                        {
                            game.rooms[currentroom].westexit = newroomID;
                            
                            Console.WriteLine("\nRoom" + newroomID + " linked to west exit");
                        }
                        if (exit == "south")
                        {
                            game.rooms[currentroom].southexit = newroomID;
                            
                            Console.WriteLine("\nRoom" + newroomID + " linked to south exit");
                        }
                        Console.Write("\nSwitch to this room now?");
                        choice = Console.ReadLine();
                        if (Utility.WordMatch(choice, "yes", false))
                        {
                            currentroom = newroomID;
                        }
                        else
                        {
                            Console.WriteLine("OK");
                        }
                    }

            }
            else
            {
                Console.WriteLine("\nNot a valid number.\n");
            }

            
        }
        public void CreateRoom(string exit)
        {

                game.rooms.Add(new Room("New Room", "New Room Description", game.rooms.Count, -1, -1, -1, -1));
                int firstnullroom = game.rooms.Count - 1;
                if (exit == "north")
                {
                    game.rooms[currentroom].northexit = firstnullroom;
                    game.rooms[firstnullroom].southexit = currentroom;
                }
                if (exit == "east")
                {
                    game.rooms[currentroom].eastexit = firstnullroom;
                    game.rooms[firstnullroom].westexit = currentroom;
                }
                if (exit == "west")
                {
                    game.rooms[currentroom].westexit = firstnullroom;
                    game.rooms[firstnullroom].eastexit = currentroom;
                }
                if (exit == "south")
                {
                    game.rooms[currentroom].southexit = firstnullroom;
                    game.rooms[firstnullroom].northexit = currentroom;
                }
                Console.WriteLine("New room created at index " + firstnullroom);
                Console.Write("\nSwitch to this room now?");
                choice = Console.ReadLine();
                if (Utility.WordMatch(choice, "yes", false))
                {
                    currentroom = firstnullroom;
                }
                else
                {
                    Console.WriteLine("OK");
                }

            
        }

        public static bool NullRoom(Room roomtest)
        {
            return roomtest == null;
        }

        public static bool NullItem(Item itemtest)
        {
            return itemtest == null;
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
