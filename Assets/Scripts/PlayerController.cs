using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRB;
    public Vector3 moveVector3;
    public float moveSpeed; // Meters per second
    Vector2 mousePosition;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        if(moveSpeed <= 0)
        {
            moveSpeed = 5;
        }
    }
    void Update()
    {
        gameObject.transform.Translate(moveVector3*Time.deltaTime*moveSpeed);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 moveVector2 = movementValue.Get<Vector2>();
        moveVector3 = new Vector3(moveVector2.x, moveVector2.y, 0);
    }
    void OnLook(InputValue lookValue)
    {
        Vector2 lookVector2 = lookValue.Get<Vector2>();
        float aimAngle = Mathf.Atan2(lookVector2.y, lookVector2.x) * Mathf.Rad2Deg - 90f;
        playerRB.rotation = aimAngle;
    }
}
