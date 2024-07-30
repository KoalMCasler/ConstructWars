using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [Header("Item Type")]
    public string itemType;
    public string itemName;
    [Header("If item is an Origin")]
    public GameObject origin;
    public string originType;
    [Header("If item is Utility")]
    public int LuckMod;
    public float CoolDownReduction;
    public float DMMod;
    [Header("If item is Heart")]
    public int HPMod;
    public int DRMod;
    [Header("If item is Mobility")]
    public float MSMod;
    [Header("If Item is a spell")]
    public GameObject Spell;

    void Start()
    {
        if(itemName == null)
        {
            Debug.Log("Item name is missing!");
        }
        if(itemType == null)
        {
            Debug.Log("Item type is missing!");
        }
        if(itemType == "Origin" && origin == null)
        {
            Debug.Log("origin is missing!");
        }
        if(itemType == "Origin" && originType == null)
        {
            Debug.Log("Origin type is missing!");
        }
        if(itemType == "Utility" && LuckMod == 0)
        {
            Debug.Log("Luck mod is missing!");
        }
        if(itemType == "Utility" && CoolDownReduction == 0)
        {
            Debug.Log("Cool Down Reduction is missing!");
        }
        if(itemType == "Utility" && DMMod == 0)
        {
            Debug.Log("Damage mod is missing!");
        }
        if(itemType == "Heart" && HPMod == 0)
        {
            Debug.Log("HP mod is missing!");
        }
        if(itemType == "Heart" && DRMod == 0)
        {
            Debug.Log("Damage Resistance mod is missing!");
        }
        if(itemType == "Mobility" && MSMod == 0)
        {
            Debug.Log("move speed mod is missing!");
        }
        if(itemType == "Spell" && Spell == null)
        {
            Debug.Log("Spell is missing!");
        }
    }
}
