using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeBolt : Spell
{
    public Rigidbody2D rb;
    void Start()
    {
        spellName = "Shade Bolt";
        player = GameObject.FindWithTag("Player");
        Crosshair = player.GetComponent<PlayerController>().Crosshair;
        if(maxShotLife <= 0)
        {
            maxShotLife = 6;
        }
        if(shotLife <= maxShotLife)
        {
            shotLife = maxShotLife;
        }
        Destroy(gameObject, shotLife);
    }

    // Update is called once per frame
    void Update()
    {
        ShotByPlayerCheck();
        if(shotByPlayer)
        {
            distance = Vector2.Distance(transform.position,Crosshair.transform.position);
            Vector2 direction = Crosshair.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position,Crosshair.transform.position,shotSpeed * Time.deltaTime);
        }
        //this makes the shot follow the mouse.
    }
    void FixedUpdate()
    {
        if(shotByPlayer)
        {
            Vector2 aimDirection = player.GetComponent<PlayerController>().mousePosition - rb.position;

            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
        //Makes shot face crosshair.
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(shotByPlayer)
        {
            //ignores player
            if(!other.gameObject.CompareTag("Player"))
            {
                //collides with enemy
                if(other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.GetComponent<Enemy>().health -= damage;
                }
                //Destory self as long as not hitting the player
                Destroy(gameObject);
            }
            if(other.gameObject.CompareTag("Spell"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
        if(!shotByPlayer)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().currentHP -= damage;
                Destroy(gameObject);
            }
            if(other.gameObject.CompareTag("Spell"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }
    void OnDestroy()
    {
        // instantiating and destroying explosion prefab
        
        GameObject Explosion = Instantiate(ExplodePreFab, transform.position, Quaternion.identity);
        Destroy(Explosion, 1f);
    }
    void ShotByPlayerCheck()
    {
        if(shotByPlayer)
        {
            player.GetComponent<Collider2D>().excludeLayers = playerLayer;
        }
        else
        {
            player.GetComponent<Collider2D>().excludeLayers = Default;
        }
    }
}
