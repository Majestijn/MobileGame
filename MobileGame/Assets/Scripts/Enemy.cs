using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private Health health;
    private short attackDamage;
    private short moveSpeed;



    private Transform target;

    private Vector3 destination;

    void Start()
    {
        health = GetComponent<Health>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        BuildEnemy();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > 0.5f)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            destination = transform.position + destination;
        }
    }

    void FixedUpdate()
    {
        if (destination != Vector3.zero)
            rigidBody.MovePosition(destination * moveSpeed * Time.fixedDeltaTime);
    }

    void BuildEnemy()
    {
        spriteRenderer.sprite = enemyStats.EnemyImage;

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
}
