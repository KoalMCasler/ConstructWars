using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats", order = 0)]
public class Stats : ScriptableObject
{
    [Header("Core Stats")]
    public int maxHP; //Hp in game, used to determain starting HP for each match. 
    public int maxHPBase;
    public float DamageResitance; //DR in game
    public float DamageResitanceBase;
    public float DamageModifier; //DM in game
    public float DamageModifierBase; 
    public float SpellChargeRate; //SR in game
    public float SpellChargeRateBase;
    public int Luck;
    public int LuckBase;
    [Header("Movement Stats")]
    public float moveSpeed = 5f; //MS in game
    public float moveSpeedBase = 5f; 
    public float maxMoveSpeed = 20f;
}
