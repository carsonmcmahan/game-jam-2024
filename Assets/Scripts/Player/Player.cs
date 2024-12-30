using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            health--;
        }
    }

    public void ResetHP()
    {
        health = maxHealth;
    }
}