using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Garlos
{
    public class SaveMaker
    {
        public void SaveData(object obj, string filename)
        {
            File.Delete(filename);
            XmlSerializer serialz = new XmlSerializer(obj.GetType());
            TextWriter writerz = new StreamWriter(filename);
            serialz.Serialize(writerz, obj);
            writerz.Close();

        }
        public Character LoadData(Character obj, string filename)
        {
            if(File.Exists(filename))
            {
                XmlSerializer serialz = new XmlSerializer(obj.GetType());
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                obj = (Character)serialz.Deserialize(stream);
                stream.Close();
                return obj;
            }
            else
            {
                obj = new Character();
                return obj;

            }
            
      
        }

        public void SaveCreatures(List<Creature> obj, string filename)
        {
            File.Delete(filename);
            XmlSerializer serialz = new XmlSerializer(obj.GetType());
            TextWriter writerz = new StreamWriter(filename);
            serialz.Serialize(writerz, obj);
            writerz.Close();
        }

        public List<Creature> LoadCreatures(List<Creature> obj, string filename)
        {
            XmlSerializer serialz = new XmlSerializer(obj.GetType());
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            obj = (List<Creature>)serialz.Deserialize(stream);
            stream.Close();
            return obj;
        }
        public void SaveGameData(GameData obj, string filename)
        {
            File.Delete(filename);
            XmlSerializer serialz = new XmlSerializer(obj.GetType());
            TextWriter writerz = new StreamWriter(filename);
            serialz.Serialize(writerz, obj);
            writerz.Close();
        }
        public void SaveRooms(List<Room> obj, string filename)
        {
            File.Delete(filename);
            XmlSerializer serialz = new XmlSerializer(obj.GetType());
            TextWriter writerz = new StreamWriter(filename);
            serialz.Serialize(writerz, obj);
            writerz.Close();   
        }

        public GameData LoadGameData(GameData obj, string filename)
        {
            XmlSerializer serialz = new XmlSerializer(obj.GetType());
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            obj = (GameData)serialz.Deserialize(stream);
            stream.Close();
            return obj;
        }
        public List<Room> LoadRooms(List<Room> obj, string filename)
        {
            //if (File.Exists(filename))
            // {
            
            XmlSerializer serialz = new XmlSerializer(obj.GetType());
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            obj = (List<Room>)serialz.Deserialize(stream);
            stream.Close();
            return obj;
         /*  }
            else
            {
                obj = new Room[100];
                obj[0] = new Room();
                return obj;

            }*/

        }
    }
}
