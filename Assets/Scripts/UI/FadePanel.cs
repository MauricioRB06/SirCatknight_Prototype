using UnityEngine;

namespace UI
{
    public class FadePanel : MonoBehaviour
    {
        private Animator _fadePanelAnimator;
        
        private void Start()
        {
            _fadePanelAnimator = GetComponent<Animator>();
        }

        public void FadeIn()
        {
            _fadePanelAnimator.Play("FadeIn_Panel");
        }
        
        public void FadeOut()
        {
            _fadePanelAnimator.Play("FadeOut_Panel");
        }
    }
}
