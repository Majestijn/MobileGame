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



    [SerializeField] private Transform target;

    void Start()
    {
        health = GetComponent<Health>();
        //rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.Find("PlayerBase").transform;

        BuildEnemy();
    }

    void Update()
    {
        Debug.Log(Vector2.Distance(transform.position, target.position));
        if (Vector2.Distance(transform.position, target.position) > 1f)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            //rigidBody.MovePosition(transform.position + direction);

            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
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
}
