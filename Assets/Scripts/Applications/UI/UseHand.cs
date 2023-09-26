using UnityEngine;

using Vive;

namespace Applications.UI
{
    public class UseHand
    {
        private static string label = "Left";
        private static string iconName = "icon 13";

        public static string Label
        {
            get
            {
                if (ViveProInfo.UseLeftHand)
                {
                    label = "Left";
                }
                else
                {
                    label = "Right";
                }

                return label;
            }
        }

        public static string IconName
        {
            get
            {
                return iconName;
            }
        }

        public static void SwitchUseHand()
        {
            if (ViveProInfo.UseLeftHand)
            {
                ViveProInfo.UseLeftHand = false;
                return;
            }
            else
            {
                ViveProInfo.UseLeftHand = true;
                return;
            }
        }
        
        public static void SwitchIconName()
        {
            if (iconName == "icon 13")
            {
                //FontIconSelector.CurrentIconName = "icon 12";
                iconName = "icon 12";
                return;
            }

            if (iconName == "icon 12")
            {
                //FontIconSelector.CurrentIconName = "icon 13";
                iconName = "icon 13";
                return;
            }
        }
    }
}