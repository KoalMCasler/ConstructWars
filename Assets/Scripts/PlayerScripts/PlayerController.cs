using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    
    [Header("Object References")]
    [SerializeField]
    private UIManager uIManager;
    [SerializeField]
    private GameManager gameManager;
    public GameObject Crosshair;
    public GameObject firePoint;
    private Rigidbody2D rb;
    public GameObject mainCam;
    public Stats playerStats;
    [Header("Player Build")]
    public GameObject Body;
    public InventoryItem origin;
    public InventoryItem heart;
    public InventoryItem utility;
    public InventoryItem mobility;
    public GameObject[] spellArray = new GameObject[3];
    public AltFire altFire;
    [Header("Movement variables")]
    //public float moveSpeed = 5f;
    //public float maxMoveSpeed = 10f;
    public float activeMoveSpeed;
    private Vector2 moveDirection;
    [Header("Attack/Aim Variables")]
    public int spellIndex;
    public Vector2 mousePosition;
    private float ShotTimer;
    public float ShotDelay; //In seconds.
    public int fireForce;
    [Header("Stats variables")]
    public float currentHP;
    //public int maxHP;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            enabled = false;
            altFire.enabled = false;
            mainCam.SetActive(false);
            this.GetComponent<PlayerInput>().enabled = false;
            return;
        }
    }
    void Awake()
    {
        // Starting HP
        currentHP = playerStats.maxHP;
        //Current move speed
        activeMoveSpeed = playerStats.moveSpeed;
        spellIndex = 0;
        firePoint = GameObject.FindWithTag("SpellPoint");
        CalculateStats();
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
        // this moves the player independent from aim direction
        rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        
        // This horrible looking math is how the character stays looking at the mouse point
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;   
    }
    void OnFire()
    {
        //Prevents player for firing when paused. 
        if(gameManager.gameState == GameManager.GameState.Gameplay)
        {
            firePoint = GameObject.FindWithTag("SpellPoint");
            //Lets shot only take place if cool down has happened
            if(ShotTimer >= ShotDelay)
            {
                //Resets shot timer
                ShotTimer = 0;
                //Creates and fires projectile
                GameObject Projectile = Instantiate(spellArray[spellIndex], firePoint.transform.position, firePoint.transform.rotation);
                Projectile.GetComponent<SpellBase>().shotByPlayer = true;
                Projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * fireForce);
            }
            else
            {
                //Debug to tell you that cool down is not ready
                Debug.Log("Wait for shot cool down.");
            }
        }
    }
    void OnAltFire()
    {
        altFire.Activate();
        Debug.Log("Alt fire triggered");
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
        //Debug take damage function
        currentHP -= 100;
    }
    void CheckStatus()
    {
        if(currentHP <= 0 && gameManager.gameState == GameManager.GameState.Gameplay)
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
    public float ReturnShotDelay()
    {
        return ShotDelay;
    }
    public float ReturnShotTimer()
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
        if(origin != null)
        {
            playerStats.maxHPBase = origin.HPMod;
            playerStats.DamageResistanceBase = origin.DRMod;
            playerStats.DamageModifierBase = origin.DMMod;
            playerStats.SpellChargeRateBase = origin.CoolDownReduction;
            playerStats.LuckBase = origin.LuckMod;
            playerStats.moveSpeedBase = origin.MSMod;
            
            //Makes sure body matches origin. 
            Destroy(Body);
            Body = Instantiate(origin.origin, this.transform);
            firePoint = GameObject.FindWithTag("SpellPoint");
            gameManager.TogglePlayerSprite(false);
        }
        if(heart != null)
        {
            playerStats.maxHP = playerStats.maxHPBase + heart.HPMod;
            playerStats.DamageResistance = playerStats.DamageResistanceBase + heart.DRMod;
        }
        else
        {
            playerStats.maxHP = playerStats.maxHPBase;
            playerStats.DamageResistance = playerStats.DamageResistanceBase;
        }
        if(utility != null)
        {
            playerStats.DamageModifier = playerStats.DamageModifierBase + utility.DMMod;
            playerStats.SpellChargeRate = playerStats.SpellChargeRateBase + utility.CoolDownReduction;
            playerStats.Luck = playerStats.LuckBase + utility.LuckMod;
        }
        else
        {
            playerStats.DamageModifier = playerStats.DamageModifierBase;
            playerStats.SpellChargeRate = playerStats.SpellChargeRateBase;
            playerStats.Luck = playerStats.LuckBase;
        }
        if(mobility != null)
        {
            playerStats.moveSpeed = playerStats.moveSpeedBase + mobility.MSMod;
        }
        else
        {
            playerStats.moveSpeed = playerStats.moveSpeedBase;
        }
        currentHP = playerStats.maxHP;
    }

    void ResetStats() //Used for a clean start
    {
        playerStats.maxHPBase = 0;
        playerStats.maxHP = 0;
        playerStats.DamageResistance = 0;
        playerStats.DamageResistanceBase = 0;
        playerStats.DamageModifier = 0;
        playerStats.DamageModifierBase = 0;
        playerStats.SpellChargeRate = 0;
        playerStats.SpellChargeRateBase = 0;
        playerStats.Luck = 0;
        playerStats.LuckBase = 0;
        playerStats.moveSpeed = 0;
        playerStats.moveSpeedBase = 0;
    }
}
