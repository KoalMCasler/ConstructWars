using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
   void Start()
    {
        spellName = "Fireball";
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
        //this makes the shot follow the mouse.
        distance = Vector2.Distance(transform.position,Crosshair.transform.position);
        Vector2 direction = Crosshair.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position,Crosshair.transform.position,shotSpeed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //ignores player
        if(!other.gameObject.CompareTag("Player"))
        {
            //collides with enemy
            if(other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.SetActive(false);
            }
            //Destory self as long as not hitting the player
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        // instantiating and destroying explosion prefab
        
        GameObject Explosion = Instantiate(ExplodePreFab, transform.position, Quaternion.identity);
        Destroy(Explosion, 1f);
    }
}
