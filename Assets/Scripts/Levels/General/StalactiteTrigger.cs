
using UnityEngine;
using System.Linq;
namespace Levels.General
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class StalactiteTrigger: MonoBehaviour
    {
        [SerializeField] private GameObject[] stalactites;
        private void Awake()
        {
            if (stalactites.Length == 0)
            {
                Debug.LogError($"<color=#D22323><b>{gameObject.name} does not have any associated" +
                               " stalactite, please add at least one.</b></color>");
            } 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;

            foreach (var stalactite in stalactites.ToList())
            {
                stalactite.GetComponent<Rigidbody2D>().isKinematic = false;
            }

            Destroy(gameObject);
        }
    }
}
