using UnityEngine;

namespace Applications.FrontCamera
{
    public class FrontCameraInfo
    {
        private static bool frontCameraIsActive = false;

        public static bool FrontCameraIsActive
        {
            set
            {
                frontCameraIsActive = value;
            }
            get
            {
                return frontCameraIsActive;
            }
        }
    }
}