using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Object References
    public GameObject Player;
    public GameObject Crosshair;
    public GameObject ProjectilePrefab;
    public Transform firePoint;
    public Rigidbody2D rb;
    //Movement variables
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 10f;
    private float activeMoveSpeed;
    Vector2 moveDirection;
    //Attack/Aim Varables
    Vector2 mousePosition;
    public float ShotTimer;
    public float ShotDelay; //In seconds.
    public int fireForce;
    //Stats variables
    public int currentHP;
    public int maxHP;

    void Start()
    {
        if(maxHP <= 0)
        {
            maxHP = 400;
        }
        // Starting HP
        currentHP = maxHP;
        //Current move speed
        activeMoveSpeed = moveSpeed;
        //Deactivate Cursor
        Cursor.visible = false;
        Crosshair.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to make sure player is not dead.
        CheckStatus();
        //Countdown for shot delay
        ShotTimer -= Time.deltaTime;
        if(ShotTimer == 0)
        {
            ShotTimer = 0;
        }
        //Mouse input, Moves Crosshair to mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Crosshair.transform.position = mousePosition;
    }
    void OnMove(InputValue movementValue)
    {
        //Movement logic
        Vector2 moveVector2 = movementValue.Get<Vector2>();
        moveDirection = moveVector2;
    }
    private void FixedUpdate()
    {
        // this moves the player indepented from aim direction
        rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        
        // This horrible looking math is how the character stays looking at the mouse point
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;   
    }
    void OnFire()
    {
        //Lets shot only take place if cooldown has happened
        if(ShotTimer <= 0)
        {
            //Resets shot timer
            ShotTimer = ShotDelay;
            //Creates and fires projectile
            GameObject Projectile = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
            Projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce);
        }
        else
        {
            //Debug to tell you that cooldown is not ready
            Debug.Log("Wait for shot cooldown.");
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //Hurts player if they touch an enemy. 
        if(other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
    void TakeDamage()
    {
        //Debug take damage funtion
        currentHP -= 100;
    }
    void CheckStatus()
    {
        if(currentHP <= 0)
        {
            Debug.Log("You Died");
            SceneManager.LoadScene(0);
        }
    }
    public float ReturnCurrentHP()
    {
        return currentHP;
    }
    public float ReturnMaxHP()
    {
        return maxHP;
    }
}
