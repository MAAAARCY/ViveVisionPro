using System.Runtime.InteropServices;
using System;
using ViveSR.anipal.Eye;
using Vive;
using UnityEngine;
using RaySettings;

namespace MouseController
{
    public class MouseCursorTracker : MonoBehaviour
    {
        //呼び出したデータ格納用の関数
        EyeData eye;

        //焦点情報--------------------
        //両目の焦点格納変数
        //レイの始点と方向（多分③の内容と同じ）
        Ray CombineRay;
        /*レイがどこに焦点を合わせたかの情報．Vector3 point : 視線ベクトルと物体の衝突位置，float distance : 見ている物体までの距離，
           Vector3 normal:見ている物体の面の法線ベクトル，Collider collider : 衝突したオブジェクトのCollider，Rigidbody rigidbody：衝突したオブジェクトのRigidbody，Transform transform：衝突したオブジェクトのTransform*/
        //焦点位置にオブジェクトを出すためにpublicにしています．
        public static FocusInfo CombineFocus;
        //レイの半径
        //float combineFocusradius;
        //レイの最大の長さ
        //float combineFocusmaxDistance;
        //オブジェクトを選択的に無視するために使用されるレイヤー ID
        //int combinefocusableLayer = 0;
        //------------------------------

        //視線情報--------------------
        //origin：起点，direction：レイの方向　x,y,z軸
        Vector3 CombineGazeRayorigin;
        //両目の視線格納変数
        Vector3 CombineGazeRaydirection;
        //------------------------------

        [SerializeField]
        private MouseVariousTracker Tracker;

        [SerializeField]
        private Transform SphereTransform;

        [SerializeField]
        private ViveLaserPointer LeftLaserPointer;

        [SerializeField]
        private ViveLaserPointer RightLaserPointer;

        [SerializeField]
        private EyePositionInfo EyePosition;

        [SerializeField]
        private ViveProInfo VivePro;

        [SerializeField]
        private bool useEyeTracking;

        [SerializeField]
        private bool useEyeFocus;

        [SerializeField]
        private bool useLeftHand;

        private Vector2 CursorPosition;

        private Vector3 DebugFocusPosition;

        private void Update()
        {
            switch(Tracker)
            {
                case MouseVariousTracker.ViveController:
                    moveCursorByViveController();
                    break;
                case MouseVariousTracker.TrackPad:
                    moveCursorByTrackPad();
                    break;
                case MouseVariousTracker.EyeTracking:
                    moveCursorByEyeTracking();
                    break;
                case MouseVariousTracker.HMDRotation:
                    moveCursorByHMDRotation();
                    break;
            }
        }

        private void moveCursorByViveController()
        {
            if (useLeftHand && VivePro.GetLeftControllerState())
            {
                if (LeftLaserPointer.GetLaserPointerUVPosition() == Vector2.zero)
                {
                    return;
                }

                MouseCursorPositioning.setCursorPositionByLaserPointer(LeftLaserPointer.GetLaserPointerUVPosition());
                MouseClicker.clickOnce(0, ViveController.InteractLeftUIState);
            }

            if (!(useLeftHand) && VivePro.GetRightControllerState())
            {
                if (RightLaserPointer.GetLaserPointerUVPosition() == Vector2.zero)
                {
                    return;
                }

                MouseCursorPositioning.setCursorPositionByLaserPointer(RightLaserPointer.GetLaserPointerUVPosition());
                MouseClicker.clickOnce(0, ViveController.InteractRightUIState);
            }
        }

        private void moveCursorByTrackPad()
        {
            if (useLeftHand && VivePro.GetLeftControllerState() != null)
            {
                //MouseClicker.clickOnce(0, ViveController.InteractLeftUIState);
                
                if (ViveController.LeftTrackPadTouchDelta == Vector2.zero)
                {
                    return;
                }

                MouseCursorPositioning.setCursorPositionByTrackPad(ViveController.LeftTrackPadTouchDelta, 10.0f);
            }

            if (!(useLeftHand) && VivePro.GetRightControllerState())
            {
                //MouseClicker.clickOnce(0, ViveController.InteractRightUIState);

                if (ViveController.RightTrackPadTouchDelta == Vector2.zero)
                {
                    return;
                }
                
                MouseCursorPositioning.setCursorPositionByTrackPad(ViveController.RightTrackPadTouchPosition, 10.0f);
            }
        }

        private void moveCursorByEyeTracking()
        {
            SRanipal_Eye_API.GetEyeData(ref eye);

            if (useEyeFocus)
            {
                if (SRanipal_Eye.Focus(GazeIndex.COMBINE, out CombineRay, out CombineFocus))
                {
                    Vector2 EyeForcusUVPosition = EyePosition.GetEyeFocusUVPosition(CombineRay);
                    MouseCursorPositioning.setCursorPositionByEyeTracking(EyeForcusUVPosition);
                }
            }
            else
            {
                if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out CombineGazeRayorigin, out CombineGazeRaydirection, eye))
                {
                    Vector2 EyeGazeRayUVPosition = EyePosition.GetGazeRayUVPosition(new Ray(CombineGazeRayorigin, CombineGazeRaydirection));
                    MouseCursorPositioning.setCursorPositionByEyeTracking(EyeGazeRayUVPosition);
                }
            }
        }

        private void moveCursorByHMDRotation()
        {
            Debug.Log(VivePro.GetHMDPosition());
            Vector2 HMDForwardUVPosition = EyePosition.GetHMDForwardUVPosition(new Ray(VivePro.GetHMDPosition(), VivePro.GetHMDForward()));
            MouseCursorPositioning.setCursorPositionByHMDForward(HMDForwardUVPosition);
        }
    }
}

