using UnityEngine;
using ViveSR.anipal.Eye;

public class TryGetEyeInformation : MonoBehaviour
{
    //取得呼び出し
    EyeData eye;

    //どのくらいまぶたを開けているか
    float leftOpenness;
    float rightOpenness;

    //視線の起点の座標（角膜の中心）mm単位
    Vector3 LeftGazeOrigin;
    Vector3 RightGazeOrigin;

    //瞳孔の位置
    Vector2 LeftPupilPosition;
    Vector2 RightPupilPosition;

    //瞳孔の直径
    float leftPupiltDiameter;
    float rightPupiltDiameter;

    void Update()
    {
        //取得呼び出し
        SRanipal_Eye_API.GetEyeData(ref eye);

        //どのぐらいまぶたを開いているか
        eyeOpennessData(eye);

        //視線の起点の座標（角膜の中心）mm単位
        eyeGazeOriginData(eye);

        //瞳孔の位置
        eyePupilPositionData(eye);

        //瞳孔の直径
        eyePupilDiameterData(eye);
    }

    void eyeOpennessData(EyeData eye)
    {
        if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_EYE_OPENNESS_VALIDITY))
        {
            leftOpenness = eye.verbose_data.left.eye_openness;
            Debug.Log("Left Openness：" + leftOpenness);
        }

        if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_EYE_OPENNESS_VALIDITY))
        {
            rightOpenness = eye.verbose_data.right.eye_openness;
            Debug.Log("RIght Openness：" + rightOpenness);
        }
    }

    void eyeGazeOriginData(EyeData eye)
    {
        if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
        {
            LeftGazeOrigin = eye.verbose_data.left.gaze_origin_mm;
            Debug.Log("Left GazeOrigin：" + LeftGazeOrigin.x + ", " + LeftGazeOrigin.y + ", " + LeftGazeOrigin.z);
        }

        if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
        {
            RightGazeOrigin = eye.verbose_data.right.gaze_origin_mm;
            Debug.Log("Right GazeOrigin：" + RightGazeOrigin.x + ", " + RightGazeOrigin.y + ", " + RightGazeOrigin.z);
        }
    }

    void eyePupilPositionData(EyeData eye)
    {
        if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_POSITION_IN_SENSOR_AREA_VALIDITY))
        {
            LeftPupilPosition = eye.verbose_data.left.pupil_position_in_sensor_area;
            Debug.Log("Left Pupil Position：" + LeftPupilPosition.x + ", " + LeftPupilPosition.y);
        }

        if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_POSITION_IN_SENSOR_AREA_VALIDITY))
        {
            RightPupilPosition = eye.verbose_data.right.pupil_position_in_sensor_area;
            Debug.Log("Right GazeOrigin：" + RightPupilPosition.x + ", " + RightPupilPosition.y);
        }
    }

    void eyePupilDiameterData(EyeData eye)
    {
        if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_DIAMETER_VALIDITY))
        {
            leftPupiltDiameter = eye.verbose_data.left.pupil_diameter_mm;
            Debug.Log("Left Pupilt Diameter：" + leftPupiltDiameter);
        }

        if (eye.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_PUPIL_DIAMETER_VALIDITY))
        {
            rightPupiltDiameter = eye.verbose_data.right.pupil_diameter_mm;
            Debug.Log("Right Pupilt Diameter：" + rightPupiltDiameter);
        }
    }
}
