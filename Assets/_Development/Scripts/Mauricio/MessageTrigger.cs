
using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Development.Scripts.Mauricio
{
    public class MessageTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject textBox;
        [SerializeField] private GameObject spritesKeyboard;
        [SerializeField] private GameObject spritesXboxGamepad;
        [SerializeField] private GameObject spritesDualShock4;
        [SerializeField] private GameObject text;
        [SerializeField] private GameObject objectToReveal;

        private GameObject _inputSelector;
        
        // 
        private void Awake()
        {
            
            StartCoroutine(SpritesFadeOut(transform.GetComponent<SpriteRenderer>(),
                transform.GetComponent<SpriteRenderer>().color));
            
            if (textBox != null)
            {
                textBox.SetActive(false);
            }
            if (spritesKeyboard != null)
            {
                _inputSelector = spritesKeyboard;
                spritesKeyboard.SetActive(false);
            }
            if (spritesXboxGamepad != null)
            {
                spritesXboxGamepad.SetActive(false);
            }
            if (spritesDualShock4 != null)
            {
                spritesDualShock4.SetActive(false);
            }
            if (text != null)
            {
                text.SetActive(false);
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
            
            if (textBox != null)
            {
                textBox.SetActive(true);
            }
            if (_inputSelector != null)
            {
                _inputSelector.SetActive(true);
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
                foreach (Transform child in text.transform)
                {
                    var childSpriteRenderer = child.GetComponent<TMP_Text>();
                    var childSpriteColor = childSpriteRenderer.color;
                    StartCoroutine(TextFadeIn(childSpriteRenderer, childSpriteColor));
                }
            }
            
            if (_inputSelector != null)
            {
                foreach (Transform child in _inputSelector.transform)
                {
                    var childSpriteRenderer = child.GetComponent<SpriteRenderer>();
                    var childSpriteColor = childSpriteRenderer.color;
                    StartCoroutine(SpritesFadeIn(childSpriteRenderer, childSpriteColor));
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;

            if (collision.GetComponent<PlayerController>().InputHandler.Device == "Mouse" ||
                collision.GetComponent<PlayerController>().InputHandler.Device == "Keyboard")
            {
                if (_inputSelector != null)
                {
                    _inputSelector = spritesKeyboard;
                    
                    spritesKeyboard.SetActive(true);
                    spritesXboxGamepad.SetActive(false);
                    spritesDualShock4.SetActive(false);
                }
            }
            else if (collision.GetComponent<PlayerController>().InputHandler.Device == "Xbox Controller")
            {
                if (_inputSelector != null)
                {
                    _inputSelector = spritesXboxGamepad;
                
                    spritesXboxGamepad.SetActive(true);
                    spritesKeyboard.SetActive(false);
                    spritesDualShock4.SetActive(false); 
                }
            }
            else if (collision.GetComponent<PlayerController>().InputHandler.Device == "PS4 Controller")
            {
                if (_inputSelector != null)
                {
                    _inputSelector = spritesDualShock4;
                
                    spritesDualShock4.SetActive(true);
                    spritesKeyboard.SetActive(false);
                    spritesXboxGamepad.SetActive(false);
                }
            }

            Debug.Log(collision.GetComponent<PlayerController>().InputHandler.Device);
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
                foreach (Transform child in text.transform)
                {
                    var childSpriteRenderer = child.GetComponent<TMP_Text>();
                    var childSpriteColor = childSpriteRenderer.color;
                    StartCoroutine(TextFadeOut(childSpriteRenderer, childSpriteColor));
                }
                text.SetActive(false);
            }
            
            if (_inputSelector != null)
            {
                foreach (Transform child in _inputSelector.transform)
                {
                    var childSpriteRenderer = child.GetComponent<SpriteRenderer>();
                    var childSpriteColor = childSpriteRenderer.color;
                    StartCoroutine(SpritesFadeOut(childSpriteRenderer, childSpriteColor));
                }
                
                _inputSelector.SetActive(false);
            }
            
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
