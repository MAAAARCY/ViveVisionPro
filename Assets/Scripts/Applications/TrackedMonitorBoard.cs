using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace Applications
{
    public class TrackedMonitorBoard : MonoBehaviour
    {
        [SerializeField] private Transform MonitorBoard;

        //HMDの位置座標格納用
        private Vector3 HMDPosition;
        //HMDの回転座標格納用（クォータニオン）
        private Quaternion HMDRotationQ;
        //HMDの回転座標格納用（オイラー角）
        private Vector3 HMDRotation;

        [SerializeField]
        private Vector3 MonitorBoardPosition;
        //MonitorBoardの回転座標格納用（オイラー角）
        [SerializeField]
        private Vector3 MonitorBoardRotation;

        [SerializeField]
        private float _distance;

        [SerializeField]
        private bool _trackFollowing = true;

        void Update()
        {
            //Head（ヘッドマウンドディスプレイ）の情報を一時保管-----------
            //位置座標を取得
            HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
            //回転座標をクォータニオンで値を受け取る
            HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
            //取得した値をクォータニオン → オイラー角に変換
            HMDRotation = HMDRotationQ.eulerAngles;
            //--------------------------------------------------------------
            UpdateMonitorBoardaTransform();
        }

        private void UpdateMonitorBoardaTransform()
        {
            if (_trackFollowing)
            {
                //位置座標を取得
                HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
                //回転座標をクォータニオンで値を受け取る
                HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
                //取得した値をクォータニオン → オイラー角に変換
                HMDRotation = HMDRotationQ.eulerAngles;

                //HMDとカメラの３軸ごとの距離
                float xDistance = Mathf.Abs(MonitorBoard.position.x - HMDPosition.x);
                float yDistance = Mathf.Abs(MonitorBoard.position.y - HMDPosition.y);
                float zDistance = Mathf.Abs(MonitorBoard.position.z - HMDPosition.z);

                float x = _distance * Mathf.Sin(HMDRotation.y * Mathf.PI / 180f);
                float y = -_distance * Mathf.Sin(HMDRotation.x * Mathf.PI / 180f);
                float z = _distance * Mathf.Cos(HMDRotation.y * Mathf.PI / 180f); // Mathf.Sqrt(Mathf.Pow(zDistance, 2.0f) + Mathf.Pow(xDistance, 2.0f))


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
