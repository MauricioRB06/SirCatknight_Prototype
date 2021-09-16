
using System;
using System.Collections;
using Player.Input;
using SrCatknight.Scripts.Levels.General;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace SrCatknight._Development.Scripts.Mauricio
{
    public class TextMessageController : MonoBehaviour
    {

        [SerializeField] private GameObject messageLauncher;
        
        [SerializeField] private int[] keyboardButtonARange;
        [SerializeField] private int[] xboxGamepadButtonARange;
        [SerializeField] private int[] dualShock4ButtonARange;
        
        [SerializeField] private int[] keyboardButtonBRange;
        [SerializeField] private int[] xboxGamepadButtonBRange;
        [SerializeField] private int[] dualShock4ButtonBRange;
        
        [SerializeField] private int[] keyboardButtonCRange;
        [SerializeField] private int[] xboxGamepadButtonCRange;
        [SerializeField] private int[] dualShock4ButtonCRange;
        
        [SerializeField] private int[] keyboardButtonDRange;
        [SerializeField] private int[] xboxGamepadButtonDRange;
        [SerializeField] private int[] dualShock4ButtonDRange;
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public string InputDevice { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int ButtonA { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int ButtonB { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int ButtonC { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int ButtonD { get; private set; }
        
        private int[] _currentButtonA;
        private int[] _currentButtonB;
        private int[] _currentButtonC;
        private int[] _currentButtonD;
        
        private LocalizeStringEvent _localizeString;
        
        // 
        private void Awake()
        {
            _localizeString = GetComponent<LocalizeStringEvent>();
        }
        
        // 
        private void OnEnable()
        {
            PlayerInputHandler.InputDeviceDelegate += UpdateString;
            InputDevice = messageLauncher.GetComponent<MessageTrigger>().CheckInputDevice();
            UpdateString(InputDevice);
            //StartCoroutine(AnimateString());
        }
        
        //
        private void OnDisable()
        {
            PlayerInputHandler.InputDeviceDelegate -= UpdateString;
            StopAllCoroutines();
        }
        
        // 
        private void Update()
        {
            foreach (var index in _currentButtonA)
            {
                ButtonA = index;
                _localizeString.RefreshString();
                //yield return new WaitForSeconds(0.5f);
            }

            foreach (var index in _currentButtonB)
            {
                ButtonB = index;
                _localizeString.RefreshString();
                //yield return new WaitForSeconds(0.5f);
            }

            foreach (var index in _currentButtonC)
            {
                ButtonC = index;
                _localizeString.RefreshString();
                //yield return new WaitForSeconds(0.5f);
            }

            foreach (var index in _currentButtonD)
            {
                ButtonD = index;
                _localizeString.RefreshString();
                //yield return new WaitForSeconds(0.5f);
            }
        }

        // 
        private void UpdateString(string inputDevice)
        {
            switch (inputDevice)
            {
                case "Keyboard":
                case "Mouse":
                    InputDevice = "UI Keyboard";
                    _currentButtonA = keyboardButtonARange;
                    _currentButtonB = keyboardButtonBRange;
                    _currentButtonC = keyboardButtonCRange;
                    _currentButtonD = keyboardButtonDRange;
                    break;
                case "Xbox Controller":
                    InputDevice = "UI XboxGamepad";
                    _currentButtonA = xboxGamepadButtonARange;
                    _currentButtonB = xboxGamepadButtonBRange;
                    _currentButtonC = xboxGamepadButtonCRange;
                    _currentButtonD = xboxGamepadButtonDRange;
                    break;
                case "PS4 Controller":
                case "Wireless Controller":
                    InputDevice = "UI DualShock4";
                    _currentButtonA = dualShock4ButtonARange;
                    _currentButtonB = dualShock4ButtonBRange;
                    _currentButtonC = dualShock4ButtonCRange;
                    _currentButtonD = dualShock4ButtonDRange;
                    break;
            }
        }
    }
}
