using System.Collections.Generic;
using UnityEngine;

namespace _Development.Scripts.Mauricio
{
    public class CollectableItemSet: MonoBehaviour
    {
        public HashSet<string> CollectedItems { get; private set; } = new HashSet<string>();
        
        private void Awake()
        {
            //GameManager.SaveInitiated += Save;
            Load();
        }

        private void Save()
        {
            //SaveManager.Save(CollectedItems, "CollectedItems");
        }

        private void Load()
        {
           // if (SaveManager.SaveExists("CollectedItems"))
           // {
            //    CollectedItems = SaveManager.Load<HashSet<string>>("CollectedItems");
            //}
        }
    }
}
