using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class DataManager : ScriptableObject
{
    [Header("Player")]
    public int Health;
    public float PlayerSpeed;
    [Header("Enemy")]
    public float EnemySpeed;
    public float BulletSpeed;
    public float EnemyAttackSpeed;
    public float EnemyAttackRange;
    public float EnemySightRange;
    [Header("Level")]
    public int NumberOfEnemies;
    public int NumberOfCoins;
}
