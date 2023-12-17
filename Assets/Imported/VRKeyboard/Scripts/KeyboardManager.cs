/***
 * Author: Yunhan Li
 * Any issue please contact yunhn.lee@gmail.com
 ***/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace VRKeyboard.Utils
{
    public class KeyboardManager : MonoBehaviour
    {
        public Action<string> newValue;

        #region Public Variables
        [Header("User defined")]
        [Tooltip("If the character is uppercase at the initialization")]
        public bool isUppercase = false;
        public int maxInputLength;

        private string inputText = "";

        [Header("Essentials")]
        public Transform keys;
        #endregion

        #region Private Variables
        private string Input
        {
            get { return inputText; }
            set { inputText = value; }
        }
        private Key[] keyList;
        private bool capslockFlag;
        #endregion

        #region Monobehaviour Callbacks
        void Awake()
        {
            keyList = keys.GetComponentsInChildren<Key>();
        }

        void Start()
        {
            foreach (var key in keyList)
            {
                key.OnKeyClicked += GenerateInput;
            }
            capslockFlag = isUppercase;
            CapsLock();
        }
        #endregion

        #region Public Methods
        public void Backspace()
        {
            if (Input.Length > 0)
            {
                Input = Input.Remove(Input.Length - 1);
                newValue?.Invoke(Input);
            }
            else
            {
                return;
            }
        }

        public void Clear()
        {
            Input = "";
            newValue?.Invoke(Input);
        }

        public void CapsLock()
        {
            foreach (var key in keyList)
            {
                if (key is Alphabet)
                {
                    key.CapsLock(capslockFlag);
                }
            }
            capslockFlag = !capslockFlag;
        }

        public void Shift()
        {
            foreach (var key in keyList)
            {
                if (key is Shift)
                {
                    key.ShiftKey();
                }
            }
        }

        public void GenerateInput(string s)
        {
            if (Input.Length > maxInputLength) { return; }
            Input += s;
            newValue?.Invoke(Input);
        }
        #endregion
    }
}