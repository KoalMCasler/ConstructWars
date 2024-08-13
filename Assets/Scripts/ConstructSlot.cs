using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConstructSlot : MonoBehaviour, IDropHandler
{
    private GameObject dropped;
    private GameObject currentItem;
    public PlayerController player;
    public string slotType;
    [Header("Only for spell slots")]
    public int SpellSlotNumber; // 0-2

    public void OnDrop(PointerEventData eventData)
    {
        dropped = eventData.pointerDrag;
        if(transform.childCount == 0 && dropped.GetComponent<InventoryItem>().itemType == slotType)
        {
            dropped.GetComponent<Draggable>().isEquipped = true;
            currentItem = dropped;
            CheckCurrentItem();  
            currentItem.GetComponent<Draggable>().parentAfterDrag = transform; 
        }
        if(transform.childCount == 1 && dropped.GetComponent<InventoryItem>().itemType == currentItem.GetComponent<InventoryItem>().itemType)
        {
            //Swaps items if they match type. 
            //Debug.Log("Swaping Items");
            currentItem.GetComponent<Draggable>().isEquipped = false;
            dropped.GetComponent<Draggable>().isEquipped = true;
            currentItem.GetComponent<Draggable>().parentAfterDrag = dropped.GetComponent<Draggable>().parentAfterDrag;
            currentItem.GetComponent<Draggable>().OnEndDrag(eventData);
            currentItem = dropped;
            CheckCurrentItem();
            currentItem.GetComponent<Draggable>().parentAfterDrag = transform;
        }
    }
    public void CheckCurrentItem()
    {
        if(currentItem.gameObject.GetComponent<InventoryItem>().itemType == slotType)
        {   
            if(currentItem.GetComponent<InventoryItem>().itemType == "Spell")
            {
                player.spellArray[SpellSlotNumber] = currentItem.GetComponent<InventoryItem>().Spell;
            }
            else if(currentItem.GetComponent<InventoryItem>().itemType == "Origin")
            {                    
                player.origin = currentItem.GetComponent<InventoryItem>();
                player.CalculateStats();
            }
            else if(dropped.GetComponent<InventoryItem>().itemType == "Heart")
            {                    
                player.heart = currentItem.GetComponent<InventoryItem>();
                player.CalculateStats();
            }
            else if(dropped.GetComponent<InventoryItem>().itemType == "Utility")
            {
                player.utility = currentItem.GetComponent<InventoryItem>();
                player.CalculateStats();
            }
            else if(dropped.GetComponent<InventoryItem>().itemType == "Mobility")
            {
                player.mobility = currentItem.GetComponent<InventoryItem>();
                player.CalculateStats();
            }
        }
        else
        {
            return;
        }
    }
}