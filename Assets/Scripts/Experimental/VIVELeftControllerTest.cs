using System;
using UnityEngine;
using Valve.VR;

public class VIVELeftControllerTest : MonoBehaviour
{
    //InteractUI�{�^��
    //private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    private SteamVR_Action_Boolean Iui = SteamVR_Input.GetBooleanAction("InteractUI");
    //���ʂ̊i�[�pBoolean�^�֐�ineractui
    private Boolean interacrtui;

    //GrabGrip�{�^��
    //private SteamVR_Action_Boolean GrabG = SteamVR_Actions.default_GrabGrip;
    private SteamVR_Action_Boolean GrabG = SteamVR_Input.GetBooleanAction("GrabGrip");
    //���ʂ̊i�[�pBoolean�^�֐�grapgrip
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
