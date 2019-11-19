using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void ThisEnemyDiedDelegate(Enemy enemy);
    public event ThisEnemyDiedDelegate OnThisEnemyDied;

    [SerializeField] private EnemyStats enemyStats;

    private Health health;
    private short attackDamage;
    private short moveSpeed;



    public Transform target;

    private Vector3 destination;

    void Start()
    {
        health = GetComponent<Health>();

        BuildEnemy();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, Vector3.up), Time.deltaTime * 10f);
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            destination = transform.position + destination;
        }
    }

    void BuildEnemy()
    {
        health.SetHealth(enemyStats.Health);

        attackDamage = enemyStats.AttackDamage;
        moveSpeed = enemyStats.MoveSpeed;

    }

    public void SetEnemyStatFile(EnemyStats _enemyStats)
    {
        enemyStats = _enemyStats;
    }

    public void SetEnemyTarget(Transform _target)
    {
        target = _target;
    }

    public void InvokeOnDiedEvent()
    {
        OnThisEnemyDied?.Invoke(this);
    }
}
