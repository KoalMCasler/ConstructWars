using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    public SpellComponent spell;
    public Rigidbody2D rb;
    public GameObject umbralSelfCollision;
    public GameObject arcaneSelfCollision;
    public GameObject FireSelfCollision;
    public GameObject FireArcaneCollision;
    public GameObject FireUmbralCollision;
    public GameObject ArcaneUmbralCollision;
    public bool shotByPlayer;
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
        
        if(shotByPlayer)
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
        if(shotByPlayer)
        {
            Vector2 aimDirection = spell.player.GetComponent<PlayerController>().mousePosition - rb.position;

            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
        if(!shotByPlayer)
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
        if(shotByPlayer)
        {
            //ignores player
            if(!other.gameObject.CompareTag("Player"))
            {
                //collides with enemy
                if(other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.GetComponent<Enemy>().health -= ((float)spell.damage)* spell.player.GetComponent<PlayerController>().playerStats.DamageModifier;
                }
                //Destory self as long as not hitting the player
                Destroy(gameObject);
            }
            if(other.gameObject.CompareTag("Spell"))
            {
                SpellCollision(other.gameObject);
            }
        }
        if(!shotByPlayer)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().currentHP -= ((float)spell.damage) - spell.player.GetComponent<PlayerController>().playerStats.DamageResitance;
                Destroy(gameObject);
            }
            if(other.gameObject.CompareTag("Spell"))
            {
                SpellCollision(other.gameObject);
            }
        }
    }
    void OnDestroy()
    {
        GameObject Explosion = Instantiate(spell.ExplodePreFab, transform.position, Quaternion.identity);
        Destroy(Explosion, 12f);
    }
    void AdvnacedDestroy(GameObject collisionEffect)
    {
        GameObject Explosion = Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(Explosion, 12f);
    }

    void SpellCollision(GameObject other)
    {
        if(spell.damageType == other.GetComponent<SpellBase>().spell.damageType)
        {
            if(spell.damageType == "Arcane")
            {
                AdvnacedDestroy(arcaneSelfCollision);
                Destroy(other);
            }
            if(spell.damageType == "Fire")
            {
                AdvnacedDestroy(FireSelfCollision);
                Destroy(other);
            }
            if(spell.damageType == "Umbral")
            {
                AdvnacedDestroy(umbralSelfCollision);
                Destroy(other);
            }
        }
        else
        {
            if((spell.damageType == "Arcane" && other.GetComponent<SpellBase>().spell.damageType == "Umbral")||(spell.damageType == "Umbral" && other.GetComponent<SpellBase>().spell.damageType == "Arcane"))
            {
                AdvnacedDestroy(ArcaneUmbralCollision);
                Destroy(other);
            }
            if((spell.damageType == "Umbral" && other.GetComponent<SpellBase>().spell.damageType == "Fire")||(spell.damageType == "Fire" && other.GetComponent<SpellBase>().spell.damageType == "Umbral"))
            {
                AdvnacedDestroy(FireUmbralCollision);
                Destroy(other);
            }
            if((spell.damageType == "Arcane" && other.GetComponent<SpellBase>().spell.damageType == "Fire")||(spell.damageType == "Fire" && other.GetComponent<SpellBase>().spell.damageType == "Arcane"))
            {
                AdvnacedDestroy(FireArcaneCollision);
                Destroy(other);
            }
        }
    }
}
