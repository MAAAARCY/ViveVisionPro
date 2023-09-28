using MouseController;

namespace Applications.UI
{
    public class CursorOperation
    {
        private static string label = "Laser Pointer";

        public static string Label
        {
            get
            {
                return label;
            }
        }

        public static void SwitchCursorOperation()
        {
            switch (MouseCursorOperationInfo.Tracker)
            {
                case MouseVariousTracker.ViveController:
                    MouseCursorOperationInfo.Tracker = MouseVariousTracker.TrackPad;
                    label = "Track Pad";
                    break;
                case MouseVariousTracker.TrackPad:
                    MouseCursorOperationInfo.Tracker = MouseVariousTracker.EyeTracking;
                    label = "Eye Tracking";
                    break;
                case MouseVariousTracker.EyeTracking:
                    MouseCursorOperationInfo.Tracker = MouseVariousTracker.HMDRotation;
                    label = "HMD Rotation";
                    break;
                case MouseVariousTracker.HMDRotation:
                    MouseCursorOperationInfo.Tracker = MouseVariousTracker.ViveController;
                    label = "Laser Pointer";
                    break;
            }
        }
    }
}