using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

using MouseController;

namespace RaySettings
{
    public class EyePositionInfo : MonoBehaviour
    {
        /*
        [SerializeField]
        private SteamVR_Behaviour_Pose pose;

        [SerializeField]
        private SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");

        [SerializeField]
        private bool active = true;

        [SerializeField]
        private Color color;

        [SerializeField]
        private float thickness = 0.002f;

        [SerializeField]
        private Color clickColor = Color.green;

        [SerializeField]
        private GameObject holder;

        [SerializeField]
        private GameObject pointer;

        private bool isActive = false;

        [SerializeField]
        private bool addRigidBody = false;

        [SerializeField]
        private Transform reference;
        */
        [SerializeField]
        private Transform SphereTransform;

        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        private RaycastHit hit;
        private Transform previousContact = null;
        private bool pointerInCollider = false;

        private Queue<float> XParams = new Queue<float>();
        private Queue<float> YParams = new Queue<float>();

        //HMDの回転座標格納用（クォータニオン）
        private Quaternion headRotationQ;
        //HMDの回転座標格納用（オイラー角）
        private Vector3 headRotation;

        private float xMean;
        private float yMean;

        private void DetectCollider(Ray raycast)
        {
            float dist = 100f;

            //Ray raycast = new Ray(transform.position, transform.forward);
            bool bHit = Physics.Raycast(raycast, out hit);

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                //args.fromInputSource = pose.inputSource;
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                //argsIn.fromInputSource = pose.inputSource;
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            //SphereTransform.localPosition = new Vector3(0f, 0f, dist);
        }

        public Vector2 GetEyeFocusUVPosition(Ray raycast)
        {
            DetectCollider(raycast);

            if (pointerInCollider)
            {
                Debug.Log("UV_X:" + hit.textureCoord.x + ", UV_Y:" + hit.textureCoord.y);

                XParams = MouseCursorPositioning.setCursorPositionsByEyeFocus(XParams, hit, true);
                YParams = MouseCursorPositioning.setCursorPositionsByEyeFocus(YParams, hit, false);

                return new Vector2(float.Parse(XParams.Average().ToString("F0")), float.Parse(YParams.Average().ToString("F0")));

            }
            else
            {
                return Vector2.zero;
            }
        }

        public Vector3 GetEyeFocusPosition(Ray raycast)
        {
            DetectCollider(raycast);

            if (pointerInCollider)
            {
                Debug.Log("X:" + hit.point.x + ", Y:" + hit.point.y + ", Z:" + hit.point.z);
                return hit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public Vector2 GetGazeRayUVPosition(Ray raycast)
        {
            DetectCollider(raycast);

            //回転座標をクォータニオンで値を受け取る
            headRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
            //取得した値をクォータニオン → オイラー角に変換
            headRotation = headRotationQ.eulerAngles;

            if (pointerInCollider)
            {
                Debug.Log("UV_X:" + hit.textureCoord.x + ", UV_Y:" + hit.textureCoord.y);

                XParams = MouseCursorPositioning.setCursorPositionsByGazeRay(XParams, hit, headRotation, true);
                YParams = MouseCursorPositioning.setCursorPositionsByGazeRay(YParams, hit, headRotation, false);

                return new Vector2(float.Parse(XParams.Average().ToString("F0")), float.Parse(YParams.Average().ToString("F0")));

            }
            else
            {
                return Vector2.zero;
            }
        }

        public Vector2 GetHMDForwardUVPosition(Ray raycast)
        {
            DetectCollider(raycast);

            if (pointerInCollider)
            {
                Debug.Log("UV_X:" + hit.textureCoord.x + ", UV_Y:" + hit.textureCoord.y);
                return hit.textureCoord;
            }
            else
            {
                return Vector2.zero;
            }
        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            Debug.Log("This is OnPointerIn.");
            pointerInCollider = true;
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            Debug.Log("This is OnPointerOut.");
            pointerInCollider = false;
            if (PointerOut != null)
                PointerOut(this, e);
        }
    }

    public struct PointerEventArgs
    {
        public uint flags;
        public float distance;
        public Transform target;
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);
}