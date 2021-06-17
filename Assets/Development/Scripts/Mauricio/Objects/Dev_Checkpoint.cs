using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dev_Checkpoint : MonoBehaviour
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
                collision.GetComponent<Player>().ReachedCheckpoint(transform.position.x, transform.position.y);
                ReachedCheckpoint.gameObject.SetActive(true);
                GetComponent<Animator>().enabled = true;
                Destroy(ReachedCheckpoint, 1f);
            }
            
        }
    }
}
