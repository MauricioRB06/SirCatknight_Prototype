using UnityEngine;

namespace _Development.Scripts.Mauricio.Boss_Test
{
    public class BossUI : MonoBehaviour
    {
        public GameObject bossPanel;
        public GameObject finalBoss;

        public static BossUI Instance;
        private bool battle;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;    
            }
        }
        
        public void Start()
        {
            bossPanel.SetActive(false);
            finalBoss.SetActive(false);
        }

        public void BossActivation()
        {
            bossPanel.SetActive(true);
            finalBoss.SetActive(true);
            battle = true;
        }

        public void Update()
        {
            if (!battle) return;

            if (finalBoss.GetComponent<FinalBoss>()._healthBoss <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
