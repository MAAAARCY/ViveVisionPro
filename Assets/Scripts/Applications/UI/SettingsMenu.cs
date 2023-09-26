using UnityEngine;

using Applications.VirtualScreen;

namespace Applications.UI
{
    public class SettingsMenu
    {
        private static bool isOpen = false;

        private static string screenFollowsLabel = "OFF";

        private static string screenFollowsIconName = "icon 123";

        public static bool IsOpen
        {
            get
            {
                return isOpen;
            }
        }

        public static string ScreenFollowsLabel
        {
            set
            {
                if (!(value == "ON" || value == "OFF"))
                {
                    screenFollowsLabel = "OFF";
                }
                else
                {
                    screenFollowsLabel = value;
                }
            }
            get
            {
                return screenFollowsLabel;
            }
        }

        public static string ScreenFollowsIconName
        {
            get
            {
                return screenFollowsIconName;
            }
        }

        public static void Menu()
        {
            if (isOpen)
            {
                isOpen = false;
                UIObserver.SwitchActive = true;
                return;
            }
            else
            {
                isOpen = true;
                UIObserver.SwitchActive = true;
                return;
            }
        }

        public static void EnableScreenFollows()
        {
            //if (VirtualScreenInfo.Tracker != VariousVirtualScreenTracker.TrackFollowing)
            if (screenFollowsLabel == "OFF")
            {
                VirtualScreenInfo.Tracker = VariousVirtualScreenTracker.TrackFollowing;
                screenFollowsLabel = "ON";
                screenFollowsIconName = "icon 135";
                return;
            }
            else
            {
                VirtualScreenInfo.Tracker = VariousVirtualScreenTracker.ViveController;
                screenFollowsLabel = "OFF";
                screenFollowsIconName = "icon 123";
                return;
            }
        }
    }
}