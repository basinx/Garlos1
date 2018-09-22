using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace Garlos
{
    class Program
    {

        static void Main(string[] args)
        {
            SaveMaker saver = new SaveMaker();
            string choice = "";
            string option = "";
            Adventure adventure = new Adventure();
            bool picked = false;
            
            int strlen;
            Character character = new Character();
            
            while (!Utility.WordMatch(choice, "exit", picked))
            {
                Console.Write("NEW - create a new character\nVIEW - view current character\nLOAD - load previous character\nROOM - enter the room editor\nEXIT - exit game\nCREATURE - enter the creature editor\nBEGIN - begin adventure\n\nWelcome to Garlos.  What will you do?");
                choice = Console.ReadLine();
                strlen = choice.Length;

                if (Utility.WordMatch(choice, "view", picked))
                {
                    character.DisplayStats();
                    picked = true;
                }
                if (Utility.WordMatch(choice, "new", picked))
                {
                    Console.WriteLine("You will begin a new character!");
                    if(Utility.KeyWord(choice, "game"))
                    {
                        Console.WriteLine("game chosen??");
                        Console.ReadLine();
                    }
                    character.NewCharacter();
                    
                    picked = true;
                        
                }
                if (Utility.WordMatch(choice, "load", picked))
                {
                    if (Utility.NotBlank(Utility.GetKeyWord(choice)))
                    {
                        option = Utility.GetKeyWord(choice);

                    }
                    else
                    {
                        Console.Write("\nLoad which character?");
                        option = Console.ReadLine();
                        
                    }
                    option += ".character";
                    if (Utility.NotBlank(option))
                    {
                        if (File.Exists(option))
                        {
                            try
                            {
                                Console.WriteLine("Your character will be loaded!");
                                character = saver.LoadData(character, option);
                                character.DisplayStats();
                            }
                            catch
                            {
                                Console.WriteLine("\nNot a valid character file!\n");
                            }

                        }
                        else
                        {
                            Console.WriteLine("\nCharacter does not exit.\n");
                        }
                    }
                    picked = true;
                        
                }
                if (Utility.WordMatch(choice, "room", picked))
                {
                    Console.WriteLine("OK lets edit rooms!");
                    RoomEditor roomy = new RoomEditor();
                    Console.WriteLine("Welcome to Edit Mode.\n");                
                    roomy.LoadFile();
                    roomy.MainMenu();
                    picked = true;
                }
                if (Utility.WordMatch(choice, "creature", picked))
                {
                    Console.WriteLine("OK lets edit creatures!");
                    CreatureEditor creaturey = new CreatureEditor();
                    Console.WriteLine("Welcome to Edit Mode.\n");
                    creaturey.LaunchEditor();
                    creaturey.MainMenu();
                    picked = true;
                }
                if (Utility.WordMatch(choice, "begin", picked))
                {
                    Console.WriteLine("Time to begin the adventure!");
                    if(adventure.LoadRoomFile())
                    {
                        Console.WriteLine("GET READY!!");
                        adventure.BeginAdventure(character);
                        adventure.Explore();
                    }
                    picked = true;

                }

                picked = false;
                
            }
            
  
        }
    }


}
