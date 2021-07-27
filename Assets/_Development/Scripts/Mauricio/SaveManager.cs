using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace _Development.Scripts.Mauricio
{
    public class SaveManager : MonoBehaviour
    {
        public static void Save<T>(T objectToSave, string key)
        {
            var path = $@"{Application.persistentDataPath}/save/";
            Directory.CreateDirectory(path);
            var formatter = new BinaryFormatter();

            using var fileStream = new FileStream(path + key + ".txt", FileMode.Create);
            formatter.Serialize(fileStream, objectToSave);
        }
        
        public static T Load<T>(string key)
        {
            var path = $@"{Application.persistentDataPath}/save/";
            var formatter = new BinaryFormatter();
            var returnValue = default(T);
            using var fileStream = new FileStream(path + key + ".txt", FileMode.Open);
            returnValue = (T)formatter.Deserialize(fileStream);

            return returnValue;
        }

        public static bool SaveExists(string key)
        {
            var path = $@"{Application.persistentDataPath}/save/" + key + ".txt";
            return File.Exists(path);
        }

        public static void DeleteAllSaveFiles()
        {
            var path = $@"{Application.persistentDataPath}/save/";
            var directory = new DirectoryInfo(path);
            directory.Delete(true);
            Directory.CreateDirectory(path);
        }
    }
}
