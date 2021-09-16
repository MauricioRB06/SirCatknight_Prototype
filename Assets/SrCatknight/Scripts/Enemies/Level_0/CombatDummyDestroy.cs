using System.Collections;
using UnityEngine;

namespace SrCatknight.Scripts.Enemies.Level_0
{
    public class CombatDummyDestroy : MonoBehaviour
    {
        [Range(10.0f, 30.0f)][SerializeField] private float destroyTime = 10.0f;
        private float _startTime;
        
        private IEnumerator DummyFadeOut(SpriteRenderer spriteRenderer, Color spriteColor)
        {
            for (var spriteAlpha = 1.0f; spriteAlpha >= 0.0f; spriteAlpha -= 0.1f)
            {
                spriteColor.a = spriteAlpha;
                spriteRenderer.color = spriteColor;
                yield return new WaitForSeconds(0.1f);
            }
            
            spriteColor.a = 0;
            spriteRenderer.color = spriteColor;
            
            Destroy(gameObject, 0.5f);
        }
        
        private void Awake()
        {
            _startTime = Time.time;
        }

        private void Update()
        {

            if (Time.time < _startTime + destroyTime) return;
            
            StartCoroutine(DummyFadeOut(
                GetComponent<SpriteRenderer>(), GetComponent<SpriteRenderer>().color));
        }
    }
}
