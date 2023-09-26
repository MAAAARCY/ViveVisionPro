using UnityEngine;
using Vive;

namespace Applications.VirtualScreen
{
    public class VirtualScreenTracker: MonoBehaviour
    {
        [SerializeField]
        private GameObject VirtualScreen;

        [SerializeField] 
        private Transform MonitorBoardTransform;

        [SerializeField]
        private Transform VirtualScreenTransform;

        [SerializeField] 
        private ViveProInfo VivePro;

        [SerializeField]
        private Vector3 MonitorBoardPosition;

        [SerializeField]
        private Vector3 MonitorBoardRotation;

        [SerializeField]
        private Vector3 VirtualScreenScale;

        [SerializeField]
        private ViveLaserPointer LeftLaserPointer;

        [SerializeField]
        private ViveLaserPointer RightLaserPointer;
        
        [SerializeField]
        private float _distance;

        [SerializeField]
        private bool _trackFollowing = true;

        [SerializeField]
        private VariousVirtualScreenTracker _tracker;

        private Vector3 LaserPointerPositionDelta;
        private Vector3 HMDRotation;
        private Vector3 LaserPointerRotation;

        private void Start()
        {
            VirtualScreenInfo.MonitorBoardPosition = MonitorBoardPosition;
            VirtualScreenInfo.MonitorBoardRotation = MonitorBoardRotation;

            VirtualScreenInfo.VirtualScreenPosition = MonitorBoardPosition;
            VirtualScreenInfo.VirtualScreenRotation = MonitorBoardRotation;
            VirtualScreenInfo.VirtualScreenScale = VirtualScreenScale;

            VirtualScreenInfo.TrackFollowing = _trackFollowing;
            VirtualScreenInfo.Distance = _distance;
            VirtualScreenInfo.Tracker = _tracker;
        }

        private void Update()
        {
            if (VirtualScreenInfo.MonitorBoardIsActive)
            {
                VirtualScreen.SetActive(true);
            }
            else
            {
                VirtualScreen.SetActive(false);
            }

            switch(VirtualScreenInfo.Tracker)
            {
                case VariousVirtualScreenTracker.TrackFollowing:
                    UpdateVirtualScreenTransform();
                    break;
                case VariousVirtualScreenTracker.ViveController:
                    MoveVirtualScreenByViveController();
                    break;
                case VariousVirtualScreenTracker.Keep:
                    //MonitorBoardTransform.position = MonitorBoardInfo.MonitorBoardPosition;
                    VirtualScreenTransform.position = VirtualScreenInfo.VirtualScreenPosition;
                    VirtualScreenTransform.rotation = Quaternion.Euler(VirtualScreenInfo.VirtualScreenRotation);
                    break;
            }

            VirtualScreenInfo.Tracker = _tracker;

            VirtualScreenTransform.localScale = VirtualScreenInfo.VirtualScreenScale;
        }

        private void UpdateVirtualScreenTransform()
        {
            HMDRotation = VivePro.GetHMDRotation();

            float x = VirtualScreenInfo.Distance * Mathf.Sin(HMDRotation.y * Mathf.PI / 180f);
            float y = -VirtualScreenInfo.Distance * Mathf.Sin(HMDRotation.x * Mathf.PI / 180f);
            float z = VirtualScreenInfo.Distance * Mathf.Cos(HMDRotation.y * Mathf.PI / 180f);

            //transformを変更する部分
            VirtualScreenTransform.position = new Vector3(x, y, z);
            VirtualScreenTransform.rotation = Quaternion.Euler(HMDRotation.x, HMDRotation.y, HMDRotation.z);

            //VirtualScreenInfoにpositionとrotationを格納し、他のプログラムから座標を取れるようにするため
            VirtualScreenInfo.VirtualScreenPosition = VirtualScreenTransform.position;
            VirtualScreenInfo.VirtualScreenRotation = VirtualScreenTransform.rotation.eulerAngles;       
        }

        private void MoveVirtualScreenByViveController()
        {
            if (ViveController.GrabLeftGrip && VivePro.GetLeftControllerState())
            {
                if (LeftLaserPointer.GetLaserPointerPosition() == Vector3.zero) return;

                LaserPointerRotation = VivePro.GetLeftControllerRotation();
                LaserPointerPositionDelta = VivePro.GetLeftControllerPositionDelta();

                VirtualScreenInfo.Distance += (20f * LaserPointerPositionDelta.z);

                VirtualScreenTransform.position = PolarCoordinates(LaserPointerRotation, VirtualScreenInfo.Distance);
                VirtualScreenTransform.rotation = Quaternion.Euler(LaserPointerRotation.x, LaserPointerRotation.y, 0f);

                VirtualScreenInfo.MonitorBoardPosition = VirtualScreenTransform.position;
                VirtualScreenInfo.MonitorBoardRotation = VirtualScreenTransform.rotation.eulerAngles;
            }

            if (ViveController.GrabRightGrip && VivePro.GetRightControllerState())
            {
                if (RightLaserPointer.GetLaserPointerPosition() == Vector3.zero) return;

                LaserPointerRotation = VivePro.GetRightControllerRotation();
                LaserPointerPositionDelta = VivePro.GetRightControllerPositionDelta();

                VirtualScreenInfo.Distance += (20f * LaserPointerPositionDelta.z);

                VirtualScreenTransform.position = PolarCoordinates(LaserPointerRotation, VirtualScreenInfo.Distance);
                VirtualScreenTransform.rotation = Quaternion.Euler(LaserPointerRotation.x, LaserPointerRotation.y, 0f);

                VirtualScreenInfo.MonitorBoardPosition = VirtualScreenTransform.position;
                VirtualScreenInfo.MonitorBoardRotation = VirtualScreenTransform.rotation.eulerAngles;
            }

            //Debug.Log(ViveController.GrabRightGrip && VivePro.GetRightControllerState());
        }

        private Vector3 PolarCoordinates(Vector3 Rotation, float distance)
        {
            float x = distance * Mathf.Sin(Rotation.y * Mathf.PI / 180f);
            float y = -distance * Mathf.Sin(Rotation.x * Mathf.PI / 180f);
            float z = distance * Mathf.Cos(Rotation.y * Mathf.PI / 180f);

            return new Vector3(x, y, z);
        }
    }
}
