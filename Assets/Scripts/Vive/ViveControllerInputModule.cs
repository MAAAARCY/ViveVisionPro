using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using System.Collections.Generic;

namespace Vive
{
    public class ViveControllerInputModule : BaseInputModule
    {
        private List<RaycastResult> RaycastResultCache;
        private Camera RayCamera;

        //[SerializeField]
        //private SteamVR_Input_Source InputSourceLeft;
        //[SerializeField]
        //private InputSource InputSourceRight;

        [SerializeField]
        private SteamVR_Action_Boolean InteractUI = SteamVR_Input.GetBooleanAction("InteractUI");

        //private List<InputSource> Poses = new List<InputSource> { InputSourceLeft, InputSourceRight };

        private GameObject CurrentObject = null;
        private PointerEventData Data = null;

        protected override void Awake()
        {
            base.Awake();

            Data = new PointerEventData(eventSystem);
            RaycastResultCache = new List<RaycastResult>();
        }

        protected override void Start()
        {
            base.Start();

            if (InteractUI == null)
                Debug.LogError("No UI interaction action has been set on this component", this);
            //InputSourceLeft.Initialize(eventSystem);
            //InputSourceRight.Initialize(eventSystem);

            RayCamera = new GameObject("RayCamera").AddComponent<Camera>();
            RayCamera.clearFlags = CameraClearFlags.Nothing;
            RayCamera.cullingMask = 0;
            RayCamera.enabled = false;
            RayCamera.fieldOfView = 1;
            RayCamera.nearClipPlane = 0.01f;

            //canvas.worldCamera = RayCamera;
        }
        /*
        public override void Process()
        {
            //Reset data, set camera
            Data.Reset();
            Data.position = new Vector2(RayCamera.pixelWidth * 0.5f, RayCamera.pixelHeight * 0.5f);

            //Raycast
            eventSystem.RaycastAll(Data, RaycastResultCache);
            Data.pointerCurrentRaycast = FindFirstRaycast(RaycastResultCache);
            CurrentObject = Data.pointerCurrentRaycast.gameObject;

            Debug.Log(RaycastResultCache.Count);

            //Clear
            Debug.Log(Data);
            Debug.Log(CurrentObject);
            HandlePointerExitAndEnter(Data, CurrentObject);

            //Press
            if (ViveController.InteractLeftGetStateDown || ViveController.InteractRightGetStateDown)
            {
                ProcessPress(Data);
            }

            //Release
            if (ViveController.InteractLeftGetStateUp || ViveController.InteractRightGetStateUp)
            {
                ProcessRelease(Data);
            }
        }
        */
        public override void Process()
        {
            //if (!InputSourceLeft.Validate() || !InputSourceRight.Validate())
            //    return;

            //Poses.ForEach(ProcessEvents);
        }

        private void UpdateCameraPositionTo(Transform transform)
        {
            RayCamera.transform.position = transform.position;
            RayCamera.transform.rotation = transform.rotation;
        }
        /*
        private void ProcessPress(PointerEventData data)
        {
            //set raycast
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            //Check for object hit, get the down handler, call
            GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(CurrentObject, data, ExecuteEvents.pointerDownHandler);

            //If no down handler, try and get click handler
            if (newPointerPress == null) newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(CurrentObject);

            //Set data
            data.pressPosition = data.position;
            data.pointerPress = newPointerPress;
            data.rawPointerPress = CurrentObject;
        }

        private void ProcessRelease(PointerEventData data)
        {
            //Execute pointer up
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

            //Check for click handler
            GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(CurrentObject);

            //Check if actual
            if (data.pointerPress == pointerUpHandler)
            {
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
            }

            //Clear selected gameobject
            eventSystem.SetSelectedGameObject(null);

            //Reset data
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;
        }
        */
    }
}