using System;
using System.IO;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Managers
{
    public class SaveManager : MonoBehaviour
    {
        private string saveFileExtension = ".perseo";
        public string saveName = "SaveData_";
        [Range(0, 10)] public int saveDataIndex = 0;

        public void SaveData(string dataToSave)
        {
            if (WriteToFile(saveName+saveDataIndex, dataToSave))
            {
                Debug.Log("Successfully save data");
            }
        }
        
        public string LoadData()
        {
            var data = "";
            if (ReadFromFile(saveName+saveDataIndex, out data))
            {
                Debug.Log("Successfully loaded data");
            }
            return data;
        }
        
        private bool WriteToFile(string name, string content)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, name);
            
            try
            {
                File.WriteAllText(fullPath, content);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error Saving to a file {e.Message}");
            }
            return false;
        }
        
        private bool ReadFromFile(string name, out string content)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, name);
            
            try
            {
                content = File.ReadAllText(fullPath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error When loading the file {e.Message}");
                content = "";
            }
            return false;
        }
    }
}
