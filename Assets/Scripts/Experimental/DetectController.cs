using UnityEngine;
using UnityEngine.EventSystems;

public class DetectController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] TargetGameObjects;

    private void Awake()
    {
        foreach (var target in TargetGameObjects)
        {
            var et = target.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener(v => Click(v));
            et.triggers.Add(entry);
        }
    }

    private void Click(BaseEventData eventData)
    {
        var pointerEventData = (PointerEventData)eventData;
        Debug.Log("hogehogehoge");
        Debug.Log(pointerEventData.pointerEnter);
    }
}
