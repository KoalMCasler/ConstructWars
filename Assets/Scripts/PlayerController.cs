using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variables
    public GameObject Player;
    public GameObject Crosshair;
    public GameObject ProjectilePrefab;
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 10f;
    public Rigidbody2D rb;
    public int currentHP;
    public int maxHP = 5;
    public int startHP = 3;
    private float activeMoveSpeed;
    Vector2 moveDirection;
    Vector2 mousePosition;
    public float ShotTimer;
    public int ShotDelay; //In seconds.
    public Transform firePoint;
    public int fireForce;

    void Start()
    {
        // Starting HP
        currentHP = startHP;
        activeMoveSpeed = moveSpeed;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShotTimer -= Time.deltaTime;
        if(ShotTimer == 0)
        {
            ShotTimer = 0;
        }
        //float movementX = Input.GetAxisRaw("Horizontal");
        //float movementY = Input.GetAxisRaw("Vertical");
        //moveDirection = new Vector2(movementX, movementY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Crosshair.transform.position = mousePosition;
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 moveVector2 = movementValue.Get<Vector2>();
        moveDirection = moveVector2;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        
        // This horrible looking math is how the character stays looking at the mouse point
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;   
    }
    void OnFire()
    {
        if(ShotTimer <= 0)
        {
            ShotTimer = ShotDelay;
            GameObject Projectile = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
            Projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce);
            Destroy(Projectile, 5f);
        }
        else
        {
            Debug.Log("Wait for shot cooldown.");
        }
    }
}
