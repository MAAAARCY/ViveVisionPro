using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Applications.MonitorBoard;
using Applications.FrontCamera;
using Vive;

namespace Applications.UI
{
    public class ButtonController : MonoBehaviour, IEventSystemHandler
    {
        [SerializeField] 
        private ViveLaserPointer LaserPointer;
        
        [SerializeField] 
        private Button EnableVirtualScreenButton;
        
        [SerializeField] 
        private Button EnableFrontCameraButton;

        [SerializeField]
        private GameObject Handle; 

        [SerializeField]
        private Slider SizeChangeBar;

        [SerializeField]
        private ViveProInfo VivePro;

        private bool isActive = true;

        private string applicationName = null;

        private void Start()
        {
            EnableVirtualScreenButton.onClick.AddListener(EnableVirtualScreen);
            EnableFrontCameraButton.onClick.AddListener(EnableFrontCamera);
        }

        private void Update()
        {
            if (LaserPointer.GetPointerInCollider() && ViveController.InteractRightGetStateDown)
            {
                Debug.Log(LaserPointer.GetColliderName());

                if (isActive)
                {
                    applicationName = LaserPointer.GetColliderName();
                    
                    switch (applicationName)
                    {
                        case "VirtualScreen":
                            //ExecuteEvents.Execute(EnableVirtualScreenButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                            EnableVirtualScreen();
                            break;
                        case "FrontCamera":
                            //ExecuteEvents.Execute(EnableFrontCameraButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                            break;
                    }
                    
                    Debug.Log(applicationName);
                    //TestButton.onClick.Invoke();
                    //OnPointerClick();
                    isActive = false;
                }
            }
            else
            {
                isActive = true;
            }

            if (LaserPointer.GetPointerInCollider() && ViveController.InteractRightUIState)
            {
                Debug.Log(LaserPointer.GetColliderName());

                applicationName = LaserPointer.GetColliderName();

                switch (applicationName)
                {
                    case "Handle":
                        ExecuteEvents.Execute(Handle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        MoveHandle();
                        break;
                }

                Debug.Log(applicationName);
            }
        }

        public void MoveHandle()
        {
            SizeChangeBar.value += VivePro.GetRightControllerPositionDelta().x * 10;
            Debug.Log(VivePro.GetRightControllerPositionDelta().x);
        }

        public void EnableVirtualScreen()
        {
            Debug.Log("Clicked");
            if (!(MonitorBoardInfo.MonitorBoardIsActive))
            {
                MonitorBoardInfo.MonitorBoardIsActive = true;
                return;
            }
            else
            {
                //MonitorBoard.SetActive(false);
                MonitorBoardInfo.MonitorBoardIsActive = false;
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