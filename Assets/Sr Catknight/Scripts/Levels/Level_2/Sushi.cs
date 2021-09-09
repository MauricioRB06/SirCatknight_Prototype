using Interfaces;
using UnityEngine;

namespace Levels.Level_2
{
    // 
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class Sushi : MonoBehaviour, IDamageableObject

    {
        [SerializeField] private bool damaged;
        [SerializeField] private GameObject sushiParticle;

        // Start is called before the first frame update
        private Rigidbody2D _sushiRigidbody2D;
        
        // 
        private void Awake()
        {
            _sushiRigidbody2D = GetComponent<Rigidbody2D>(); 
        }

        // Update is called once per frame
        private void Update()
        {
            if (damaged)
            {
                TakeDamage(0);
            }
        }
        
        // 
        public void TakeDamage(float damageAmount)
        {
            Instantiate(sushiParticle, transform.position, Quaternion.identity);
            Destroy(gameObject, 0f);
        }
    }
}
