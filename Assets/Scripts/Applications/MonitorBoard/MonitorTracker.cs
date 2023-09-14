using UnityEngine;
using Vive;

namespace Applications.MonitorBoard
{
    public class MonitorTracker: MonoBehaviour
    {
        [SerializeField] 
        private Transform MonitorBoardTransform;

        [SerializeField] 
        private ViveProInfo VivePro;

        [SerializeField]
        private Vector3 MonitorBoardPosition;

        [SerializeField]
        private Vector3 MonitorBoardRotation;

        [SerializeField]
        private ViveLaserPointer LeftLaserPointer;

        [SerializeField]
        private ViveLaserPointer RightLaserPointer;
        
        [SerializeField]
        private float _distance;

        [SerializeField]
        private bool _trackFollowing = true;

        [SerializeField]
        private MonitorVariousTracker _tracker;

        private Vector3 LaserPointerPositionDelta;
        private Vector3 HMDRotation;
        private Vector3 LaserPointerRotation;

        private void Start()
        {
            MonitorBoardInfo.MonitorBoardPosition = MonitorBoardPosition;
            MonitorBoardInfo.MonitorBoardRotation = MonitorBoardRotation;
            MonitorBoardInfo.TrackFollowing = _trackFollowing;
            MonitorBoardInfo.Distance = _distance;
            MonitorBoardInfo.Tracker = _tracker;
        }

        private void Update()
        {
            switch(MonitorBoardInfo.Tracker)
            {
                case MonitorVariousTracker.TrackFollowing:
                    UpdateMonitorBoardTransform();
                    break;
                case MonitorVariousTracker.ViveController:
                    MoveMonitorBoardByViveController();
                    break;
                case MonitorVariousTracker.Keep:
                    MonitorBoardTransform.position = MonitorBoardInfo.MonitorBoardPosition;
                    MonitorBoardTransform.rotation = Quaternion.Euler(MonitorBoardInfo.MonitorBoardRotation);
                    break;
            }

            MonitorBoardInfo.Tracker = _tracker;
        }

        private void UpdateMonitorBoardTransform()
        {
            HMDRotation = VivePro.GetHMDRotation();

            float x = MonitorBoardInfo.Distance * Mathf.Sin(HMDRotation.y * Mathf.PI / 180f);
            float y = -MonitorBoardInfo.Distance * Mathf.Sin(HMDRotation.x * Mathf.PI / 180f);
            float z = MonitorBoardInfo.Distance * Mathf.Cos(HMDRotation.y * Mathf.PI / 180f);

            //transformを変更する部分
            MonitorBoardTransform.position = new Vector3(x, y, z);
            MonitorBoardTransform.rotation = Quaternion.Euler(HMDRotation.x, HMDRotation.y, HMDRotation.z);

            //MonitorBoardInfoにpositionとrotationを格納し、他のプログラムから座標を取れるようにするため
            MonitorBoardInfo.MonitorBoardPosition = MonitorBoardTransform.position;
            MonitorBoardInfo.MonitorBoardRotation = MonitorBoardTransform.rotation.eulerAngles;       
        }

        private void MoveMonitorBoardByViveController()
        {
            if (ViveController.GrabLeftGrip && VivePro.GetLeftControllerState())
            {
                if (LeftLaserPointer.GetLaserPointerPosition() == Vector3.zero) return;

                LaserPointerRotation = VivePro.GetLeftControllerRotation();
                LaserPointerPositionDelta = VivePro.GetLeftControllerPositionDelta();

                MonitorBoardTransform.position = PolarCoordinates(LaserPointerRotation, MonitorBoardInfo.Distance+LaserPointerPositionDelta.z);
                MonitorBoardTransform.rotation = Quaternion.Euler(LaserPointerRotation.x, LaserPointerRotation.y, 0f);

                MonitorBoardInfo.MonitorBoardPosition = MonitorBoardTransform.position;
                MonitorBoardInfo.MonitorBoardRotation = MonitorBoardTransform.rotation.eulerAngles;
            }

            if (ViveController.GrabRightGrip && VivePro.GetRightControllerState())
            {
                if (RightLaserPointer.GetLaserPointerPosition() == Vector3.zero) return;

                LaserPointerRotation = VivePro.GetRightControllerRotation();
                LaserPointerPositionDelta = VivePro.GetRightControllerPositionDelta();

                MonitorBoardInfo.Distance += (20f * LaserPointerPositionDelta.z);

                MonitorBoardTransform.position = PolarCoordinates(LaserPointerRotation, MonitorBoardInfo.Distance);
                MonitorBoardTransform.rotation = Quaternion.Euler(LaserPointerRotation.x, LaserPointerRotation.y, 0f);

                MonitorBoardInfo.MonitorBoardPosition = MonitorBoardTransform.position;
                MonitorBoardInfo.MonitorBoardRotation = MonitorBoardTransform.rotation.eulerAngles;
            }
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
