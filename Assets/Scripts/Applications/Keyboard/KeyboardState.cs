using System.Collections.Generic;
using UnityEngine;

namespace Keyboard
{
    public static class KeyboardState
    {
        private static KeyCode nowKeyCode;
        private static string nowKeyName = "None";
        private static bool nowKeyDown = false;

        private static Dictionary<string, Transform> keyTransforms = new Dictionary<string, Transform>();

        public static KeyCode NowKeyCode
        {
            set
            {
                nowKeyCode = value;
            }
            get
            {
                return nowKeyCode;
            }
        }

        public static bool NowKeyDown
        {
            set
            {
                nowKeyDown = value;
            }
            get
            {
                return nowKeyDown;
            }
        }

        public static string NowKeyName
        {
            set
            {
                nowKeyName = value;
            }
            get
            {
                return nowKeyName;
            }
        }

        public static void setKeyTransform(string keyName, Transform keyTransform)
        {
            if (!(keyTransforms.ContainsKey(keyName)))
            {
                keyTransforms.Add(keyName, keyTransform);
            }
        }

        public static Transform getKeyTransform(string keyName)
        {
            return keyTransforms[keyName];
        }

        public static void setKeyDown(string keyName)
        {
            if (keyName == "None" || !(keyTransforms.ContainsKey(keyName))) return;

            Vector3 KeyPosition = keyTransforms[keyName].localPosition;
            keyTransforms[keyName].localPosition = new Vector3(KeyPosition.x, 0, KeyPosition.z);
        }

        public static void setKeyUp(string keyName)
        {
            if (keyName == "None" || !(keyTransforms.ContainsKey(keyName))) return;

            Vector3 KeyPosition = keyTransforms[keyName].localPosition;
            keyTransforms[keyName].localPosition = new Vector3(KeyPosition.x, 0.015f, KeyPosition.z);
        }
    }
}