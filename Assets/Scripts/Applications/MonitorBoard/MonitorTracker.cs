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

        private Vector3 HMDRotation;
        
        [SerializeField]
        private float _distance;

        [SerializeField]
        private bool _trackFollowing = true;

        [SerializeField]
        private MonitorVariousTracker _tracker;

        private Vector3 LaserPointPosition;

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

            //transform��ύX���镔��
            MonitorBoardTransform.position = new Vector3(x, y, z);
            MonitorBoardTransform.rotation = Quaternion.Euler(HMDRotation.x, HMDRotation.y, HMDRotation.z);

            //MonitorBoardInfo��position��rotation���i�[���A���̃v���O����������W������悤�ɂ��邽��
            MonitorBoardInfo.MonitorBoardPosition = MonitorBoardTransform.position;
            MonitorBoardInfo.MonitorBoardRotation = MonitorBoardTransform.rotation.eulerAngles;       
        }

        private void MoveMonitorBoardByViveController()
        {
            if (ViveController.GrabLeftGrip && VivePro.GetLeftControllerState())
            {
                if (LeftLaserPointer.GetLaserPointerPosition() == Vector3.zero) return;

                LaserPointPosition = LeftLaserPointer.GetLaserPointerPosition();

                MonitorBoardTransform.position = LaserPointPosition;

                MonitorBoardInfo.MonitorBoardPosition = MonitorBoardTransform.position;

                //Debug.Log("RIGHT");

                //Debug.Log(VivePro.GetRightControllerRotation());
            }
        }
    }
}