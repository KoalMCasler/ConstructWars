using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Object Referances")]
    public GameObject spell;
    public Transform projectilePos;
    public GameObject player;
    public GameObject targetHUD;
    public Slider healthBar;
    public Rigidbody2D rb;
    [Header("Detection Varaibles")]
    public bool isTargetInCombatRange;
    public Vector2 directionToTarget;
    public float combatRange;
    private float distance;
    public float targetDistance;
    public List<Transform> enemiesInArena;
    public Transform target;
    [Header("Attack Varaibles")]
    public int attackRange;
    public int shotDelay;
    public float shotTime;
    public bool canFire;
    [Header("Stats")]
    public float currentHP;
    public float rotationSpeed;
    public Stats enemyStats;
    private float turnTimer;
    public float maxTurnTimer;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = enemyStats.maxHP;
        if(shotTime <= 0)
        {
            shotTime = shotDelay;
        }
        canFire = false;
        healthBar.maxValue = enemyStats.maxHP;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        turnTimer = maxTurnTimer;
    }
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies[0] != null)
        {
            foreach(GameObject i in enemies)
            {
                if(i != this.gameObject)
                {
                    enemiesInArena.Add(i.transform);
                }
            }
        }
        enemiesInArena.Add(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHP;
        CheckStatus();
        shotTime -= Time.deltaTime;
        CheckClosestEnemy();
        CheckEnemyList();
        if(target != null)
        {
            Vector2 targetDirecion = target.position - transform.position;
            directionToTarget = targetDirecion.normalized;
            CheckIfCanFire();
            if(shotTime <= 0)
            {
                if(canFire && isTargetInCombatRange)
                {
                    Shoot();
                }
                shotTime = shotDelay;
            }
            if(target.gameObject.activeSelf == false)
            {
                target = null;
            }
        }
    }
    void FixedUpdate()
    {
        if(target != null)
        {
            RotateTowardsTarget();
            PresueTarget();
        }
        else
        {
            Wander();
        }
    }
    void Shoot()
    {
        GameObject projectile = Instantiate(spell, projectilePos.position, Quaternion.identity);
        projectile.GetComponent<SpellBase>().shotByPlayer = false;
        projectile.GetComponent<SpellBase>().target = target;
        projectile.GetComponent<SpellBase>().firedFrom = this.gameObject;
    }
    void CheckStatus()
    {
        if(currentHP <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    void OnDisable()
    {
        targetHUD.SetActive(false);
    }

    void CheckClosestEnemy()
    {
        List<float> distances = new List<float>();
        for(int i = 0; i < enemiesInArena.Count; i++)
        {
            distance = Vector3.Distance(this.transform.position, enemiesInArena[i].position);
            distances.Add(distance);
            if(distances[i] < combatRange)
            {   
                target = enemiesInArena[distances.IndexOf(distances.Min())];
                targetDistance = distances.Min();
                isTargetInCombatRange = true;
            }
            else
            {
                target = null;
                isTargetInCombatRange = false;
            }
        }

    }

    void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, directionToTarget);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.SetRotation(rotation);
    }
    void PresueTarget()
    {
        if(targetDistance > attackRange)
        {
            rb.velocity = transform.up * enemyStats.moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    void Wander()
    {
        rb.velocity = transform.up * (enemyStats.moveSpeed * 0.8f);
        PickRandomDirection();
    }
    void CheckIfCanFire()
    {
        if(targetDistance <= attackRange && targetDistance < combatRange)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }
    }
    void PickRandomDirection()
    {
        turnTimer -= Time.deltaTime;
        if(turnTimer <= 0)
        {
            float angleChange = Random.Range(-90f,90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            rb.SetRotation(rotation);
            turnTimer = maxTurnTimer - Random.Range(-2f,2f);
        }
    }
    void CheckEnemyList()
    {
        for(int i = 0; i < enemiesInArena.Count; i++)
        {
            if(enemiesInArena[i].gameObject.activeSelf == false)
            {
                enemiesInArena.RemoveAt(i);
            }
        }
    }
}
