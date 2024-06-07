using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats", order = 0)]
public class Stats : ScriptableObject
{
    [Header("Core Stats")]
    public int maxHP; //Hp in game, used to determain starting HP for each match. 
    public float DamageResitance; //DR in game
    public float DamageModifier; //DM in game
    public float SpellChargeRate; //SR in game
    public int Luck;
    [Header("Movement Stats")]
    public float moveSpeed = 5f; //MS in game
    public float maxMoveSpeed = 10f;
}
