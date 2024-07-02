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
}
