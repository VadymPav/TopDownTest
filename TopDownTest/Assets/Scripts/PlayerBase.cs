using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerBase : MonoBehaviour
{
    [Inject] private DataManager config;
    [Inject] private GameManager gameManager;
    public Image bar;
    private int health;
    private float fill;

    private void Start()
    {
        health = config.Health;
        fill = health * 0.01f;

    }

    private void Update()
    {
        if (health <= 0)
        {
            gameManager.GameLost();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        health -= config.Damage;
        bar.fillAmount = health * 0.01f;
    }
}
