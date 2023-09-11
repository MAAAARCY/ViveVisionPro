using System;
using UnityEngine;
using Valve.VR;

public class VIVELeftControllerTest : MonoBehaviour
{
    //InteractUIÉ{É^Éì
    //private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    private SteamVR_Action_Boolean Iui = SteamVR_Input.GetBooleanAction("InteractUI");
    //åãâ ÇÃäiî[ópBooleanå^ä÷êîineractui
    private Boolean interacrtui;

    //GrabGripÉ{É^Éì
    //private SteamVR_Action_Boolean GrabG = SteamVR_Actions.default_GrabGrip;
    private SteamVR_Action_Boolean GrabG = SteamVR_Input.GetBooleanAction("GrabGrip");
    //åãâ ÇÃäiî[ópBooleanå^ä÷êîgrapgrip
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
