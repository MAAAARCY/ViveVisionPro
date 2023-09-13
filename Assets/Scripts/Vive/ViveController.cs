using UnityEngine;
using Valve.VR;
using System;

namespace Vive
{
    public class ViveController : MonoBehaviour
    {
        //InteractUIÉ{É^Éì
        //private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
        private SteamVR_Action_Boolean Iui = SteamVR_Input.GetBooleanAction("InteractUI");
        //åãâ ÇÃäiî[ópBooleanå^ä÷êîineractui
        private static Boolean interactLeftUI;
        private static Boolean interactRightUI;

        //GrabGripÉ{É^Éì
        //private SteamVR_Action_Boolean GrabG = SteamVR_Actions.default_GrabGrip;
        private SteamVR_Action_Boolean GrabG = SteamVR_Input.GetBooleanAction("GrabGrip");
        //åãâ ÇÃäiî[ópBooleanå^ä÷êîgrapgrip
        private Boolean grapgrip;

        //TrackPad
        private SteamVR_Action_Vector2 TrackPad = SteamVR_Input.GetVector2Action("TrackPad");
        private static Vector2 LeftTrackPadAxis;
        private static Vector2 LeftTrackPadAxisDelta;
        private static Vector2 RightTrackPadAxis;
        private static Vector2 RightTrackPadAxisDelta;

        void Update()
        {
            interactLeftUI = Iui.GetState(SteamVR_Input_Sources.LeftHand);
            interactRightUI = Iui.GetState(SteamVR_Input_Sources.RightHand);
            LeftTrackPadAxis = TrackPad.GetLastAxis(SteamVR_Input_Sources.LeftHand);
            LeftTrackPadAxisDelta = TrackPad.GetLastAxisDelta(SteamVR_Input_Sources.LeftHand);
            RightTrackPadAxis = TrackPad.GetLastAxis(SteamVR_Input_Sources.RightHand);
            RightTrackPadAxisDelta = TrackPad.GetLastAxisDelta(SteamVR_Input_Sources.RightHand);

            if (interactLeftUI || interactRightUI)
            {
                Debug.Log("InteractUI(Left):" + interactLeftUI + ", InteractUI(Right):" + interactRightUI);
            }

            grapgrip = GrabG.GetState(SteamVR_Input_Sources.LeftHand);

            if (grapgrip)
            {
                Debug.Log("GrabGrip");
            }

            //Debug.Log("x:" + LeftTrackPadAxis.x + ", y:" + LeftTrackPadAxis.y);
            //Debug.Log("x:" + LeftTrackPadAxisDelta.x + ", y:" + LeftTrackPadAxisDelta.y);
        }

        public static Boolean InteractLeftUIState
        {
            get
            {
                return interactLeftUI;
            }
        }

        public static Boolean InteractRightUIState
        {
            get
            {
                return interactRightUI;
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
