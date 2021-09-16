
using UnityEngine;

namespace SrCatknight.Scripts.Levels.Decoration_Objects
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    
    public class DecorationChicken : MonoBehaviour
    {
        // 
        private enum ChickenType
        {
            ChickenA,
            ChickenB,
            ChickenC,
            ChickenD
        }
        
        // 
        private enum ChickenAnimation
        {
            Idle1,
            Idle2,
            UpDown1,
            UpDown2
        }
        
        [Header("Key Settings")] [Space(5)]
        [Tooltip("Select the type of key")]
        [SerializeField] private ChickenType chickenType;
        
        [Header("Key Settings")] [Space(5)]
        [Tooltip("Select the type of key")]
        [SerializeField] private ChickenAnimation chickenAnimation; 
        
        // 
        private Animator _chickenAnimator;
        
        // 
        private static readonly int ChickenA = Animator.StringToHash("ChickenA");
        private static readonly int ChickenB = Animator.StringToHash("ChickenB");
        private static readonly int ChickenC = Animator.StringToHash("ChickenC");
        private static readonly int ChickenD = Animator.StringToHash("ChickenD");
        
        private static readonly int Idle1 = Animator.StringToHash("Idle1");
        private static readonly int Idle2 = Animator.StringToHash("Idle2");
        private static readonly int UpDown1 = Animator.StringToHash("UpDown1");
        private static readonly int UpDown2 = Animator.StringToHash("UpDown2");

        private void Awake()
        {
            _chickenAnimator = GetComponent<Animator>();
            
            // 
            switch (chickenType.ToString())
            {
                case "ChickenA":
                    _chickenAnimator.SetBool(ChickenA, true);
                    break;
                case "ChickenB":
                    _chickenAnimator.SetBool(ChickenB, true);
                    break;
                case "ChickenC":
                    _chickenAnimator.SetBool(ChickenC, true);
                    break;
                case "ChickenD":
                    _chickenAnimator.SetBool(ChickenD, true);
                    break;
            }
            
            // 
            switch (chickenAnimation.ToString())
            {
                 
                case "Idle1":
                    _chickenAnimator.SetBool(Idle1, true);
                    break;
                case "Idle2":
                    _chickenAnimator.SetBool(Idle2, true);
                    break;
                case "UpDown1":
                    _chickenAnimator.SetBool(UpDown1, true);
                    break;
                case "UpDown2":
                    _chickenAnimator.SetBool(UpDown2, true);
                    break;
            }
        }
    }
}
