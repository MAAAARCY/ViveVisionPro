using Applications.FrontCamera;

namespace Applications.UI
{
    public class EnableFrontCamera
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
                if (FrontCameraInfo.FrontCameraIsActive)
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

        public static void SwitchFrontCameraState()
        {
            if (!(FrontCameraInfo.FrontCameraIsActive))
            {
                FrontCameraInfo.FrontCameraIsActive = true;
                return;
            }
            else
            {
                FrontCameraInfo.FrontCameraIsActive = false;
                return;
            }
        }
    }
}