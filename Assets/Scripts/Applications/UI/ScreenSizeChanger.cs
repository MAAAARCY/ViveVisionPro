using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Applications.VirtualScreen;

namespace Applications.UI
{
    public class ScreenSizeChanger
    {
        private static float sliderValue = 1.0f;

        public static float SliderValue
        {
            /*
            set
            {
                sliderValue = value;
            }
            */
            get
            {
                return sliderValue;
            }
        }

        public static void SizeChanger(float delta)
        {
            Debug.Log("MoveHandle: " + delta);
            sliderValue += delta * 10;

            if (sliderValue < 0.5f) sliderValue = 0.5f;

            VirtualScreenInfo.VirtualScreenScale = new Vector3(sliderValue, sliderValue * 0.5f, 1.0f);
        }
    }

}