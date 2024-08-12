using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private GameObject dropped;

    private void Start()
    {   

    }

    public void OnDrop(PointerEventData eventData)
    {
        dropped = eventData.pointerDrag;
        if(transform.childCount == 0)
        {
            dropped.GetComponent<Draggable>().parentAfterDrag = transform;
        }
    }
}