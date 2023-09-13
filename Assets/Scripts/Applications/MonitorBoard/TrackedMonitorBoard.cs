using UnityEngine;
using Vive;

namespace Applications.MonitorBoard
{
    public class TrackedMonitorBoard : MonoBehaviour
    {
        [SerializeField] 
        private Transform MonitorBoard;

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

        private void Update()
        {
            UpdateMonitorBoardaTransform();
        }

        private void UpdateMonitorBoardaTransform()
        {
            if (_trackFollowing)
            {
                HMDRotation = VivePro.GetHMDRotation();

                float x = _distance * Mathf.Sin(HMDRotation.y * Mathf.PI / 180f);
                float y = -_distance * Mathf.Sin(HMDRotation.x * Mathf.PI / 180f);
                float z = _distance * Mathf.Cos(HMDRotation.y * Mathf.PI / 180f);
                

                MonitorBoard.position = new Vector3(x, y, z);
                MonitorBoard.rotation = Quaternion.Euler(HMDRotation.x, HMDRotation.y, HMDRotation.z);

                MonitorBoardPosition = MonitorBoard.position;
                MonitorBoardRotation = MonitorBoard.rotation.eulerAngles;
            }
            else
            {
                MonitorBoard.position = MonitorBoardPosition;
                MonitorBoard.rotation = Quaternion.Euler(MonitorBoardRotation);
            }

        }
    }
}
