using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject player;
    public GameObject Crosshair;
    private float distance;
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Crosshair = GameObject.FindWithTag("Crosshair");
        speed = player.GetComponent<PlayerController>().fireForce;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position,Crosshair.transform.position);
        Vector2 direction = Crosshair.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position,Crosshair.transform.position,speed * Time.deltaTime);
    }
}
