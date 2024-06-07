using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    public SpellComponent spell;
    public Rigidbody2D rb;
    void Start()
    {
        spell.player = GameObject.FindWithTag("Player");
        spell.Crosshair = spell.player.GetComponent<PlayerController>().Crosshair;
        if(spell.maxShotLife <= 0)
        {
            spell.maxShotLife = 6;
        }
        if(spell.shotLife != spell.maxShotLife)
        {
            spell.shotLife = spell.maxShotLife;
        }
        Destroy(gameObject, spell.shotLife);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(spell.shotByPlayer)
        {
            spell.distance = Vector2.Distance(transform.position,spell.Crosshair.transform.position);
            Vector2 direction = spell.Crosshair.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position,spell.Crosshair.transform.position,spell.shotSpeed * Time.deltaTime);
        }
        else
        {
            spell.distance = Vector2.Distance(transform.position,spell.player.transform.position);
            Vector2 direction = spell.player.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position,spell.player.transform.position,spell.shotSpeed * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        //Makes shot face crosshair.
        if(spell.shotByPlayer)
        {
            Vector2 aimDirection = spell.player.GetComponent<PlayerController>().mousePosition - rb.position;

            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
        if(!spell.shotByPlayer)
        {
            Vector2 aimDirection = new Vector2();
            aimDirection.y = spell.player.transform.position.y - rb.position.y;
            aimDirection.x = spell.player.transform.position.x - rb.position.x;

            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(spell.shotByPlayer)
        {
            //ignores player
            if(!other.gameObject.CompareTag("Player"))
            {
                //collides with enemy
                if(other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.GetComponent<Enemy>().health -= spell.damage;
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
        if(!spell.shotByPlayer)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().currentHP -= spell.damage;
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
        
        GameObject Explosion = Instantiate(spell.ExplodePreFab, transform.position, Quaternion.identity);
        Destroy(Explosion, 1f);
    }
    void ShotByPlayerCheck()
    {
        if(spell.shotByPlayer)
        {
            spell.player.GetComponent<Collider2D>().excludeLayers = spell.playerLayer;
        }
        else
        {
            spell.player.GetComponent<Collider2D>().excludeLayers = spell.Default;
        }
    }
}
