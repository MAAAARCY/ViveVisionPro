using System.Collections.Generic;
using UnityEngine;
using System;

namespace Keyboard
{
    public class KeyboardObserver : MonoBehaviour
    {
        [SerializeField]
        private Transform KeyboardBaseTransform;

        [SerializeField]
        private Transform DebugCylinderTransform;

        private void Start()
        {
            int keyCount = KeyboardBaseTransform.childCount;
            
            if (keyCount == 0) return;

            for (int n = 0; n < keyCount; n++)
            {
                GameObject child = KeyboardBaseTransform.GetChild(n).gameObject;

                KeyboardState.setKeyTransform(child.name, child.transform);
            }
        }

        private void Update()      
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(code))
                    {
                        KeyboardState.PressedKeyCode = code;
                        KeyboardState.PressedKeyName = code.ToString();
                        KeyboardState.addPressedKeyDownList(code);
                        break;
                    }
                }
            }

            if (Input.GetKey(KeyboardState.PressedKeyCode))
            {
                //Debug.Log(KeyboardState.PressedKeyCode);
                KeyboardState.setKeyDown(KeyboardState.PressedKeyName);
                KeyboardState.PressedKeyDown = true;
            }

            for(int i = 0; i < KeyboardState.PressedKeyDownList.Count; i++)
            {
                if (!(Input.GetKey(KeyboardState.PressedKeyDownList[i])))
                {
                    KeyboardState.setKeyUp(KeyboardState.PressedKeyDownList[i].ToString());
                    KeyboardState.removePressedKeyDownList(KeyboardState.PressedKeyDownList[i]);

                    KeyboardState.PressedKeyDown = false;
                }
            }
            
            //Debug.Log(KeyboardState.PressedKeyDownList.Count);
        }
    }
}