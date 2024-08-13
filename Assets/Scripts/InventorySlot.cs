using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private GameObject dropped;
    private GameObject currentItem;
    private void Start()
    {   
        if(transform.childCount == 1)
        {
            currentItem = this.gameObject.transform.GetChild(0).gameObject;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        dropped = eventData.pointerDrag;
        if(transform.childCount == 0)
        {
            currentItem = dropped;
            dropped.GetComponent<Draggable>().parentAfterDrag = transform;
        }
        if(transform.childCount == 1 && dropped.GetComponent<Draggable>().parentAfterDrag.GetComponent<ConstructSlot>() == null)
        {
            currentItem.GetComponent<Draggable>().isEquipped = false;
            dropped.GetComponent<Draggable>().isEquipped = true;
            currentItem.GetComponent<Draggable>().parentAfterDrag = dropped.GetComponent<Draggable>().parentAfterDrag;
            currentItem.GetComponent<Draggable>().OnEndDrag(eventData);
            //currentItem.GetComponent<Draggable>().parentAfterDrag.GetComponent<ConstructSlot>().CheckCurrentItem();
            currentItem = dropped;
            currentItem.GetComponent<Draggable>().parentAfterDrag = transform;
        }
    }
}