using System;
using UnityEngine;
using Valve.VR;

public class VIVELeftControllerTest : MonoBehaviour
{
    //InteractUIボタン
    //private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    private SteamVR_Action_Boolean Iui = SteamVR_Input.GetBooleanAction("InteractUI");
    //結果の格納用Boolean型関数ineractui
    private Boolean interacrtui;

    //GrabGripボタン
    //private SteamVR_Action_Boolean GrabG = SteamVR_Actions.default_GrabGrip;
    private SteamVR_Action_Boolean GrabG = SteamVR_Input.GetBooleanAction("GrabGrip");
    //結果の格納用Boolean型関数grapgrip
    private Boolean grapgrip;

    void Update()
    {
        interacrtui = Iui.GetState(SteamVR_Input_Sources.LeftHand);

        if (interacrtui)
        {
            Debug.Log(interacrtui);
        }

        grapgrip = GrabG.GetState(SteamVR_Input_Sources.LeftHand);

        if (grapgrip)
        {
            Debug.Log("GrabGrip");
        }
    }
}
