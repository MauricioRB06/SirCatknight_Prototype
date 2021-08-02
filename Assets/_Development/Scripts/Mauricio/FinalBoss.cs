using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour, IDamageableObject
{
   public Image healthBossBar;
   public float _healthBoss = 100.0f;
   public GameObject bossDeathSound;
   public GameObject bossPortal;
   
   private void Update()
   {
      healthBossBar.fillAmount = _healthBoss / 100;

      if (_healthBoss <= 0)
      {
         //Instantiate(bossDeathSound, transform.position, Quaternion.identity);
         Destroy(gameObject,0.2f);
         bossPortal.gameObject.SetActive(true);
      }
   }

   public void Damage(float damage)
   {
      Debug.Log(damage + "Damage Taken");
      _healthBoss -= damage;
   }
}
