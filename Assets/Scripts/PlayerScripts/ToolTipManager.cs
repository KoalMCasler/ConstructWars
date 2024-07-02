using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI dMText;
    public TextMeshProUGUI dRText;
    public TextMeshProUGUI cDRText;
    public TextMeshProUGUI mSText;
    public TextMeshProUGUI luckText;
    public void SetAndShowToolTip(InventoryItem item)
    {
        transform.position = Input.mousePosition;
        this.gameObject.SetActive(true);
        itemName.text = item.itemName;
        if(item.itemType == "Origin")
        {
            hpText.text = string.Format("Base HP: {0}",item.HPMod);
            dMText.text = string.Format("Base DM: {0}",item.DMMod);
            dRText.text = string.Format("Base DR: {0}",item.DRMod);
            cDRText.text = string.Format("Base CDR: {0}",item.CoolDownReduction);
            mSText.text = string.Format("Base MS: {0}",item.MSMod);
            luckText.text = string.Format("Base Luck: {0}%",item.LuckMod);
        }
        else if(item.itemType == "Spell")
        {
            hpText.text = string.Format("Base Damage = {0}",item.Spell.GetComponent<SpellBase>().spell.damage);
            dRText.text = string.Format("Shot life = {0}/s",item.Spell.GetComponent<SpellBase>().spell.maxShotLife);
            mSText.text = string.Format("Shot Delay = {0}/s",item.Spell.GetComponent<SpellBase>().spell.shotDelay);
            dMText.text = string.Format("Shot Speed = {0}m/s",item.Spell.GetComponent<SpellBase>().spell.shotSpeed);
            cDRText.text = string.Empty;
            luckText.text = string.Format("Damage Type = {0}",item.Spell.GetComponent<SpellBase>().spell.damageType);
        }
        else if(item.itemType == "Heart")
        {
            hpText.text = string.Format("HP mod: {0}",item.HPMod);
            dMText.text = string.Format("DM mod: {0}",item.DMMod);
            dRText.text = string.Format("DR mod: {0}",item.DRMod);
            cDRText.text = string.Format("CDR mod: {0}",item.CoolDownReduction);
            mSText.text = string.Format("MS mod: {0}",item.MSMod);
            luckText.text = string.Format("Luck mod: {0}%",item.LuckMod);
        }
        else if(item.itemType == "Utility")
        {
            hpText.text = string.Format("HP mod: {0}",item.HPMod);
            dMText.text = string.Format("DM mod: {0}",item.DMMod);
            dRText.text = string.Format("DR mod: {0}",item.DRMod);
            cDRText.text = string.Format("CDR mod: {0}",item.CoolDownReduction);
            mSText.text = string.Format("MS mod: {0}",item.MSMod);
            luckText.text = string.Format("Luck mod: {0}%",item.LuckMod);
        }
        else if(item.itemType == "Mobility")
        {
            hpText.text = string.Format("HP mod: {0}",item.HPMod);
            dMText.text = string.Format("DM mod: {0}",item.DMMod);
            dRText.text = string.Format("DR mod: {0}",item.DRMod);
            cDRText.text = string.Format("CDR mod: {0}",item.CoolDownReduction);
            mSText.text = string.Format("MS mod: {0}",item.MSMod);
            luckText.text = string.Format("Luck mod: {0}%",item.LuckMod);
        }
    }
    public void HideToolTip()
    {
        this.gameObject.SetActive(false);
        itemName.text = string.Empty;
        hpText.text = string.Empty;
        dMText.text = string.Empty;
        dRText.text = string.Empty;
        cDRText.text = string.Empty;
        mSText.text = string.Empty;
        luckText.text = string.Empty; 
    }
}
