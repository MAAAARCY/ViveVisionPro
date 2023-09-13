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
        //�Ăяo�����f�[�^�i�[�p�̊֐�
        EyeData eye;

        //�œ_���--------------------
        //���ڂ̏œ_�i�[�ϐ�
        //���C�̎n�_�ƕ����i�����B�̓��e�Ɠ����j
        Ray CombineRay;
        /*���C���ǂ��ɏœ_�����킹�����̏��DVector3 point : �����x�N�g���ƕ��̂̏Փˈʒu�Cfloat distance : ���Ă��镨�̂܂ł̋����C
           Vector3 normal:���Ă��镨�̖̂ʂ̖@���x�N�g���CCollider collider : �Փ˂����I�u�W�F�N�g��Collider�CRigidbody rigidbody�F�Փ˂����I�u�W�F�N�g��Rigidbody�CTransform transform�F�Փ˂����I�u�W�F�N�g��Transform*/
        //�œ_�ʒu�ɃI�u�W�F�N�g���o�����߂�public�ɂ��Ă��܂��D
        public static FocusInfo CombineFocus;
        //���C�̔��a
        //float combineFocusradius;
        //���C�̍ő�̒���
        //float combineFocusmaxDistance;
        //�I�u�W�F�N�g��I��I�ɖ������邽�߂Ɏg�p����郌�C���[ ID
        //int combinefocusableLayer = 0;
        //------------------------------

        //�������--------------------
        //origin�F�N�_�Cdirection�F���C�̕����@x,y,z��
        Vector3 CombineGazeRayorigin;
        //���ڂ̎����i�[�ϐ�
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

