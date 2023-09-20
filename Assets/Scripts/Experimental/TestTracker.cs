using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class TestTracker : MonoBehaviour
{
    //トラッカーの位置座標格納用
    private Vector3 Tracker1Position;

    //トラッカーの回転座標格納用（クォータニオン）
    private Quaternion Tracker1RotationQ;
    //トラッカーの回転座標格納用（オイラー角）
    private Vector3 Tracker1Rotation;

    //トラッカーのpose情報を取得するためにtracker1という関数にSteamVR_Actions.default_Poseを固定
    private SteamVR_Action_Pose tracker1 = SteamVR_Actions.default_Pose;

    public Transform KeyboardTransform;

    public Vector3 parameter1;
    public Vector3 parameter2;
    public Vector3 parameter3;

    private Vector3 KeyboardPosition;
    private Vector3 KeyboardRotation;

    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {
        //位置座標を取得
        Tracker1Position = tracker1.GetLocalPosition(SteamVR_Input_Sources.Keyboard);
        //回転座標をクォータニオンで値を受け取る
        Tracker1RotationQ = tracker1.GetLocalRotation(SteamVR_Input_Sources.Keyboard);
        //取得した値をクォータニオン → オイラー角に変換
        Tracker1Rotation = Tracker1RotationQ.eulerAngles;

        //取得したデータを表示（T1D：Tracker1位置，T1R：Tracker1回転）
        //Debug.Log("T1D:" + Tracker1Position.x + ", " + Tracker1Position.y + ", " + Tracker1Position.z + "\n" +
        //            "T1R:" + Tracker1Rotation.x + ", " + Tracker1Rotation.y + ", " + Tracker1Rotation.z);

        //KeyboardPosition += Tracker1Posision;

        KeyboardTransform.localPosition = parameter1;
        KeyboardTransform.localRotation = Quaternion.Euler(parameter2);
        KeyboardTransform.localScale = parameter3;
    }
}
