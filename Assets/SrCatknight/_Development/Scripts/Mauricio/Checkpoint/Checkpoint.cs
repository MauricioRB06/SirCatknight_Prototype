using System.Collections;
using Player;
using SrCatknight.Scripts.Player;
using UnityEngine;

/* The purpose of this Script is:

  Insert Here

  Documentation and References:

  IEnumerator: https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=net-5.0
  Coroutines: https://docs.unity3d.com/Manual/Coroutines.html
  Coroutines in Unity: https://www.youtube.com/watch?v=OGyp3jAmpnw [ Spanish ]

 */

namespace _Development.Scripts.Mauricio.Levels
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private GameObject reachedCheckpoint;
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
            StartCoroutine(ShowUIText(3f, false));
        }
    }
}
