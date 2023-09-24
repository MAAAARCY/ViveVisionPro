using UnityEngine;

namespace Applications.MonitorBoard
{
    public sealed class MonitorBoardInfo
    {
        private static Vector3 monitorBoardPosition;
        private static Vector3 monitorBoardRotation;
        private static bool trackFollowing;
        private static bool monitorBoardIsActive = false;
        private static float distance;
        private static MonitorVariousTracker tracker;

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

        public static MonitorVariousTracker Tracker

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