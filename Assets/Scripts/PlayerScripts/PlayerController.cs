using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [Header("Object Referances")]
    public UIManager uIManager;
    public GameManager gameManager;
    public GameObject Crosshair;
    public Transform firePoint;
    public Rigidbody2D rb;
    public Stats playerStats;
    [Header("Player Build")]
    public GameObject core;
    public GameObject utility;
    public GameObject mobility;
    public GameObject[] spellArray = new GameObject[3];
    [Header("Movement variables")]
    //public float moveSpeed = 5f;
    //public float maxMoveSpeed = 10f;
    private float activeMoveSpeed;
    Vector2 moveDirection;
    [Header("Attack/Aim Varables")]
    public int spellIndex;
    public Vector2 mousePosition;
    public float ShotTimer;
    public float ShotDelay; //In seconds.
    public int fireForce;
    [Header("Stats variables")]
    public int currentHP;
    //public int maxHP;

    void Awake()
    {
        // Starting HP
        currentHP = playerStats.maxHP;
        //Current move speed
        activeMoveSpeed = playerStats.moveSpeed;
        spellIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ShotDelay = spellArray[spellIndex].GetComponent<SpellBase>().spell.shotDelay;
        //Checks to make sure player is not dead.
        CheckStatus();
        //Countdown for shot delay
        ShotTimer += Time.deltaTime;
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
        //Prevents player for fiering when pasued. 
        if(gameManager.gameState == GameManager.GameState.Gameplay)
        {
            //Lets shot only take place if cooldown has happened
            if(ShotTimer >= ShotDelay)
            {
                //Resets shot timer
                ShotTimer = 0;
                //Creates and fires projectile
                GameObject Projectile = Instantiate(spellArray[spellIndex], firePoint.position, firePoint.rotation);
                Projectile.GetComponent<SpellBase>().spell.shotByPlayer = true;
                Projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce);
            }
            else
            {
                //Debug to tell you that cooldown is not ready
                Debug.Log("Wait for shot cooldown.");
            }
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
        return playerStats.maxHP;
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
    void OnPause()
    {
        if(gameManager.gameState == GameManager.GameState.Paused)
        {
            //isPaused = false;
            gameManager.gameState = GameManager.GameState.Gameplay;
            gameManager.ChangeGameState();
        }
        if(gameManager.gameState == GameManager.GameState.Gameplay)
        {
            //isPaused = true;
            gameManager.gameState = GameManager.GameState.Paused;
            gameManager.ChangeGameState();
        }
    }

    public void CalculateStats()
    {
        
    }
}
