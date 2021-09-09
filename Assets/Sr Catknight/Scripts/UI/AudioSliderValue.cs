
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AudioSliderValue : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI sliderText;

        private void Start()
        {
            slider.onValueChanged.AddListener(text =>
                sliderText.text = text.ToString("0"));
        }
    }
}
