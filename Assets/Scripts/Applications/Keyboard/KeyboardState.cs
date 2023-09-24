using System.Collections.Generic;
using UnityEngine;

namespace Keyboard
{
    public static class KeyboardState
    {
        private static KeyCode pressedKeyCode;
        private static string pressedKeyName = "None";
        private static bool pressedKeyDown = false;

        private static Dictionary<string, Transform> keyTransforms = new Dictionary<string, Transform>();

        private static List<KeyCode> pressedKeyDownList = new List<KeyCode>();

        public static KeyCode PressedKeyCode
        {
            set
            {
                pressedKeyCode = value;
            }
            get
            {
                return pressedKeyCode;
            }
        }

        public static bool PressedKeyDown
        {
            set
            {
                pressedKeyDown = value;
            }
            get
            {
                return pressedKeyDown;
            }
        }

        public static string PressedKeyName
        {
            set
            {
                pressedKeyName = value;
            }
            get
            {
                return pressedKeyName;
            }
        }

        public static List<KeyCode> PressedKeyDownList
        {
            get
            {
                return pressedKeyDownList;
            }
        }

        public static void addPressedKeyDownList(KeyCode code)
        {
            if (code.ToString() == "None" || !(keyTransforms.ContainsKey(code.ToString()))) return;

            pressedKeyDownList.Add(code);
        }

        public static void removePressedKeyDownList(KeyCode code)
        {
            if (code.ToString() == "None" || !(keyTransforms.ContainsKey(code.ToString()))) return;

            pressedKeyDownList.Remove(code);
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

            //Debug.Log(keyName == pressedKeyCode.ToString());
            //if (keyName == pressedKeyCode.ToString() && !(pressedKeyDownQueue.Contains(pressedKeyCode))) pressedKeyDownQueue.Enqueue(pressedKeyCode);
        }

        public static void setKeyUp(string keyName)
        {
            if (keyName == "None" || !(keyTransforms.ContainsKey(keyName))) return;

            Vector3 KeyPosition = keyTransforms[keyName].localPosition;
            keyTransforms[keyName].localPosition = new Vector3(KeyPosition.x, 0.015f, KeyPosition.z);
        }
    }
}