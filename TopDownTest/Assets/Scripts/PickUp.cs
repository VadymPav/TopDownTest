using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Zenject;

public class PickUp : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            Destroy(collision.gameObject);
            gameManager.IncreaseCoins();
            gameManager.SpawnCoin();
        }
    }
}
