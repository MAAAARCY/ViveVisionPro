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

        [SerializeField]
        private MeshFilter _cameraViewMeshFilter;

        //両目左右の切り替え用
        [SerializeField]
        private bool left = true;

        [SerializeField]
        private Material _material;

        [SerializeField]
        private Material _anotherMaterial;

        [SerializeField]
        private RenderTexture _renderTexture;

        [SerializeField]
        private float distance;

        [SerializeField]
        private bool useRenderTexture;

        //HMDの位置座標格納用
        private Vector3 headPosition;
        //HMDの回転座標格納用（クォータニオン）
        private Quaternion headRotationQ;
        //HMDの回転座標格納用（オイラー角）
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

            if (useRenderTexture)
            {
                
                //_material.mainTexture = texture;

                float aspect = (float)texture.width / texture.height;

                if (_cropped)
                {
                    var bounds = source.frameBounds;

                    float du = bounds.uMax - bounds.uMin;
                    float dv = bounds.vMax - bounds.vMin;

                    if (left) //左目
                    {
                        Graphics.Blit(texture, _renderTexture, new Vector2(du, dv / 2), new Vector2(bounds.uMin, bounds.vMin));
                    }
                    else //右目
                    {
                        Graphics.Blit(texture, _renderTexture, new Vector2(du, -dv / 2), new Vector2(bounds.uMin, bounds.vMin));
                    }

                    aspect *= Mathf.Abs(du / dv);
                }
                else
                {
                    _material.mainTextureOffset = Vector2.zero;
                    _material.mainTextureScale = new Vector2(1f, -1f);
                }

                _material.CopyPropertiesFromMaterial(_anotherMaterial);
                Debug.Log(_material == _anotherMaterial);
            }
            else
            {
                _material.mainTexture = texture;

                float aspect = (float)texture.width / texture.height;

                if (_cropped)
                {
                    var bounds = source.frameBounds;
                    _material.mainTextureOffset = new Vector2(bounds.uMin, bounds.vMin);

                    float du = bounds.uMax - bounds.uMin;
                    float dv = bounds.vMax - bounds.vMin;

                    if (left) //左目
                    {
                        _material.mainTextureScale = new Vector2(du, dv / 2); //dv/2
                    }
                    else //右目
                    {
                        _material.mainTextureScale = new Vector2(du, -dv / 2);//-dv/2
                    }

                    aspect *= Mathf.Abs(du / dv);
                }
                else
                {
                    _material.mainTextureOffset = Vector2.zero;
                    _material.mainTextureScale = new Vector2(1f, -1f);
                }
            }
            

            if (source.hasTracking)
            {
                const float ProjectionZ = 1.0f;
                
                //Vector2 LocalScale = new Vector2(4.0f * ProjectionZ / ProjectionScale.x, 4.0f * ProjectionZ / ProjectionScale.y);
                //Debug.Log(LocalScale);
                if (_cameraViewMeshFilter.mesh.ToString().Contains("Quad"))
                {
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
                }
                if (_cameraViewMeshFilter.mesh.ToString().Contains("Plane"))
                {
                    Vector2 ProjectionScale = GetProjectionScale(source);
                    Vector2 LocalScale = new Vector2(0.4f * ProjectionZ / ProjectionScale.x, 0.4f * ProjectionZ / ProjectionScale.y);

                    if (left)
                    {
                        _viveFrontCameraView.localScale = new Vector3(LocalScale.x, 1.0f, LocalScale.y / 2);
                    }
                    else
                    {
                        _viveFrontCameraView.localScale = new Vector3(LocalScale.x, 1.0f, -LocalScale.y / 2);
                    }
                }
                if (_cameraViewMeshFilter.mesh.ToString().Contains("Sphere"))
                {
                    Vector2 ProjectionScale = GetProjectionScale(source);
                    Vector2 LocalScale = new Vector2(1.0f * ProjectionZ / ProjectionScale.x, 1.0f * ProjectionZ / ProjectionScale.y);

                    if (left)
                    {
                        _viveFrontCameraView.localScale = new Vector3(LocalScale.x, 1.0f, LocalScale.y);
                    }
                    else
                    {
                        _viveFrontCameraView.localScale = new Vector3(LocalScale.x, 1.0f, -LocalScale.y);
                    }
                }


                var trackedCameraTransform = source.transform;

                UpdateFrontCameraTransform(trackedCameraTransform, ProjectionZ);
            }

        }

        private void UpdateFrontCameraTransform(SteamVR_Utils.RigidTransform sourceTransform, float ProjectionZ)
        {
            InputTracking.GetNodeStates(nodeStates);

            var headState = nodeStates.FirstOrDefault(node => node.nodeType == XRNode.Head);
            //位置座標を取得
            headPosition = InputTracking.GetLocalPosition(XRNode.Head);
            //headPosition = headState.TryGetPosition(out headPosition);
            //回転座標をクォータニオンで値を受け取る
            headRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
            //取得した値をクォータニオン → オイラー角に変換
            headRotation = headRotationQ.eulerAngles;

            //HMDとカメラの３軸ごとの距離
            float xDistance = Mathf.Abs(_viveFrontCameraView.position.x - headPosition.x);
            float yDistance = Mathf.Abs(_viveFrontCameraView.position.y - headPosition.y);
            float zDistance = Mathf.Abs(_viveFrontCameraView.position.z - headPosition.z);

            //
            float x = distance * Mathf.Sin(headRotation.y * Mathf.PI / 180f);
            float y = -distance * Mathf.Sin(headRotation.x * Mathf.PI / 180f);
            float z = distance * Mathf.Cos(headRotation.y * Mathf.PI / 180f); // Mathf.Sqrt(Mathf.Pow(zDistance, 2.0f) + Mathf.Pow(xDistance, 2.0f))

            //Debug.Log("HMD yRotation:" + headRotation.y);

            if (_cameraViewMeshFilter.mesh.ToString().Contains("Quad"))
            {
                _viveFrontCameraView.localPosition = sourceTransform.TransformPoint(new Vector3(0, 0, distance));
                _viveFrontCameraView.localRotation = sourceTransform.rot;
            }
            if (_cameraViewMeshFilter.mesh.ToString().Contains("Plane"))
            {
                _viveFrontCameraView.position = new Vector3(x + headPosition.x, y + headPosition.y, z + headPosition.z);
                //_viveFrontCameraView.localPosition = sourceTransform.TransformPoint(new Vector3(0, 0, ProjectionZ));
                _viveFrontCameraView.localRotation = sourceTransform.rot * Quaternion.Euler(90f, 180f, 0f);
                //_viveFrontCameraView.localRotation = sourceTransform.rot;
            }
            if (_cameraViewMeshFilter.mesh.ToString().Contains("Sphere"))
            {
                _viveFrontCameraView.position = new Vector3(x + headPosition.x, y + headPosition.y, z + headPosition.z);
                //_viveFrontCameraView.localPosition = sourceTransform.TransformPoint(new Vector3(0, 0, ProjectionZ));
                _viveFrontCameraView.localRotation = sourceTransform.rot * Quaternion.Euler(0f, -90f, 0f);
                //_viveFrontCameraView.localRotation = sourceTransform.rot;
            }
            //_viveFrontCameraView.localRotation = sourceTransform.rot;
            //_viveFrontCameraView.localRotation = Quaternion.Euler(sourceTransform.rot.x-90f, sourceTransform.rot.y, sourceTransform.rot.z);
            //_viveFrontCameraView.localRotation = Quaternion.Euler(sourceTransform.rot.x, sourceTransform.rot.y, sourceTransform.rot.z);
        }

        private void EnableSteamVRCamera()
        {
            var source = SteamVR_TrackedCamera.Source(_undistorted);
            source.Acquire();

            // カメラが認識されていなかったらdisableにする
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