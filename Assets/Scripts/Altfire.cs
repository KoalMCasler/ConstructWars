using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altfire : MonoBehaviour
{
    [Header("Managers/Objects")]
    public GameObject player;
    public UIManager uIManager;
    public PlayerController playerCon;
    public GameObject Shield;
    [Header("Variables")]
    public float abilityDuration;
    public float abilityMaxDuration;
    [Header("clockwork altfire")]
    public float clockworkAbilityMaxTime;
    public bool abilityIsActive;
    [Header("elemental altfire")]
    public float elementalAbilityMaxTime;
    private float dashCounter;
    public float dashSpeed;
    public float dashLength;
    [Header("spiritual altfire")]
    public float spiritualAbilityMaxTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerCon = FindObjectOfType<PlayerController>();
        uIManager = FindObjectOfType<UIManager>();
        uIManager.UpdateHUD();
    }
    void Update()
    {
        if(abilityIsActive == true)
        {
            //Debug.Log("Shield should be active");
            abilityDuration -= Time.deltaTime;
            if(abilityDuration <= 0)
            {
                abilityDuration = abilityMaxDuration;
                abilityIsActive = false;
            }
        }
        if(abilityIsActive == false)
        {
            Shield.SetActive(false);
        }
        if(playerCon.origin != null)
        {
            if(playerCon.origin.originType == "Clockwork")
            {
                
            }
            else if(playerCon.origin.originType != "Clockwork")
            {
                Shield.SetActive(false);
            }
            if(playerCon.origin.originType == "Elemental")
            {
                abilityDuration -= Time.deltaTime;
                if(abilityDuration <= 0)
                {
                    abilityDuration = 0;
                }
                if (dashCounter > 0)
                {
                    dashCounter -= Time.deltaTime;
                    abilityDuration = abilityMaxDuration;
                    if (dashCounter <= 0) 
                    {
                        playerCon.activeMoveSpeed = playerCon.playerStats.moveSpeed;
                        player.GetComponent<CircleCollider2D>().enabled = true;
                    }
                }
            }
            if(playerCon.origin.originType == "Umbral")
            {
                
            }
        }
    }
    public void Activate()
    {
        
        if(playerCon.origin.originType == "Clockwork")
        {
            if(abilityIsActive == false)
            {
                Debug.Log("ability tunred on");
                abilityIsActive = true;
                Shield.SetActive(true);
            }
            else if(abilityIsActive == true)
            {
                Debug.Log("ability tunred off");
                abilityIsActive = false;
                Shield.SetActive(false);
            }
        }
        
        else if(playerCon.origin.originType == "Elemental")
        {
            if(abilityDuration <= 0 && dashCounter <= 0)
            {
                playerCon.activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                player.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
    public void ArenaStart()
    {
        abilityIsActive = false;
        Shield.SetActive(false);
        if(playerCon.origin.originType == "Clockwork")
        {
            abilityMaxDuration = clockworkAbilityMaxTime;
        }
        if(playerCon.origin.originType == "Elemental")
        {
            abilityMaxDuration = elementalAbilityMaxTime;
        }
        if(playerCon.origin.originType == "Spiritual")
        {
            abilityMaxDuration = spiritualAbilityMaxTime;
        }
        uIManager.UpdateHUD();
    }
}
