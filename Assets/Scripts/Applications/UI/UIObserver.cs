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

        [SerializeField]
        private TextMeshProUGUI SettingsMenuLabel;

        ///Font Selector
        [SerializeField]
        private FontIconSelector UseHandIcon;

        [SerializeField]
        private FontIconSelector ScreenFollowsIcon;

        [SerializeField]
        private GameObject Settings;

        private static bool switchActive = false;

        private void Start()
        {
            //ScreenSizeChangeBar.value = 0.5f;
            Settings.SetActive(false);
        }

        private void Update()
        {
            ScreenSizeChangeBar.value = ScreenSizeChanger.SliderValue;
            EnableVirtualScreenLabel.text = EnableVirtualScreen.Label;
            EnableFrontCameraLabel.text = EnableFrontCamera.Label;
            UseHandLabel.text = UseHand.Label;
            UseHandIcon.CurrentIconName = UseHand.IconName;
            SettingsMenuLabel.text = SettingsMenu.ScreenFollowsLabel;
            ScreenFollowsIcon.CurrentIconName = SettingsMenu.ScreenFollowsIconName;

            Debug.Log(ScreenFollowsIcon.CurrentIconName);

            if (switchActive)
            {
                Settings.SetActive(SettingsMenu.IsOpen);
                switchActive = false;
            }
        }

        public static bool SwitchActive
        {
            set
            {
                switchActive = value;
            }
        }
    }

}