﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyStats enemyStatFile;

    public GameObject enemyPrefab;
    public Transform spawnpoint;
    public Transform enemyTarget;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(enemyPrefab, spawnpoint.position, Quaternion.identity);
            obj.GetComponent<Enemy>().SetEnemyStatFile(enemyStatFile);
            obj.GetComponent<Enemy>().SetEnemyTarget(enemyTarget);
        }
    }
}
