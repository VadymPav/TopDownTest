using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Inject] private DataManager config;
    [Inject] private EnemyAI enemy;
    [Inject] private DiContainer _diContainer;
    
    private int enemyNumber;
    private int coinNumber;
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public float range = 100f;
    private void Start()
    {
        enemyNumber = config.NumberOfEnemies;
        coinNumber = config.NumberOfCoins;
        SpawnEnemy();
        SpawnCoin();
    }


    public void SpawnCoin()
    {
        if (coinNumber > 0)
        {
            coinNumber -= 1;
            Vector3 point;
            if (RandomPoint(transform.position, range, out point))
            {
                _diContainer.InstantiatePrefab(coinPrefab, point, Quaternion.identity, null);
            }
        }
        else 
            return;
    }
    void SpawnEnemy()
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            Vector3 point;
            if (RandomPoint(transform.position, range, out point))
            {
                _diContainer.InstantiatePrefab(enemyPrefab, point, Quaternion.identity, null);
            }
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 100, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
