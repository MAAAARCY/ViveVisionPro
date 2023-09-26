using UnityEngine;
using UnityEngine.UI;

using TMPro;
using MixedReality.Toolkit.UX;

namespace Applications.UI
{
    public class UIObserver : MonoBehaviour
    {
        ///Slider
        [SerializeField]
        private UnityEngine.UI.Slider ScreenSizeChangeBar;

        ///TextMeshPro in Button
        [SerializeField]
        private TextMeshProUGUI EnableVirtualScreenLabel;
        
        [SerializeField]
        private TextMeshProUGUI EnableFrontCameraLabel;

        [SerializeField]
        private TextMeshProUGUI UseHandLabel;

        ///Font Selector
        [SerializeField]
        private FontIconSelector UseHandIcon;

        void Start()
        {
            //ScreenSizeChangeBar.value = 0.5f;
        }

        void Update()
        {
            ScreenSizeChangeBar.value = ScreenSizeChanger.SliderValue;
            EnableVirtualScreenLabel.text = EnableVirtualScreen.Label;
            EnableFrontCameraLabel.text = EnableFrontCamera.Label;
            UseHandLabel.text = UseHand.Label;
            UseHandIcon.CurrentIconName = UseHand.IconName;
        }
    }

}