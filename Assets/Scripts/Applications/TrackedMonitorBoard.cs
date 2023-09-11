using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace Applications
{
    public class TrackedMonitorBoard : MonoBehaviour
    {
        [SerializeField] private Transform MonitorBoard;

        //HMD�̈ʒu���W�i�[�p
        private Vector3 HMDPosition;
        //HMD�̉�]���W�i�[�p�i�N�H�[�^�j�I���j
        private Quaternion HMDRotationQ;
        //HMD�̉�]���W�i�[�p�i�I�C���[�p�j
        private Vector3 HMDRotation;

        [SerializeField]
        private Vector3 MonitorBoardPosition;
        //MonitorBoard�̉�]���W�i�[�p�i�I�C���[�p�j
        [SerializeField]
        private Vector3 MonitorBoardRotation;

        [SerializeField]
        private float _distance;

        [SerializeField]
        private bool _trackFollowing = true;

        void Update()
        {
            //Head�i�w�b�h�}�E���h�f�B�X�v���C�j�̏����ꎞ�ۊ�-----------
            //�ʒu���W���擾
            HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
            //��]���W���N�H�[�^�j�I���Œl���󂯎��
            HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
            //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
            HMDRotation = HMDRotationQ.eulerAngles;
            //--------------------------------------------------------------
            UpdateMonitorBoardaTransform();
        }

        private void UpdateMonitorBoardaTransform()
        {
            if (_trackFollowing)
            {
                //�ʒu���W���擾
                HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
                //��]���W���N�H�[�^�j�I���Œl���󂯎��
                HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
                //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
                HMDRotation = HMDRotationQ.eulerAngles;

                //HMD�ƃJ�����̂R�����Ƃ̋���
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
