namespace MouseController
{
    public class MouseCursorOperationInfo
    {
        private static MouseVariousTracker tracker = MouseVariousTracker.ViveController;

        public static MouseVariousTracker Tracker
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