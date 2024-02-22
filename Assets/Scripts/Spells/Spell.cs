using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [Header("SpellComponents")]
    [SerializeField]
    public string spellName;
    [SerializeField]
    public int damage;
    [SerializeField]
    public string damageType;
    [SerializeField]
    public int shotLife; //In seconds
    [SerializeField]
    public int maxShotLife;
    [SerializeField]
    public int shotDelay; //In seconds
    [SerializeField]
    public float shotSpeed;
    [SerializeField]
    public bool shotByPlayer;
    [Header("Object Referances")]
    public GameObject player;
    public GameObject Crosshair;
    public GameObject ExplodePreFab;
    //Variables
    public float distance;
}
