using UnityEngine;
using UnityEngine.UI;
/*
namespace Development.Scripts.Mauricio.Objects
{
    public class DevCheckpoint : MonoBehaviour
    {
        public Text ReachedCheckpoint;
        private bool _isActive;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if(_isActive != true)
                {
                    _isActive = true;
                    Player.ReachedCheckpoint(transform.position.x, transform.position.y);
                    ReachedCheckpoint.gameObject.SetActive(true);
                    GetComponent<Animator>().enabled = true;
                    Destroy(ReachedCheckpoint, 1f);
                }
            
            }
        }
    }
}*/
