using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Text toolTipText;

    [SerializeField] private GameObject toolTipParent;

    [SerializeField]private string toolTipString;
    private void OnEnable()
    {
         var eventTrigger = toolTipParent.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        entry.callback.AddListener((eventData) => { DisplayTooltip(toolTipParent.transform.position); });
        eventTrigger.triggers.Add(entry);

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;

        exit.callback.AddListener((eventData) => { toolTip.SetActive(false); });
        eventTrigger.triggers.Add(exit);

    }

    private void DisplayTooltip(Vector3 position)
    {
        var rectTransform = toolTipText.GetComponent<RectTransform>();
        toolTipText.text = toolTipString;
        toolTip.transform.position = position + new Vector3(rectTransform.rect.width, 0);
        toolTip.SetActive(true);
    }
}
