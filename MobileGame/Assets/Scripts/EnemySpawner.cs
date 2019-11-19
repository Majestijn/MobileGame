using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public delegate void EnemyDiedDelegate(Enemy enemy);
    public event EnemyDiedDelegate OnEnemyDied;

    [SerializeField] EnemyStats enemyStatFile;

    private List<Enemy> enemyList;

    public GameObject enemyPrefab;
    public Transform spawnpoint;
    public Transform enemyTarget;
    void Start()
    {
        instance = this;

        enemyList = new List<Enemy>();

        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, new Vector3(5 + i * 2, 0, 10 + i * 2), Quaternion.identity);
            Enemy enemy = obj.GetComponent<Enemy>();

            enemy.SetEnemyStatFile(enemyStatFile);
            enemy.SetEnemyTarget(enemyTarget);
            enemy.OnThisEnemyDied += RemoveEnemyFromList;
            enemy.OnThisEnemyDied += InvokeOnEnemyDiedEvent;

            enemyList.Add(enemy);
        }
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public void InvokeOnEnemyDiedEvent(Enemy enemy)
    {
        OnEnemyDied?.Invoke(enemy);
    }

    public Enemy GetClosestEnemy(Transform playerTransform)
    {
        float closestDistance = float.PositiveInfinity;
        Enemy closestEnemy = null;

        for (int i = 0; i < enemyList.Count; i++)
        {
            float distance = Vector3.Distance(playerTransform.position, enemyList[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemyList[i];
            }
        }

        return closestEnemy;
    }
}
