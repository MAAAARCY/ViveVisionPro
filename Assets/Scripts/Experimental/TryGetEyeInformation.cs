using UnityEngine;
using ViveSR.anipal.Eye;

public class TryGetEyeInformation : MonoBehaviour
{
    //�擾�Ăяo��
    EyeData eye;

    //�ǂ̂��炢�܂Ԃ����J���Ă��邩
    float leftOpenness;
    float rightOpenness;

    //�����̋N�_�̍��W�i�p���̒��S�jmm�P��
    Vector3 LeftGazeOrigin;
    Vector3 RightGazeOrigin;

    //���E�̈ʒu
    Vector2 LeftPupilPosition;
    Vector2 RightPupilPosition;

    //���E�̒��a
    float leftPupiltDiameter;
    float rightPupiltDiameter;

    void Update()
    {
        //�擾�Ăяo��
        SRanipal_Eye_API.GetEyeData(ref eye);

        //�ǂ̂��炢�܂Ԃ����J���Ă��邩
        eyeOpennessData(eye);

        //�����̋N�_�̍��W�i�p���̒��S�jmm�P��
        eyeGazeOriginData(eye);

        //���E�̈ʒu
        eyePupilPositionData(eye);

        //���E�̒��a
        eyePupilDiameterData(eye);
    }

    void eyeOpennessData(EyeData eye)
    {
        if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_EYE_OPENNESS_VALIDITY))
        {
            leftOpenness = eye.verbose_data.left.eye_openness;
            Debug.Log("Left Openness�F" + leftOpenness);
        }

        if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_EYE_OPENNESS_VALIDITY))
        {
            rightOpenness = eye.verbose_data.right.eye_openness;
            Debug.Log("RIght Openness�F" + rightOpenness);
        }
    }

    void eyeGazeOriginData(EyeData eye)
    {
        if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
        {
            LeftGazeOrigin = eye.verbose_data.left.gaze_origin_mm;
            Debug.Log("Left GazeOrigin�F" + LeftGazeOrigin.x + ", " + LeftGazeOrigin.y + ", " + LeftGazeOrigin.z);
        }

        if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
        {
            RightGazeOrigin = eye.verbose_data.right.gaze_origin_mm;
            Debug.Log("Right GazeOrigin�F" + RightGazeOrigin.x + ", " + RightGazeOrigin.y + ", " + RightGazeOrigin.z);
        }
    }

    void eyePupilPositionData(EyeData eye)
    {
        if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_POSITION_IN_SENSOR_AREA_VALIDITY))
        {
            LeftPupilPosition = eye.verbose_data.left.pupil_position_in_sensor_area;
            Debug.Log("Left Pupil Position�F" + LeftPupilPosition.x + ", " + LeftPupilPosition.y);
        }

        if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_POSITION_IN_SENSOR_AREA_VALIDITY))
        {
            RightPupilPosition = eye.verbose_data.right.pupil_position_in_sensor_area;
            Debug.Log("Right GazeOrigin�F" + RightPupilPosition.x + ", " + RightPupilPosition.y);
        }
    }

    void eyePupilDiameterData(EyeData eye)
    {
        if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_DIAMETER_VALIDITY))
        {
            leftPupiltDiameter = eye.verbose_data.left.pupil_diameter_mm;
            Debug.Log("Left Pupilt Diameter�F" + leftPupiltDiameter);
        }

        if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_DIAMETER_VALIDITY))
        {
            rightPupiltDiameter = eye.verbose_data.right.pupil_diameter_mm;
            Debug.Log("Right Pupilt Diameter�F" + rightPupiltDiameter);
        }
    }
}
