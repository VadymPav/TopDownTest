using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Inject] private DataManager config;
    [Inject] private EnemyAI enemy;
    [Inject] private DiContainer _diContainer;
    
    private int enemyNumber;
    private int coinNumber;
    private int currentCoin;
    public GameObject enemyPrefab;
    public GameObject coinPrefab;

    public GameObject GameWinWindow;
    public GameObject GameLostWindow;
    public GameObject GamePauseWindow;
    
    public TextMeshProUGUI text;
    public float range = 100f;
    private void Start()
    {
        Time.timeScale = 1;
        enemyNumber = config.NumberOfEnemies;
        coinNumber = config.NumberOfCoins;
        GameWinWindow.SetActive(false);
        GameLostWindow.SetActive(false);
        GamePauseWindow.SetActive(false);
        SpawnEnemy();
        SpawnCoin();
    }

    private void Update()
    {
        if (currentCoin == config.NumberOfCoins)
        {
            GameWin();
        }
    }

    private void UpdateText()
    {
        text.text = currentCoin.ToString() + "/" + config.NumberOfCoins;
    }

     void GameWin()
     {
         Time.timeScale = 0;
         GameWinWindow.SetActive(true);
         
     }
     public void GameLost()
     {
         Time.timeScale = 0;
         GameLostWindow.SetActive(true);
     }

     public void GamePaused()
     {
         Time.timeScale = 0;
         GamePauseWindow.SetActive(true);
     }

     public void IncreaseCoins()
     {
         currentCoin += 1;
         UpdateText();
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

    public void MenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void PlayButton()
    {
        Time.timeScale = 1;
        GamePauseWindow.SetActive(false);
    }
}
