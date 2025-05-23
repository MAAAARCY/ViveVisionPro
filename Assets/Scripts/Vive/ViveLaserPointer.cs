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
        private bool left = true;

        [SerializeField]
        private Color color;

        [SerializeField]
        private float thickness = 0.002f;

        [SerializeField]
        private Color clickColor = Color.green;

        [SerializeField]
        private GameObject holder;

        [SerializeField]
        private GameObject laser;

        [SerializeField]
        private GameObject pointer;
        
        private bool isActive = false;

        [SerializeField]
        private bool addRigidBody = false;

        [SerializeField]
        private Transform reference;

        [SerializeField]
        private ViveProInfo VivePro;
        
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        private RaycastHit hit;
        private Transform previousContact = null;
        private bool pointerInCollider = false;
        private float pointerRadius = 0.05f;

        private bool bHit = false;

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

            laser = GameObject.CreatePrimitive(PrimitiveType.Cube);
            laser.transform.parent = holder.transform;
            laser.transform.localScale = new Vector3(thickness, thickness, 100f);
            laser.transform.localPosition = new Vector3(0f, 0f, 50f);
            laser.transform.localRotation = Quaternion.identity;

            pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointer.GetComponent<SphereCollider>().enabled = false;
            pointer.transform.parent = this.transform;
            pointer.transform.localScale = new Vector3(pointerRadius, pointerRadius, pointerRadius);
            pointer.transform.localPosition = new Vector3(0f, 0f, 0f);

            BoxCollider collider = laser.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = laser.AddComponent<Rigidbody>();
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

            laser.GetComponent<MeshRenderer>().material = newMaterial;
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
            pointer.SetActive(false);
        }
        /*
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
        */
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

        public Vector3 GetLaserPointerPosition()
        {
            if (pointerInCollider)
            {
                Debug.Log("X:" + hit.point.x + ", Y:" + hit.point.y + ", Z:" + hit.point.z);
                return hit.point;
            }
            else
            {
                Debug.Log("hoge");
                return Vector3.zero;
            }
        }

        public bool GetPointerInCollider()
        {
            return pointerInCollider;
        }

        public string GetColliderName()
        {
            if (!(bHit))
            {
                return "None";
            }
            return hit.collider.name;
        }

        private void Update()
        {
            if (left)
            {
                if (VivePro.GetLeftControllerState())
                {
                    UpdateRayCast(ViveController.InteractLeftGetState);
                }
            }
            else
            {
                if (VivePro.GetRightControllerState())
                {
                    UpdateRayCast(ViveController.InteractRightGetState);
                }
            }
        }

        private void UpdateRayCast(bool controllerState)
        {
            if (!isActive)
            {
                isActive = true;
                laser.SetActive(true);
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast = new Ray(transform.position, transform.forward);
            bHit = Physics.Raycast(raycast, out hit);

            if (!bHit)
            {
                previousContact = null;
                pointerInCollider = false;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;

                pointerInCollider = true;
                //Debug.Log(pointer.transform.localPosition);
            }

            if (controllerState)
            {
                laser.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                laser.GetComponent<MeshRenderer>().material.color = clickColor;
                pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            }
            else
            {
                laser.transform.localScale = new Vector3(thickness, thickness, dist);
                laser.GetComponent<MeshRenderer>().material.color = color;
                pointer.GetComponent<MeshRenderer>().material.color = color;
            }

            if (pointerInCollider)
            {
                pointer.transform.position = hit.point;
                //pointer.transform.localPosition = new Vector3(0, 0, dist);
                pointer.SetActive(true);
            }
            else
            {
                pointer.transform.position = Vector3.zero;
                pointer.SetActive(false);
            }
            
            laser.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
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
