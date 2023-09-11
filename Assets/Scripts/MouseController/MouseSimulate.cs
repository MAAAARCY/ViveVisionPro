using System.Runtime.InteropServices;
using System;

namespace MouseController
{
    public class MouseSimulate
    {
        private const UInt32 MOUSEEVENTF_MOVE = 0x0001;
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const UInt32 MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const UInt32 MOUSEEVENTF_RIGHTUP = 0x0010;
        private const UInt32 MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const UInt32 MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const UInt32 MOUSEEVENTF_WHEEL = 0x0800;
        private const UInt32 MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        protected internal static void LeftDownClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        protected internal static void LeftUpClick()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        protected internal static void RightDownClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        }

        protected internal static void RightUpClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        protected internal static void MiddleDownClick()
        {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
        }

        protected internal static void MiddleUpClick()
        {
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
        }
    }
}