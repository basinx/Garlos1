using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Garlos
{
    class Adventure
    {
        Random rng = new Random();
        public GameData game;
        public List<Room> rooma;
        int nextroom;
        public Character character;
        string choice;
        int currentroom = 0;
        SaveMaker saver = new SaveMaker();

        public Adventure()
        {
            game = new GameData();
            rooma = new List<Room>();
        }

        public void BeginAdventure(Character chara)
        {
            character = chara;
            RespawnEnemies();
        }
        public void RespawnEnemies()
        {
            int enemiesinroom = 0;
            int sameinroom = 0;
            foreach(Room r in game.rooms)
            {
                foreach (int cs in r.spawnindex)
                {
                    enemiesinroom = r.creatures.Count(p => p.templateindex == cs);
                    sameinroom = r.spawnindex.Count(sm => sm == cs);
                    /*foreach (Creature ctr in r.creatures)
                    {
                        if(ctr.name == game.creatures[cs].name)
                        {
                            enemiesinroom++;
                        }
                    }
                    
                    foreach (int csim in r.spawnindex)
                    {
                        if(cs == csim)
                        {
                            sameinroom++;
                        }
                    }
                    */
                    if(sameinroom > enemiesinroom)
                    {
                        r.creatures.Add((Creature)game.creatures[cs].Clone());
                    }
                }
            }
        }
        public void Explore()
        {
            bool finished = false;
            string choice;
            bool picked = false;
            character.DisplayStats();

            while (!finished)
            {
                DisplayRoom();
                Console.Write("\nWhat do you want to do?");
                choice = Console.ReadLine();
                picked = false;
                if (Utility.NotBlank(choice))
                {

                    if (Utility.WordMatch(choice, "north", picked))
                    {
                        nextroom = game.rooms[currentroom].northexit;
                        if (nextroom == -1)
                        {
                            Console.Write("\nYou cannot go that way.\n");
                            System.Threading.Thread.Sleep(600);
                        }
                        else
                        {
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
                            Console.Write("\nYou cannot go that way.\n");
                            System.Threading.Thread.Sleep(600);
                        }
                        else
                        {
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
                            Console.Write("\nYou cannot go that way.\n");
                            System.Threading.Thread.Sleep(600);
                        }
                        else
                        {
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
                            Console.Write("\nYou cannot go that way.\n");
                            System.Threading.Thread.Sleep(600);
                        }
                        else
                        {
                            currentroom = nextroom;
                            Console.WriteLine("OK\n");
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "stay", picked))
                    {
                        bool foundkeeper = false;
                        int innprice = 0;
                        foreach(Creature c in game.rooms[currentroom].creatures)
                        {
                            foreach(CreatureAttribute a in c.cattributes)
                            {
                                if(a.atname == "Inn")
                                {
                                    foundkeeper = true;
                                    innprice = a.atvalue;
                                    break;
                                }
                            }
                        }
                        if(foundkeeper)
                        {
                            if (character.gold >= innprice)
                            {
                                Console.Write("\nWill you stay at the Inn for " + innprice + " gold? ");
                                choice = Console.ReadLine();
                                if (Utility.WordMatch(choice, "yes", picked))
                                {
                                    character.gold -= innprice;
                                    character.hp = character.maxhp;
                                    character.mp = character.maxmp;
                                    Console.WriteLine("You have a hearty meal and a good nights sleep.\nYou awaken invigorated and ready to win the day!");
                                    character.DisplayStats();
                                    RespawnEnemies();
                                }
                                else
                                {
                                    Console.WriteLine("Away with you then!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("You do not have enough gold to stay at this Inn");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no Innkeeper here.");
                        }
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "inventory", picked))
                    {
                        character.displayinv();
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "save", picked))
                    {
                        
                        saver.SaveData(character, character.name + ".character");
                        Console.Write("\nData Saved.\n");
                        character.DisplayStats();
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "equip", picked) || Utility.WordMatch(choice, "wear", picked))
                    {
                        foreach (Item item in character.inventory)
                        {
                            if (Utility.KeyWord(choice, item.name))
                            {
                                character.equip(item);
                            }
                        }
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "inspect", picked) || Utility.WordMatch(choice, "look", picked))
                    {
                        foreach (Item item in character.inventory)
                        {
                            if (Utility.KeyWord(choice, item.name))
                            {
                                item.FullDetails();
                            }
                        }
                        picked = true;
                    }

                    if (Utility.WordMatch(choice, "kill", picked) || Utility.WordMatch(choice, "fight", picked))
                    {
                        //check every creature in room and find the first one whos name matches
                        foreach (Creature target in game.rooms[currentroom].creatures)
                        {
                            if (Utility.KeyWord(choice, target.name))
                            {
                                //found a creature with this name


                                List<Creature> opponents = new List<Creature>();
                                opponents.Add(target);
                                //check each creature in room, if it is in the same faction it will assist
                                foreach (Creature assistant in game.rooms[currentroom].creatures)
                                {
                                    if (target.Equals(assistant))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Utility.Colorize("You attack #c" + target.name);
                                        System.Threading.Thread.Sleep(50);
                                    }
                                    else
                                    {
                                        if (target.faction == assistant.faction)
                                        {
                                            Utility.Colorize("#c" + assistant.name + "#w moves to assist #c" + target.name);
                                            opponents.Add(assistant);
                                            System.Threading.Thread.Sleep(50);
                                        }
                                        else
                                        {

                                            Utility.Colorize("#c" + assistant.name + "#w doesn't give a shit.");
                                            System.Threading.Thread.Sleep(50);
                                        }
                                    }
                                }
                                Combat mycombat = new Combat(character, opponents);
                                if (mycombat.Battle())
                                {
                                    int expgain;
                                    int goldgain;


                                    foreach (Creature o in opponents)
                                    {

                                        expgain = rng.Next(1, o.exp);
                                        goldgain = rng.Next(1, o.gold);
                                        Console.WriteLine("Gained " + expgain + " experience and " + goldgain + " gold from defeating " + o.name);
                                        character.exp += expgain;
                                        character.gold += goldgain;
                                        if (game.rooms[currentroom].creatures.Contains(o))
                                        {
                                            game.rooms[currentroom].creatures.Remove(o);
                                        }


                                    }
                                    opponents.Clear();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                }
                            }
                        }
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "stats", picked) || Utility.WordMatch(choice, "score", picked))
                    {
                        Console.WriteLine("Display stats");
                        character.DisplayStats();
                        picked = true;
                    }
                    if (Utility.WordMatch(choice, "done", picked) || Utility.WordMatch(choice, "exit", picked))
                    {

                        Console.Write("\nBe done with you!.\n");
                        picked = true;
                        finished = true;
                    }
                    if (Utility.WordMatch(choice, "help", picked))
                    {
                        Console.WriteLine("kill / fight = attack a creature");
                        Console.WriteLine("stats = see your current stats");
                        Console.WriteLine("look / inspect = take a closer look at something");
                        Console.WriteLine("equip / wear = wear/wield a piece of equipment");
                        Console.WriteLine("save = save your character");
                        Console.WriteLine("inventory = check your inventory");
                        Console.WriteLine("north/south/east/west = travel");
                        picked = true;
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
        }
        public bool LoadRoomFile()
        {
            bool success = false;
            bool finished = false;
            bool picked = false;
            string gamefilename;     
            SaveMaker saver = new SaveMaker();
            while (!finished)
            {
                Console.Write("\nWhat Game File?");

                gamefilename = Console.ReadLine();
                if (Utility.NotBlank(gamefilename))
                {
                    gamefilename += ".game";
                    if (File.Exists(gamefilename))
                    {
                        game = saver.LoadGameData(game, gamefilename);
                        Console.Write("\nGame file loaded!\n");
                        success = true;
                        finished = true;
                    }
                    else
                    {
                        while (!picked)
                        {
                            Console.Write("\nFile does not exist.  Try again?");
                            choice = Console.ReadLine();
                            if (Utility.WordMatch(choice, "yes", picked))
                            {
                                picked = true;
                                success = false;
                                finished = false;
                            }
                            if (Utility.WordMatch(choice, "no", picked))
                            {
                                picked = true;
                                success = false;
                                finished = true;
                            }
                        }
                    }
                }
                else
                {
                    while (!picked)
                    {
                        Console.Write("\nNo name entered.  Try again?");
                        choice = Console.ReadLine();
                        if (Utility.WordMatch(choice, "yes", picked))
                        {
                            picked = true;
                            success = false;
                            finished = false;
                        }
                        if (Utility.WordMatch(choice, "no", picked))
                        {
                            picked = true;
                            success = false;
                            finished = true;
                        }
                    }
                }
            }
            return success;
        }

        public void DisplayRoom()
        {
            Console.WriteLine("\nRoom Name:" + game.rooms[currentroom].name);
            //Console.WriteLine("Room Description:\n" + rooma[currentroom].description);
            Utility.Colorize("Room Description:\n" + game.rooms[currentroom].description);

            Console.ForegroundColor = ConsoleColor.Cyan;
            game.rooms[currentroom].creatures.ForEach(Displayc);
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine("Exits:" + game.rooms[currentroom].roomexits());
        }

        private static void Displayc(Creature c)
        {
            Console.WriteLine(c.description);
        }





    }
}
