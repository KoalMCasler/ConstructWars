using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altfire : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerCon;
    public float abilityDuration;
    public float abilityMaxDuration;
    public bool abilityIsActive;
    public GameObject Shield;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerCon = player.gameObject.GetComponent<PlayerController>();
        playerCon.uIManager.UpdateHUD();
    }
    void Update()
    {

        if(abilityIsActive == false)
        {
            Shield.SetActive(false);
            Debug.Log("Shield is inactive");
        }
        else if(abilityIsActive == true)
        {
            Debug.Log("Shield should be active");
            Shield.SetActive(true);
            abilityDuration -= Time.deltaTime;
            if(abilityDuration <= 0)
            {
                abilityDuration = abilityMaxDuration;
                abilityIsActive = false;
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
            }
            else if(abilityIsActive == true)
            {
                Debug.Log("ability tunred off");
                abilityIsActive = false;
            }
        }
    }
}
