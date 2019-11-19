using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyStatFile", menuName ="EnemyStatFile", order =0)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private short health;
    [SerializeField] private short attackDamage;
    [SerializeField] private short moveSpeed;

    public short Health
    {
        get { return health; }
        set { health = value; }
    }

    public short AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public short MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
}
