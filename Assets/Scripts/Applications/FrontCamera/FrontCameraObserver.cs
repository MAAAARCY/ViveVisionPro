using UnityEngine;

namespace Applications.FrontCamera
{
    public class FrontCameraObserver : MonoBehaviour
    {
        [SerializeField]
        private GameObject ViveFrontCameraScreen;

        private bool isActive = true;

        void Update()
        {
            if (FrontCameraInfo.FrontCameraIsActive)
            {
                if (isActive)
                {
                    ViveFrontCameraScreen.SetActive(true);
                    isActive = false;
                }
            }
            else
            {
                ViveFrontCameraScreen.SetActive(false);
                isActive = true;
            }
        }
    }
}