/*
 * SaveGameState -
 * Created by : Allan N. Murillo
 * Last Edited : 3/4/2020
 */

using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace ANM
{
    public class SaveGameState : MonoBehaviour
    {
        public static void Save<T>(T objectToSave, string key)
        {
            var path = Application.persistentDataPath + "/saves/";
            var formatter = new BinaryFormatter();
            Directory.CreateDirectory(path);
            using (var fileStream = new FileStream(path + key + ".txt", FileMode.Create))
            {
                formatter.Serialize(fileStream, objectToSave);
            }
        }
        
        public static T Load<T>(string key)
        {
            var path = Application.persistentDataPath + "/saves/";
            var formatter = new BinaryFormatter();
            T loadedObj;
            
            using (var fileStream = new FileStream(path + key + ".txt", FileMode.Open))
            { loadedObj = (T) formatter.Deserialize(fileStream); }
            
            return loadedObj;
        }

        public static bool SaveExists(string key)
        {
            var path = Application.persistentDataPath + "/saves/" + key + ".txt";
            return File.Exists(path);
        }

        public static void DeleteAllSaves()
        {
            var path = Application.persistentDataPath + "/saves/";
            var directory = new DirectoryInfo(path);
            directory.Delete(true);
            Directory.CreateDirectory(path);
        }
    }
}
