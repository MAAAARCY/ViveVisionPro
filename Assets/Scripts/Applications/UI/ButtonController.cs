using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vive;

using Applications.VirtualScreen;
using Applications.FrontCamera;
using MouseController;
using EyeTracking;
using RaySettings;

namespace Applications.UI
{
    public class ButtonController : MonoBehaviour, IEventSystemHandler
    {
        [SerializeField] 
        private ViveLaserPointer LeftLaserPointer;
        
        [SerializeField]
        private ViveLaserPointer RightLaserPointer;

        [SerializeField]
        private ViveProInfo VivePro;

        private bool isActive = true;

        private string applicationName = null;

        private bool controllerGetState;
        private bool controllerGetStateUp;
        private bool controllerGetStateDown;

        private bool pointerInCollider;

        private float xdelta; //controllerÇÃxï˚å¸ÇÃìÆÇ´ÇÃç∑ï™

        private void Start()
        {

        }

        private void Update()
        {
            if (MouseCursorOperationInfo.Tracker == MouseVariousTracker.HMDRotation)
            {
                applicationName = RayPositionInfo.GetColliderName();
                SelectButton(RayPositionInfo.PointerInCollider && !(EyeTrackingInfo.LeftOpenness));
            }

            if (ViveProInfo.UseLeftHand)
            {
                controllerGetState = ViveController.InteractLeftGetState;
                controllerGetStateUp = ViveController.InteractLeftGetStateUp;
                controllerGetStateDown = ViveController.InteractLeftGetStateDown;

                applicationName = LeftLaserPointer.GetColliderName();
                pointerInCollider = LeftLaserPointer.GetPointerInCollider();
                xdelta = VivePro.GetLeftControllerPositionDelta().x;
            }
            else
            {
                controllerGetState = ViveController.InteractRightGetState;
                controllerGetStateUp = ViveController.InteractRightGetStateUp;
                controllerGetStateDown = ViveController.InteractRightGetStateDown;

                applicationName = RightLaserPointer.GetColliderName();
                pointerInCollider = RightLaserPointer.GetPointerInCollider();
                xdelta = VivePro.GetRightControllerPositionDelta().x;
            }

            SelectButton(pointerInCollider && controllerGetStateDown);

            //Debug.Log("Select?: " + (RayPositionInfo.PointerInCollider && !(EyeTrackingInfo.LeftOpenness)));


            if (pointerInCollider && controllerGetState)
            {
                switch (applicationName)
                {
                    case "Handle":
                        ScreenSizeChanger.SizeChanger(xdelta);
                        break;
                }
            }
        }

        private void SelectButton(bool pressed)
        {
            if (pressed)
            {
                if (isActive)
                {
                    switch (applicationName)
                    {
                        case "VirtualScreen":
                            EnableVirtualScreen.SwitchVirtualScreenState();
                            break;
                        case "FrontCamera":
                            EnableFrontCamera.SwitchFrontCameraState();
                            break;
                        case "Settings":
                            SettingsMenu.Menu();
                            break;
                        case "UseHand":
                            UseHand.SwitchUseHand();
                            UseHand.SwitchIconName();
                            break;
                        case "ScreenFollows":
                            SettingsMenu.EnableScreenFollows();
                            break;
                        case "CursorOperation":
                            CursorOperation.SwitchCursorOperation();
                            break;
                    }

                    Debug.Log(applicationName);
                    isActive = false;
                }
            }
            else
            {
                isActive = true;
            }
        }
    }
}