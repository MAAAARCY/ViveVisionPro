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
                        KeyboardState.NowKeyCode = code;
                        KeyboardState.NowKeyName = code.ToString();
                        break;
                    }
                }
            }

            if (Input.GetKey(KeyboardState.NowKeyCode))
            {
                Debug.Log(KeyboardState.NowKeyCode);
                KeyboardState.setKeyDown(KeyboardState.NowKeyName);
                KeyboardState.NowKeyDown = true;
            }
            else
            {
                KeyboardState.setKeyUp(KeyboardState.NowKeyName);
                KeyboardState.NowKeyDown = false;
            }
        }
    }
}