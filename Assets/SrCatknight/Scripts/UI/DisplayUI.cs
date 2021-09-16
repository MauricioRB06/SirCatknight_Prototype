using TMPro;
using UnityEngine;

namespace SrCatknight.Scripts.UI
{
    public class DisplayUI : MonoBehaviour
    {
        public TextMeshProUGUI myText;
        public float fadeTime = 10f;
        public bool displayInfo;
        
        // 
        private void Start()
        {
            //myText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
            myText.color = Color.clear;
        }
        
        // 
        private void Update()
        {
            FadeText();
        }

        private void OnMouseOver()
        {
            displayInfo = true;
        }
        
        private void OnMouseExit()
        {
            displayInfo = false;
        }

        private void FadeText()
        {
            if (displayInfo)
            {
                myText.color = Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);
            }
            else
            {
                myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
            }
        }
    }
}
