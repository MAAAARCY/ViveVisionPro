using UnityEngine;
using UnityEngine.XR;

namespace Vive
{
    public class ViveProInfo : MonoBehaviour
    {
        //HMDの位置座標格納用
        private Vector3 HMDPosition;
        //HMDの回転座標格納用（クォータニオン）
        private Quaternion HMDRotationQ;
        //HMDの回転座標格納用（オイラー角）
        private Vector3 HMDRotation;

        //左コントローラの位置座標格納用
        private Vector3 LeftHandPosition;
        //左コントローラの回転座標格納用（クォータニオン）
        private Quaternion LeftHandRotationQ;
        //左コントローラの回転座標格納用
        private Vector3 LeftHandRotation;

        //右コントローラの位置座標格納用
        private Vector3 RightHandPosition;
        //右コントローラの回転座標格納用（クォータニオン）
        private Quaternion RightHandRotationQ;
        //右コントローラの回転座標格納用
        private Vector3 RightHandRotation;

        [SerializeField]
        private Transform HMDCameraTransform;

        private void Update()
        {
            //Head（ヘッドマウンドディスプレイ）の情報を一時保管-----------
            //位置座標を取得
            HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
            //HMDPosition = InputTracking.GetNodeState(XRNode.Head).devicePosition;
            //HMDPosition = InputDevice.TryGetFeatureValue
            //回転座標をクォータニオンで値を受け取る
            HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
            //取得した値をクォータニオン → オイラー角に変換
            HMDRotation = HMDRotationQ.eulerAngles;
            //--------------------------------------------------------------


            //LeftHand（左コントローラ）の情報を一時保管--------------------
            //位置座標を取得
            //LeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            //回転座標をクォータニオンで値を受け取る
            //LeftHandRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
            //取得した値をクォータニオン → オイラー角に変換
            //LeftHandRotation = LeftHandRotationQ.eulerAngles;
            //--------------------------------------------------------------


            //RightHand（右コントローラ）の情報を一時保管--------------------
            //位置座標を取得
            //RightHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
            //回転座標をクォータニオンで値を受け取る
            //RightHandRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
            //取得した値をクォータニオン → オイラー角に変換
            //RightHandRotation = RightHandRotationQ.eulerAngles;
            //--------------------------------------------------------------
        }

        public Vector3 GetHMDPosition()
        {
            return HMDPosition;
        }

        public Vector3 GetHMDRotation()
        {
            return HMDRotation;
        }

        public Vector3 GetLeftHandPosition()
        {
            return HMDPosition;
        }

        public Vector3 GetLeftHandRotation()
        {
            return HMDRotation;
        }

        public Vector3 GetRightHandPosition()
        {
            return HMDPosition;
        }

        public Vector3 GetRightHandRotation()
        {
            return HMDRotation;
        }

        public Vector3 GetHMDForward()
        {
            //Debug.Log(HMDCameraTransform.forward);
            return HMDCameraTransform.forward;
        }
    }

}