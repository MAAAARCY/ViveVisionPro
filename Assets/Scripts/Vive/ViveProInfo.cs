using UnityEngine;
using UnityEngine.XR;

namespace Vive
{
    public class ViveProInfo : MonoBehaviour
    {
        //HMD�̈ʒu���W�i�[�p
        private Vector3 HMDPosition;
        //HMD�̉�]���W�i�[�p�i�N�H�[�^�j�I���j
        private Quaternion HMDRotationQ;
        //HMD�̉�]���W�i�[�p�i�I�C���[�p�j
        private Vector3 HMDRotation;

        //���R���g���[���̈ʒu���W�i�[�p
        private Vector3 LeftHandPosition;
        //���R���g���[���̉�]���W�i�[�p�i�N�H�[�^�j�I���j
        private Quaternion LeftHandRotationQ;
        //���R���g���[���̉�]���W�i�[�p
        private Vector3 LeftHandRotation;

        //�E�R���g���[���̈ʒu���W�i�[�p
        private Vector3 RightHandPosition;
        //�E�R���g���[���̉�]���W�i�[�p�i�N�H�[�^�j�I���j
        private Quaternion RightHandRotationQ;
        //�E�R���g���[���̉�]���W�i�[�p
        private Vector3 RightHandRotation;

        [SerializeField]
        private Transform HMDCameraTransform;

        private void Update()
        {
            //Head�i�w�b�h�}�E���h�f�B�X�v���C�j�̏����ꎞ�ۊ�-----------
            //�ʒu���W���擾
            HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
            //HMDPosition = InputTracking.GetNodeState(XRNode.Head).devicePosition;
            //HMDPosition = InputDevice.TryGetFeatureValue
            //��]���W���N�H�[�^�j�I���Œl���󂯎��
            HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
            //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
            HMDRotation = HMDRotationQ.eulerAngles;
            //--------------------------------------------------------------


            //LeftHand�i���R���g���[���j�̏����ꎞ�ۊ�--------------------
            //�ʒu���W���擾
            //LeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            //��]���W���N�H�[�^�j�I���Œl���󂯎��
            //LeftHandRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
            //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
            //LeftHandRotation = LeftHandRotationQ.eulerAngles;
            //--------------------------------------------------------------


            //RightHand�i�E�R���g���[���j�̏����ꎞ�ۊ�--------------------
            //�ʒu���W���擾
            //RightHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
            //��]���W���N�H�[�^�j�I���Œl���󂯎��
            //RightHandRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
            //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
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