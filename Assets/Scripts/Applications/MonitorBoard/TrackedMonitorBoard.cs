using UnityEngine;
using Vive;

namespace Applications.MonitorBoard
{
    public class TrackedMonitorBoard : MonoBehaviour
    {
        [SerializeField] 
        private Transform MonitorBoardTransform;

        [SerializeField] 
        private ViveProInfo VivePro;

        [SerializeField]
        private Vector3 MonitorBoardPosition;

        [SerializeField]
        private Vector3 MonitorBoardRotation;

        private Vector3 HMDRotation;
        
        [SerializeField]
        private float _distance;

        [SerializeField]
        private bool _trackFollowing = true;

        private void Start()
        {
            MonitorBoardInfo.MonitorBoardPosition = MonitorBoardPosition;
            MonitorBoardInfo.MonitorBoardRotation = MonitorBoardRotation;
            MonitorBoardInfo.TrackFollowing = _trackFollowing;
            MonitorBoardInfo.Distance = _distance;
        }

        private void Update()
        {
            UpdateMonitorBoardaTransform();
        }

        private void UpdateMonitorBoardaTransform()
        {
            if (MonitorBoardInfo.TrackFollowing)
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
            else
            {
                MonitorBoardTransform.position = MonitorBoardInfo.MonitorBoardPosition;
                MonitorBoardTransform.rotation = Quaternion.Euler(MonitorBoardInfo.MonitorBoardRotation);
            }

            MonitorBoardInfo.TrackFollowing = _trackFollowing; //Debug�p�̑���Ȃ̂Ŋ������������
        }
    }
}
