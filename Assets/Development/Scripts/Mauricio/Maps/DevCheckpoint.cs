using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Development.Scripts.Mauricio.Maps
{
    public class DevCheckpoint : MonoBehaviour
    {
        public Text reachedCheckpoint;
        private bool _isActive;

        private IEnumerator ShowUIText(float time, bool setText)
        {
            yield return new WaitForSecondsRealtime(time);
            reachedCheckpoint.gameObject.SetActive(setText);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            if (_isActive) return;

            _isActive = true;
            var checkpointPosition = transform.position;
            
            PlayerRespawn.ReachedCheckpoint(checkpointPosition.x, checkpointPosition.y);
            StartCoroutine(ShowUIText(0.1f, true));
            GetComponent<Animator>().enabled = true;
            StartCoroutine(ShowUIText(1f, false));
        }
    }
}