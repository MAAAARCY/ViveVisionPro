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

        //[SerializeField] 
        //private Button EnableVirtualScreenButton;

        //[SerializeField] 
        //private Button EnableFrontCameraButton;

        [SerializeField]
        private GameObject Handle; 

        [SerializeField]
        private Slider SizeChangeBar;

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
            //EnableVirtualScreenButton.onClick.AddListener(EnableVirtualScreen);
            //EnableFrontCameraButton.onClick.AddListener(EnableFrontCamera);
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
                //Debug.Log(LeftLaserPointer.GetColliderName());

                if (isActive)
                {
                    switch (applicationName)
                    {
                        case "VirtualScreen":
                            EnableVirtualScreen();
                            break;
                        case "FrontCamera":
                            EnableFrontCamera();
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
                //Debug.Log(LeftLaserPointer.GetColliderName());

                switch (applicationName)
                {
                    case "Handle":
                        ExecuteEvents.Execute(Handle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        MoveHandle();
                        break;
                }

                //Debug.Log(applicationName);
            }

            //Debug.Log("pointerInCollider"+ pointerInCollider + ", controllerGetState: " + controllerGetState);
        }

        public void MoveHandle()
        {
            Debug.Log("MoveHandle: " + VivePro.GetRightControllerPositionDelta().x);
            SizeChangeBar.value += VivePro.GetRightControllerPositionDelta().x * 10;
            VirtualScreenInfo.VirtualScreenScale = new Vector3(SizeChangeBar.value, SizeChangeBar.value * 0.5f, 1.0f);
        }

        public void EnableVirtualScreen()
        {
            if (!(VirtualScreenInfo.MonitorBoardIsActive))
            {
                VirtualScreenInfo.MonitorBoardIsActive = true;
                return;
            }
            else
            {
                VirtualScreenInfo.MonitorBoardIsActive = false;
                return;
            }
        }

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
    }
}