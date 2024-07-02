using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem item;
    public ToolTipManager tTM;
    void Start()
    {
        item = gameObject.GetComponent<InventoryItem>();
        tTM =  FindObjectOfType<ToolTipManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tTM.SetAndShowToolTip(item);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tTM.HideToolTip();
    }
}
