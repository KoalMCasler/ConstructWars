using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [Header("Object Referances")]
    public GameObject Player;
    public GameObject Crosshair;
    public GameObject ProjectilePrefab;
    public Transform firePoint;
    public Rigidbody2D rb;
    [Header("Movement variables")]
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 10f;
    private float activeMoveSpeed;
    Vector2 moveDirection;
    [Header("Attack/Aim Varables")]
    public GameObject[] spellArray = new GameObject[3];
    public int spellIndex;
    Vector2 mousePosition;
    public float ShotTimer;
    public float ShotDelay; //In seconds.
    public int fireForce;
    [Header("Stats variables")]
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
        spellIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ShotDelay = spellArray[spellIndex].GetComponent<Spell>().shotDelay;
        //Checks to make sure player is not dead.
        CheckStatus();
        //Countdown for shot delay
        ShotTimer += Time.deltaTime;
        if(ShotTimer > ShotDelay)
        {
            ShotTimer = ShotDelay;
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
        if(ShotTimer >= ShotDelay)
        {
            //Resets shot timer
            ShotTimer = 0;
            //Creates and fires projectile
            GameObject Projectile = Instantiate(spellArray[spellIndex], firePoint.position, firePoint.rotation);
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
    void OnLook(InputValue lookValue)
    {
        //mousePosition += lookValue.Get<Vector2>();
    }
    public float ReturneShotDelay()
    {
        return ShotDelay;
    }
    public float ReturneShotTimer()
    {
        return ShotTimer;
    }
    void OnNextWeapon()
    {
        spellIndex += 1;
        if(spellIndex > 2)
        {
            spellIndex = 0;
        }
    }
    void OnPrevWeapon()
    {
        spellIndex -= 1;

        if(spellIndex < 0)
        {
            spellIndex = 2;
        }
    }
}
