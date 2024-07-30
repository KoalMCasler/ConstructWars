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

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            dropped = eventData.pointerDrag;
            dropped.GetComponent<Draggable>().isEquipped = true;
            if(dropped.gameObject.GetComponent<InventoryItem>().itemType == slotType)
            {   

                if(dropped.GetComponent<InventoryItem>().itemType == "Spell")
                {
                    player.spellArray[SpellSlotNumber] = dropped.GetComponent<InventoryItem>().Spell;
                }
                else if(dropped.GetComponent<InventoryItem>().itemType == "Origin")
                {
                    player.origin = dropped.GetComponent<InventoryItem>();
                    player.CalculateStats();
                }
                else if(dropped.GetComponent<InventoryItem>().itemType == "Heart")
                {
                    player.heart = dropped.GetComponent<InventoryItem>();
                    player.CalculateStats();
                }
                else if(dropped.GetComponent<InventoryItem>().itemType == "Utility")
                {
                    player.utility = dropped.GetComponent<InventoryItem>();
                    player.CalculateStats();
                }
                else if(dropped.GetComponent<InventoryItem>().itemType == "Mobility")
                {
                    player.mobility = dropped.GetComponent<InventoryItem>();
                    player.CalculateStats();
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