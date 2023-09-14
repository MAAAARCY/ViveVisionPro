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
        private Vector3 LeftControllerPosition;
        //���R���g���[���̉�]���W�i�[�p�i�N�H�[�^�j�I���j
        private Quaternion LeftControllerRotationQ;
        //���R���g���[���̉�]���W�i�[�p
        private Vector3 LeftControllerRotation;

        //�E�R���g���[���̈ʒu���W�i�[�p
        private Vector3 RightControllerPosition;
        //�E�R���g���[���̉�]���W�i�[�p�i�N�H�[�^�j�I���j
        private Quaternion RightControllerRotationQ;
        //�E�R���g���[���̉�]���W�i�[�p
        private Vector3 RightControllerRotation;

        [SerializeField]
        private Transform HMDCameraTransform;

        [SerializeField]
        private ViveController LeftController;

        [SerializeField]
        private ViveController RightController;


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


            //LeftController�i���R���g���[���j�̏����ꎞ�ۊ�--------------------
            //�ʒu���W���擾
            LeftControllerPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            //��]���W���N�H�[�^�j�I���Œl���󂯎��
            LeftControllerRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
            //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
            LeftControllerRotation = LeftControllerRotationQ.eulerAngles;
            //--------------------------------------------------------------


            //RightController�i�E�R���g���[���j�̏����ꎞ�ۊ�--------------------
            //�ʒu���W���擾
            RightControllerPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
            //��]���W���N�H�[�^�j�I���Œl���󂯎��
            RightControllerRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
            //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
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