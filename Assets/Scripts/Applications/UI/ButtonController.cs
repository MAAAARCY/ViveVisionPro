using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Applications.VirtualScreen;
using Applications.FrontCamera;
using Vive;

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

            if (pointerInCollider && controllerGetStateDown)
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
                        case "UseHand":
                            UseHand.SwitchUseHand();
                            UseHand.SwitchIconName();
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
        /*
        public void EnableFrontCamera()
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
        */
    }
}