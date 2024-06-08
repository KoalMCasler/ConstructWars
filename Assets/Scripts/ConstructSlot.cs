using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConstructSlot : MonoBehaviour, IDropHandler
{
    private GameObject dropped;
    public PlayerController player;
    public string slotType;
    [Header("Only for spell slots")]
    public int SpellSlotNumber; // 0-2

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            dropped = eventData.pointerDrag;
            if(dropped.gameObject.GetComponent<InventoryItem>().itemType == slotType)
            {   
                if(dropped.GetComponent<InventoryItem>().itemType == "Spell")
                {
                    player.spellArray[SpellSlotNumber] = dropped.GetComponent<InventoryItem>().Spell;
                }
                else if(dropped.GetComponent<InventoryItem>().itemType == "Core")
                {
                    player.core = dropped;
                }
                else if(dropped.GetComponent<InventoryItem>().itemType == "Utility")
                {
                    player.utility = dropped;
                }
                else if(dropped.GetComponent<InventoryItem>().itemType == "Mobility")
                {
                    player.mobility = dropped;
                }
                dropped.GetComponent<Draggable>().parentAfterDrag = transform;
            }
            else
            {
                return;
            }
        }
    }
}