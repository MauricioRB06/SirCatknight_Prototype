using _Development.Scripts.Mauricio.Managers;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Boss_Test
{
    public class BossTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            AudioManager.PlaySound(AudioManager.Instance.BossMusic);
            BossUI.Instance.BossActivation();
            AudioManager.StopAudio(AudioManager.Instance.LevelMusic);
            Destroy(gameObject);
        }
    }
}
