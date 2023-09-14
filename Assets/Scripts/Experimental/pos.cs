using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class pos : MonoBehaviour
{
    /*
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

    private List<XRNodeState> nodeStates = new List<XRNodeState>();

    void Start()
    {
        
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.Head);

        Debug.Log(device.name);
        if (device.isValid)
        {
            Debug.Log(string.Format("name:{0}, role:{1}, manufacturer:{2}, characteristics:{3}, serialNumber:{4},", device.name, device.role, device.manufacturer, device.characteristics, device.serialNumber));
            //if (device.TryGetFeatureValue(CommonUsages.eyesData, out eyes))
            //    return true;
        }
        
    }

    void Update()
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


        //�擾�����f�[�^��\���iHMDP�FHMD�ʒu�CHMDR�FHMD��]�CLFHR�F���R���ʒu�CLFHR�F���R����]�CRGHP�F�E�R���ʒu�CRGHR�F�E�R����]�j
        //Debug.Log("HMDP:" + HMDPosition.x + ", " + HMDPosition.y + ", " + HMDPosition.z + "\n" +
        //            "HMDR:" + HMDRotation.x + ", " + HMDRotation.y + ", " + HMDRotation.z);
        //Debug.Log("LFHP:" + LeftHandPosition.x + ", " + LeftHandPosition.y + ", " + LeftHandPosition.z + "\n" +
        //            "LFHR:" + LeftHandRotation.x + ", " + LeftHandRotation.y + ", " + LeftHandRotation.z);
        //Debug.Log("RGHP:" + RightHandPosition.x + ", " + RightHandPosition.y + ", " + RightHandPosition.z + "\n" +
        //            "RGHR:" + RightHandRotation.x + ", " + RightHandRotation.y + ", " + RightHandRotation.z);

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
            Debug.Log(string.Format("name:{0}, role:{1}, manufacturer:{2}, characteristics:{3}, serialNumber:{4},", device.name, device.role, device.manufacturer, device.characteristics, device.serialNumber));
        }
    }
    */
}
