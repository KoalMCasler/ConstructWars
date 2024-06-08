using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Object Referances")]
    public GameObject spell;
    public Transform projectilePos;
    public GameObject player;
    public GameObject Self;
    public GameObject targetHUD;
    public Slider healthBar;
    [Header("Attack Varaibles")]
    public int attackRange;
    public int shotDelay;
    public float shotTime;
    [Header("Stats")]
    public int damage;
    public float health;
    public int maxHealth;
    public int moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if(shotTime <= 0)
        {
            shotTime = shotDelay;
        }
        player= GameObject.FindWithTag("Player");
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        CheckStatus();
        shotTime -= Time.deltaTime;
        if(shotTime <= 0)
        {
            if(Vector3.Distance(player.transform.position, gameObject.transform.position) < attackRange)
            {
                Shoot();
            }
            shotTime = shotDelay;
        }
        
    }
    void Shoot()
    {
        GameObject projectile = Instantiate(spell, projectilePos.position, Quaternion.identity);
        projectile.GetComponent<SpellBase>().spell.shotByPlayer = false;
    }
    void CheckStatus()
    {
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    void OnDisable()
    {
        targetHUD.SetActive(false);
    }
}
