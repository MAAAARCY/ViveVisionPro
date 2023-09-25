using UnityEngine;

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

namespace Applications.UI
{
    public class RegisterInputController : MonoBehaviour, IMixedRealitySourceStateHandler
    {
        [SerializeField]
        private GameObject InputController;

        private void OnEnable()
        {
            //CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(this);
            CoreServices.InputSystem?.PushFallbackInputHandler(InputController);
        }

        private void OnDisable()
        {
            //CoreServices.InputSystem?.UnregisterHandler<IMixedRealitySourceStateHandler>(this);
            CoreServices.InputSystem?.PopFallbackInputHandler();
        }

        // IMixedRealitySourceStateHandler interface
        public void OnSourceDetected(SourceStateEventData eventData)
        {
            /*
            var hand = eventData.Controller as IMixedRealityHand;

            // Only react to articulated hand input sources
            if (hand != null)
            {
                Debug.Log("Source detected: " + hand.ControllerHandedness);
            }
            */
            Debug.Log(eventData.Controller);
        }

        public void OnSourceLost(SourceStateEventData eventData)
        {
            /*
            var hand = eventData.Controller as IMixedRealityHand;

            // Only react to articulated hand input sources
            if (hand != null)
            {
                Debug.Log("Source lost: " + hand.ControllerHandedness);
            }
            */
            Debug.Log(eventData.Controller);
        }
    }
}
