using Applications.VirtualScreen;

namespace Applications.UI
{
    public class EnableVirtualScreen
    {
        private static string label = "OFF";

        public static string Label
        {
            /*
            set
            {
                label = value;
            }
            */
            get
            {
                if (VirtualScreenInfo.MonitorBoardIsActive)
                {
                    label = "ON";
                }
                else
                {
                    label = "OFF";
                }

                return label;
            }
        }

        public static void SwitchVirtualScreenState()
        {
            if (!(VirtualScreenInfo.MonitorBoardIsActive))
            {
                VirtualScreenInfo.MonitorBoardIsActive = true;
                //label = "ON";
                return;
            }
            else
            {
                VirtualScreenInfo.MonitorBoardIsActive = false;
                //label = "OFF";
                return;
            }
        }
    }
}