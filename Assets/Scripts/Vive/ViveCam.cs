using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using System.Collections.Generic;
using System.Linq;

namespace Vive
{
    public class ViveCam : MonoBehaviour
    {
        [SerializeField]
        private bool _enableScreen = true;

        [SerializeField]
        private bool _undistorted = true;

        [SerializeField]
        private bool _cropped = true;

        [SerializeField]
        private Transform _viveFrontCameraView;

        //���ڍ��E�̐؂�ւ��p
        [SerializeField]
        private bool left = true;

        [SerializeField]
        private Material _material;

        [SerializeField]
        private float distance;

        //HMD�̈ʒu���W�i�[�p
        private Vector3 headPosition;
        //HMD�̉�]���W�i�[�p�i�N�H�[�^�j�I���j
        private Quaternion headRotationQ;
        //HMD�̉�]���W�i�[�p�i�I�C���[�p�j
        private Vector3 headRotation;

        private List<XRNodeState> nodeStates = new List<XRNodeState>();


        #region ### MonoBehaviour ###
        private void OnEnable()
        {
            EnableSteamVRCamera();
        }

        private void OnDisable()
        {
            DisableSteamVRCamera();
        }

        private void Update()
        {
            if (_enableScreen)
            {
                UpdateCameraTexture();
            }
        }
        #endregion ### MonoBehaviour ###

        private void UpdateCameraTexture()
        {
            var source = SteamVR_TrackedCamera.Source(_undistorted);
            var texture = source.texture;

            if (texture == null)
            {
                return;
            }

            _material.mainTexture = texture;

            float aspect = (float)texture.width / texture.height;

            if (_cropped)
            {
                var bounds = source.frameBounds;
                _material.mainTextureOffset = new Vector2(bounds.uMin, bounds.vMin);

                float du = bounds.uMax - bounds.uMin;
                float dv = bounds.vMax - bounds.vMin;

                if (left) //����
                {
                    _material.mainTextureScale = new Vector2(du, dv / 2);
                }
                else //�E��
                {
                    _material.mainTextureScale = new Vector2(du, -dv / 2);
                }

                aspect *= Mathf.Abs(du / dv);
            }
            else
            {
                _material.mainTextureOffset = Vector2.zero;
                _material.mainTextureScale = new Vector2(1f, -1f);
            }

            if (source.hasTracking)
            {
                const float ProjectionZ = 1.0f;
                Vector2 ProjectionScale = GetProjectionScale(source);
                Vector2 LocalScale = new Vector2(4.0f * ProjectionZ / ProjectionScale.x, 4.0f * ProjectionZ / ProjectionScale.y);

                if (left)
                {
                    _viveFrontCameraView.localScale = new Vector3(LocalScale.x, LocalScale.y / 2, 1.0f);
                }
                else
                {
                    _viveFrontCameraView.localScale = new Vector3(LocalScale.x, -LocalScale.y / 2, 1.0f);
                }

                var trackedCameraTransform = source.transform;

                UpdateFrontCameraTransform(trackedCameraTransform, ProjectionZ);
            }

        }

        private void UpdateFrontCameraTransform(SteamVR_Utils.RigidTransform sourceTransform, float ProjectionZ)
        {
            InputTracking.GetNodeStates(nodeStates);

            var headState = nodeStates.FirstOrDefault(node => node.nodeType == XRNode.Head);
            //�ʒu���W���擾
            headPosition = InputTracking.GetLocalPosition(XRNode.Head);
            //headPosition = headState.TryGetPosition(out headPosition);
            //��]���W���N�H�[�^�j�I���Œl���󂯎��
            headRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
            //�擾�����l���N�H�[�^�j�I�� �� �I�C���[�p�ɕϊ�
            headRotation = headRotationQ.eulerAngles;

            //HMD�ƃJ�����̂R�����Ƃ̋���
            float xDistance = Mathf.Abs(_viveFrontCameraView.position.x - headPosition.x);
            float yDistance = Mathf.Abs(_viveFrontCameraView.position.y - headPosition.y);
            float zDistance = Mathf.Abs(_viveFrontCameraView.position.z - headPosition.z);

            //
            float x = distance * Mathf.Sin(headRotation.y * Mathf.PI / 180f);
            float y = -distance * Mathf.Sin(headRotation.x * Mathf.PI / 180f);
            float z = distance * Mathf.Cos(headRotation.y * Mathf.PI / 180f); // Mathf.Sqrt(Mathf.Pow(zDistance, 2.0f) + Mathf.Pow(xDistance, 2.0f))

            //Debug.Log("HMD yRotation:" + headRotation.y);

            _viveFrontCameraView.localPosition = sourceTransform.TransformPoint(new Vector3(0, 0, ProjectionZ));
            _viveFrontCameraView.localRotation = sourceTransform.rot;
        }

        private void EnableSteamVRCamera()
        {
            var source = SteamVR_TrackedCamera.Source(_undistorted);
            source.Acquire();

            // �J�������F������Ă��Ȃ�������disable�ɂ���
            if (!source.hasCamera)
            {
                enabled = false;
            }
        }

        private void DisableSteamVRCamera()
        {
            _material.mainTexture = null;

            var source = SteamVR_TrackedCamera.Source(_undistorted);
            source.Release();
        }

        private static Vector2 GetProjectionScale(SteamVR_TrackedCamera.VideoStreamTexture source)
        {
            CVRTrackedCamera trackedCamera = OpenVR.TrackedCamera;

            if (trackedCamera == null) return Vector2.one;

            const float near = 1.0f;
            const float far = 100.0f;
            HmdMatrix44_t ProjectionMatrix = new HmdMatrix44_t();

            if (trackedCamera.GetCameraProjection(source.deviceIndex, source.frameId, source.frameType, near, far, ref ProjectionMatrix) != EVRTrackedCameraError.None)
            {
                return Vector2.one;
            }

            return new Vector2(ProjectionMatrix.m0, ProjectionMatrix.m5);
        }
    }
}