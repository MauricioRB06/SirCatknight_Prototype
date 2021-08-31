
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Development.Scripts.Mauricio
{
    public class MessageTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject messageTextBox;
        [SerializeField] private GameObject messageSpritesContainer;
        [SerializeField] private GameObject messageTextContainer;
        [SerializeField] private GameObject objectToReveal;
        
        // 
        private void Awake()
        {
            StartCoroutine(SpritesFadeOut(transform.GetComponent<SpriteRenderer>(),
                transform.GetComponent<SpriteRenderer>().color));
            
            if (messageTextBox != null)
            {
                messageTextBox.SetActive(false);
            }
            if (messageSpritesContainer != null)
            {
                messageSpritesContainer.SetActive(false);
            }
            if (messageTextContainer != null)
            {
                messageTextContainer.SetActive(false);
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
            
            if (messageTextBox != null)
            {
                messageTextBox.SetActive(true);
            }
            if (messageSpritesContainer != null)
            {
                messageSpritesContainer.SetActive(true);
            }
            if (messageTextContainer != null)
            {
                messageTextContainer.SetActive(true);
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
            
            if (messageTextBox != null)
            {
                StartCoroutine(SpritesFadeIn(messageTextBox.GetComponent<SpriteRenderer>(),
                    messageTextBox.GetComponent<SpriteRenderer>().color));
            }
            
            if (messageTextContainer != null)
            {
                foreach (Transform child in messageTextContainer.transform)
                {
                    var childSpriteRenderer = child.GetComponent<TMP_Text>();
                    var childSpriteColor = childSpriteRenderer.color;
                    StartCoroutine(TextFadeIn(childSpriteRenderer, childSpriteColor));
                }
            }
            
            if (messageSpritesContainer != null)
            {
                foreach (Transform child in messageSpritesContainer.transform)
                {
                    var childSpriteRenderer = child.GetComponent<SpriteRenderer>();
                    var childSpriteColor = childSpriteRenderer.color;
                    StartCoroutine(SpritesFadeIn(childSpriteRenderer, childSpriteColor));
                }
            }
        }
        
        // 
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            StartCoroutine(SpritesFadeOut(transform.GetComponent<SpriteRenderer>(),
                transform.GetComponent<SpriteRenderer>().color));
            
            if (messageTextBox != null)
            {
                StartCoroutine(SpritesFadeOut(messageTextBox.GetComponent<SpriteRenderer>(),
                    messageTextBox.GetComponent<SpriteRenderer>().color));
            }
            
            if (messageTextContainer != null)
            {
                foreach (Transform child in messageTextContainer.transform)
                {
                    var childSpriteRenderer = child.GetComponent<TMP_Text>();
                    var childSpriteColor = childSpriteRenderer.color;
                    StartCoroutine(TextFadeOut(childSpriteRenderer, childSpriteColor));
                }
                messageTextContainer.SetActive(false);
            }
            
            if (messageSpritesContainer != null)
            {
                foreach (Transform child in messageSpritesContainer.transform)
                {
                    var childSpriteRenderer = child.GetComponent<SpriteRenderer>();
                    var childSpriteColor = childSpriteRenderer.color;
                    StartCoroutine(SpritesFadeOut(childSpriteRenderer, childSpriteColor));
                }
                
                messageSpritesContainer.SetActive(false);
            }
            
            messageTextBox.SetActive(false);
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
