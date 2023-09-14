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
        private Vector3 LeftControllerPosition;
        //左コントローラの回転座標格納用（クォータニオン）
        private Quaternion LeftControllerRotationQ;
        //左コントローラの回転座標格納用
        private Vector3 LeftControllerRotation;

        //右コントローラの位置座標格納用
        private Vector3 RightControllerPosition;
        //右コントローラの回転座標格納用（クォータニオン）
        private Quaternion RightControllerRotationQ;
        //右コントローラの回転座標格納用
        private Vector3 RightControllerRotation;

        [SerializeField]
        private Transform HMDCameraTransform;

        [SerializeField]
        private ViveController LeftController;

        [SerializeField]
        private ViveController RightController;


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


            //LeftController（左コントローラ）の情報を一時保管--------------------
            //位置座標を取得
            LeftControllerPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            //回転座標をクォータニオンで値を受け取る
            LeftControllerRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
            //取得した値をクォータニオン → オイラー角に変換
            LeftControllerRotation = LeftControllerRotationQ.eulerAngles;
            //--------------------------------------------------------------


            //RightController（右コントローラ）の情報を一時保管--------------------
            //位置座標を取得
            RightControllerPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
            //回転座標をクォータニオンで値を受け取る
            RightControllerRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
            //取得した値をクォータニオン → オイラー角に変換
            RightControllerRotation = RightControllerRotationQ.eulerAngles;
            //--------------------------------------------------------------

            //Debug.Log(RightControllerRotation);
        }

        public Vector3 GetHMDPosition()
        {
            return HMDPosition;
        }

        public Vector3 GetHMDRotation()
        {
            return HMDRotation;
        }

        public Vector3 GetLeftControllerPosition()
        {
            return LeftControllerPosition;
        }

        public Vector3 GetLeftControllerRotation()
        {
            return LeftControllerRotation;
        }

        public Vector3 GetRightControllerPosition()
        {
            return RightControllerPosition;
        }

        public Vector3 GetRightControllerRotation()
        {
            return RightControllerRotation;
        }

        public Vector3 GetHMDForward()
        {
            return HMDCameraTransform.forward;
        }

        public bool GetLeftControllerState()
        {
            if (LeftController != null)
            {
                return true;
            }

            return false;
        }

        public bool GetRightControllerState()
        {
            if (RightController != null)
            {
                return true;
            }

            return false;
        }
    }

}