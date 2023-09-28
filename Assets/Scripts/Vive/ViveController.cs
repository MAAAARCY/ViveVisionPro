using UnityEngine;
using Valve.VR;
using System;

namespace Vive
{
    public class ViveController : MonoBehaviour
    {
        //InteractUIÉ{É^Éì
        //private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
        private static SteamVR_Action_Boolean Iui = SteamVR_Input.GetBooleanAction("InteractUI");
        //åãâ ÇÃäiî[ópBooleanå^ä÷êîineractui
        private static Boolean interactLeftUI;
        private static Boolean interactRightUI;

        //GrabGripÉ{É^Éì
        //private SteamVR_Action_Boolean GrabG = SteamVR_Actions.default_GrabGrip;
        private SteamVR_Action_Boolean GrabG = SteamVR_Input.GetBooleanAction("GrabGrip");
        //åãâ ÇÃäiî[ópBooleanå^ä÷êîgrapgrip
        private static Boolean grabLeftGrip;
        private static Boolean grabRightGrip;

        //TrackPad
        private SteamVR_Action_Vector2 TrackPad = SteamVR_Input.GetVector2Action("TrackPad");
        private static Vector2 LeftTrackPadAxis;
        private static Vector2 LeftTrackPadAxisDelta;
        private static Vector2 RightTrackPadAxis;
        private static Vector2 RightTrackPadAxisDelta;

        void Update()
        {
            //interactLeftUI = Iui.GetState(SteamVR_Input_Sources.LeftHand);
            //interactRightUI = Iui.GetState(SteamVR_Input_Sources.RightHand);
            LeftTrackPadAxis = TrackPad.GetLastAxis(SteamVR_Input_Sources.LeftHand);
            LeftTrackPadAxisDelta = TrackPad.GetLastAxisDelta(SteamVR_Input_Sources.LeftHand);
            RightTrackPadAxis = TrackPad.GetLastAxis(SteamVR_Input_Sources.RightHand);
            RightTrackPadAxisDelta = TrackPad.GetLastAxisDelta(SteamVR_Input_Sources.RightHand);

            grabLeftGrip = GrabG.GetState(SteamVR_Input_Sources.LeftHand);
            grabRightGrip = GrabG.GetState(SteamVR_Input_Sources.RightHand);
            /*
            if (interactLeftUI || interactRightUI)
            {
                Debug.Log("InteractUI(Left):" + interactLeftUI + ", InteractUI(Right):" + interactRightUI);
            }

            if (grabLeftGrip || grabRightGrip)
            {
                Debug.Log("leftGrabGrip:" + grabLeftGrip + ", rightGrabGrip:" + grabRightGrip);
            }
            */
            //Debug.Log("x:" + LeftTrackPadAxis.x + ", y:" + LeftTrackPadAxis.y);
            //Debug.Log("x:" + LeftTrackPadAxisDelta.x + ", y:" + LeftTrackPadAxisDelta.y);
        }

        public static Boolean InteractLeftGetState
        {
            get
            {
                return Iui.GetState(SteamVR_Input_Sources.LeftHand);
            }
        }

        public static Boolean InteractRightGetState
        {
            get
            {
                return Iui.GetState(SteamVR_Input_Sources.RightHand);
            }
        }

        public static Boolean InteractLeftGetStateUp
        {
            get
            {
                return Iui.GetStateUp(SteamVR_Input_Sources.LeftHand);
            }
        }

        public static Boolean InteractRightGetStateUp
        {
            get
            {
                return Iui.GetStateUp(SteamVR_Input_Sources.RightHand);
            }
        }

        public static Boolean InteractLeftGetStateDown
        {
            get
            {
                return Iui.GetStateDown(SteamVR_Input_Sources.LeftHand);
            }
        }

        public static Boolean InteractRightGetStateDown
        {
            get
            {
                return Iui.GetStateDown(SteamVR_Input_Sources.RightHand);
            }
        }

        public static Boolean GrabLeftGrip
        {
            get
            {
                return grabLeftGrip;
            }
        }

        public static Boolean GrabRightGrip
        {
            get
            {
                return grabRightGrip;
            }
        }

        public static Vector2 LeftTrackPadTouchPosition
        {
            get
            {
                return LeftTrackPadAxis;
            }
        }

        public static Vector2 LeftTrackPadTouchDelta
        {
            get
            {
                //Debug.Log("x:" + LeftTrackPadAxisDelta.x + ", y:" + LeftTrackPadAxisDelta.y);
                return LeftTrackPadAxisDelta;
            }
        }

        public static Vector2 RightTrackPadTouchPosition
        {
            get
            {
                return RightTrackPadAxis;
            }
        }

        public static Vector2 RightTrackPadTouchDelta
        {
            get
            {
                return RightTrackPadAxisDelta;
            }
        }
    }
}
