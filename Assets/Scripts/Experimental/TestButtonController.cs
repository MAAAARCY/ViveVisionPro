using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vive;

public class TestButtonController : MonoBehaviour, IEventSystemHandler//IPointerEnterHandler
{
    [SerializeField] private ViveLaserPointer LaserPointer;
    [SerializeField] private Button TestButton;

    private bool isActive = true;

    private void Start()
    {
        TestButton.onClick.AddListener(OnPointerClick);
    }

    private void Update()
    {
        if (LaserPointer.GetPointerInCollider() && ViveController.InteractRightUIState)
        {
            if (isActive)
            {
                ExecuteEvents.Execute(TestButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                //TestButton.onClick.Invoke();
                //OnPointerClick();
                isActive = false;
            }
        }
        else
        {
            isActive = true;
        }
    }

    public void OnPointerClick()
    {
        Debug.Log("On");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
    }
}
