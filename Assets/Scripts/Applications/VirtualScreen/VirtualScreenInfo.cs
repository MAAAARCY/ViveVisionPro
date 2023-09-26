using UnityEngine;

namespace Applications.VirtualScreen
{
    public sealed class VirtualScreenInfo
    {
        private static Vector3 monitorBoardPosition;
        private static Vector3 monitorBoardRotation;
        private static Vector3 virtualScreenPosition;
        private static Vector3 virtualScreenRotation;
        private static Vector3 virtualScreenScale;
        private static bool trackFollowing;
        private static bool monitorBoardIsActive = false;
        private static float distance;
        private static VariousVirtualScreenTracker tracker;

        public static Vector3 MonitorBoardPosition
        {
            set
            {
                monitorBoardPosition = value;
            }
            get
            {
                return monitorBoardPosition;
            }
        }

        public static Vector3 MonitorBoardRotation
        {
            set
            {
                monitorBoardRotation = value;
            }
            get
            {
                return monitorBoardRotation;
            }
        }

        public static Vector3 VirtualScreenPosition
        {
            set
            {
                virtualScreenPosition = value;
            }
            get
            {
                return virtualScreenPosition;
            }
        }
        
        public static Vector3 VirtualScreenRotation
        {
            set
            {
                virtualScreenRotation = value;
            }
            get
            {
                return virtualScreenRotation;
            }
        }

        public static Vector3 VirtualScreenScale
        {
            set
            {
                virtualScreenScale = value;
            }
            get
            {
                return virtualScreenScale;
            }
        }

        public static bool TrackFollowing
        {
            set
            {
                trackFollowing = value;
            }
            get
            {
                return trackFollowing;
            }
        }

        public static bool MonitorBoardIsActive
        {
            set
            {
                monitorBoardIsActive = value;
            }
            get
            {
                return monitorBoardIsActive;
            }
        }

        public static float Distance
        {
            set
            {
                distance = value;
            }
            get
            {
                return distance;
            }
        }

        public static VariousVirtualScreenTracker Tracker

        {
            set
            {
                tracker = value;
            }
            get
            {
                return tracker;
            }
        }
    }
}