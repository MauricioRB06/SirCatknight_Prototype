
using UnityEngine;

namespace Levels.Decoration_Objects
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]

    public class DecorationCat : MonoBehaviour
    {
        // 
        private enum CatAnimation
        {
            Idle1,
            Idle2,
            Clean,
            Sleep,
            Interact,
            Jump,
            Grunt
        }
        
        [Header("Cat Animation Selector")] [Space(5)]
        [Tooltip("Select the animation")]
        [SerializeField] private CatAnimation catAnimation;
        
        // 
        private Animator _catAnimator;
        private static readonly int Idle1 = Animator.StringToHash("Idle1");
        private static readonly int Idle2 = Animator.StringToHash("Idle2");
        private static readonly int Clean = Animator.StringToHash("Clean");
        private static readonly int Sleep = Animator.StringToHash("Sleep");
        private static readonly int Interact = Animator.StringToHash("Interact");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Grunt = Animator.StringToHash("Grunt");

        private void Awake()
        {
            _catAnimator = GetComponent<Animator>();
            
            switch (catAnimation.ToString())
            {
                case "Idle1":
                    _catAnimator.SetBool(Idle1, true);
                    break;
                case "Idle2":
                    _catAnimator.SetBool(Idle2, true);
                    break;
                case "Clean":
                    _catAnimator.SetBool(Clean, true);
                    break;
                case "Sleep":
                    _catAnimator.SetBool(Sleep, true);
                    break;
                case "Interact":
                    _catAnimator.SetBool(Interact, true);
                    break;
                case "Jump":
                    _catAnimator.SetBool(Jump, true);
                    break;
                case "Grunt":
                    _catAnimator.SetBool(Grunt, true);
                    break;
            }
        }
    }
}