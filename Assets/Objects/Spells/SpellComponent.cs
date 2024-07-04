using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellComp", menuName = "SpellComponent", order = 0)]
public class SpellComponent : ScriptableObject
{
    [Header("SpellComponents")]
    [SerializeField]
    public string spellName;
    [SerializeField]
    public float damage;
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
    public LayerMask playerLayer;
    public LayerMask Default;
}
