
using System.Collections;
using SrCatknight.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SrCatknight.Scripts.Levels.General
{
    public class MessageTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject textBox;
        [SerializeField]  private GameObject text;
        [SerializeField] private GameObject objectToReveal;
        [Space(15)]
        
        private string _inputDevice;
        
        // 
        private void Awake()
        {
            
            StartCoroutine(SpritesFadeOut(transform.GetComponent<SpriteRenderer>(),
                transform.GetComponent<SpriteRenderer>().color));
            
            if (textBox != null)
            {
                textBox.SetActive(false);
                
            }
            else
            {
                Debug.LogError($"{gameObject.name} dont have a textBox GameObject, please add once");
            }
            
            if (text != null)
            {
                text.SetActive(false);
            }
            else
            {
                 Debug.LogError($"{gameObject.name} dont have a tex GameObject, please add once");
            }
            
            if (objectToReveal != null)
            {
                objectToReveal.SetActive(false);
            }
        }
        
        // 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            _inputDevice = collision.GetComponent<PlayerController>().InputHandler.CheckInputDevice();
            
            if (textBox != null)
            {
                textBox.SetActive(true);
            }
            
            if (text != null)
            {
                text.SetActive(true);
            }
            
            if (objectToReveal != null)
            {
                objectToReveal.SetActive(true);
                
                StartCoroutine(SpritesFadeIn(objectToReveal.GetComponent<SpriteRenderer>(),
                    objectToReveal.GetComponent<SpriteRenderer>().color));
                
                objectToReveal = null;
            }
            
            StartCoroutine(SpritesFadeIn(transform.GetComponent<SpriteRenderer>(),
                transform.GetComponent<SpriteRenderer>().color));
            
            if (textBox != null)
            {
                StartCoroutine(SpritesFadeIn(textBox.GetComponent<SpriteRenderer>(),
                    textBox.GetComponent<SpriteRenderer>().color));
            }
            
            if (text != null)
            {
                StartCoroutine(TextFadeIn(text.GetComponent<TMP_Text>(),
                    text.GetComponent<TMP_Text>().color));
            }
        }
        
        // 
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            StartCoroutine(SpritesFadeOut(transform.GetComponent<SpriteRenderer>(),
                transform.GetComponent<SpriteRenderer>().color));
            
            if (textBox != null)
            {
                StartCoroutine(SpritesFadeOut(textBox.GetComponent<SpriteRenderer>(),
                    textBox.GetComponent<SpriteRenderer>().color));
            }
            
            if (text != null)
            {
                StartCoroutine(TextFadeOut(text.GetComponent<TMP_Text>(),
                    text.GetComponent<TMP_Text>().color));
            }

            StartCoroutine(TextChangeDisable());
        }
        
        public string CheckInputDevice()
        {
            if (_inputDevice == null)
            {
                _inputDevice = "Keyboard";
                return _inputDevice;
            }
            else
            {
                return _inputDevice;
            }
        }

        private IEnumerator TextChangeDisable()
        {
            yield return new WaitForSeconds(1);
            text.SetActive(false);
            textBox.SetActive(false);
        }

        // Coroutine that makes the sprites of the damage zone appear smoothly.
        private static IEnumerator SpritesFadeIn(SpriteRenderer childSpriteRenderer, Color childSpriteColor)
        {
            for (var childSpriteAlpha = 0.0f; childSpriteAlpha <= 1.0f; childSpriteAlpha += 0.1f)
            {
                childSpriteColor.a = childSpriteAlpha;
                childSpriteRenderer.color = childSpriteColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            childSpriteColor.a = 1;
            childSpriteRenderer.color = childSpriteColor;
        }
        
        // Coroutine that makes the sprites in the damage area disappear smoothly.
        private static IEnumerator SpritesFadeOut(SpriteRenderer childSpriteRenderer, Color childSpriteColor)
        {
            for (var childSpriteAlpha = 1.0f; childSpriteAlpha >= 0.0f; childSpriteAlpha -= 0.1f)
            {
                childSpriteColor.a = childSpriteAlpha;
                childSpriteRenderer.color = childSpriteColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            childSpriteColor.a = 0;
            childSpriteRenderer.color = childSpriteColor;
        }
        
        // Coroutine that makes the sprites of the damage zone appear smoothly.
        private static IEnumerator TextFadeIn(Graphic childTMProText, Color childTextColor)
        {
            for (var childTextAlpha = 0.0f; childTextAlpha <= 1.0f; childTextAlpha += 0.1f)
            {
                childTextColor.a = childTextAlpha;
                childTMProText.color = childTextColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            childTextColor.a = 1;
            childTMProText.color = childTextColor;
        }
        
        // Coroutine that makes the sprites in the damage area disappear smoothly.
        private static IEnumerator TextFadeOut(Graphic childTMProText, Color childTextColor)
        {
            for (var childTextAlpha = 1.0f; childTextAlpha >= 0.0f; childTextAlpha -= 0.1f)
            {
                childTextColor.a = childTextAlpha;
                childTMProText.color = childTextColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            childTextColor.a = 0;
            childTMProText.color = childTextColor;
        }
    }
}
