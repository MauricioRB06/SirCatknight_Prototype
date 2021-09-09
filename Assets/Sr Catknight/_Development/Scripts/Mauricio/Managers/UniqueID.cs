using UnityEngine;

namespace _Development.Scripts.Mauricio
{
    public class UniqueID: MonoBehaviour
    {
        public string ID { get; private set; }
        
        private void Awake()
        {
            ID = transform.position.sqrMagnitude + '-' + name + '-' + transform.GetSiblingIndex();
            Debug.Log("ID for " + name + "is " + ID);
        }
    }
}
