using System.Collections;
using UnityEngine;
using Valve.VR;

namespace Vive
{
    public class ViveLaserPointer : MonoBehaviour
    {
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
        
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        private RaycastHit hit;
        private Transform previousContact = null;
        private bool pointerInCollider = false;

        private void Start()
        {
            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.", this);


            holder = new GameObject();
            holder.transform.parent = this.transform;
            holder.transform.localPosition = Vector3.zero;
            holder.transform.localRotation = Quaternion.identity;

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = holder.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            pointer.transform.localRotation = Quaternion.identity;
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                rigidBody.isKinematic = true;
            }
            else
            {
                if (collider)
                {
                    Object.Destroy(collider);
                }
            }
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
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

        public Vector2 GetLaserPointerUVPosition()
        {
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

        private void Update()
        {
            if (!isActive)
            {
                //pointer.SetActive(false);
                //return;
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast = new Ray(transform.position, transform.forward);
            bool bHit = Physics.Raycast(raycast, out hit);

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                args.fromInputSource = pose.inputSource;
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.fromInputSource = pose.inputSource;
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

            if (bHit && interactWithUI.GetStateUp(pose.inputSource))
            {
                PointerEventArgs argsClick = new PointerEventArgs();
                argsClick.fromInputSource = pose.inputSource;
                argsClick.distance = hit.distance;
                argsClick.flags = 0;
                argsClick.target = hit.transform;
                OnPointerClick(argsClick);
            }

            if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
            {
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            }
            else
            {
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                pointer.GetComponent<MeshRenderer>().material.color = color;
            }

            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
        }

        public struct PointerEventArgs
        {
            public SteamVR_Input_Sources fromInputSource;
            public uint flags;
            public float distance;
            public Transform target;
        }

        public delegate void PointerEventHandler(object sender, PointerEventArgs e);
    }

}