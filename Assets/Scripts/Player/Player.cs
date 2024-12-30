using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthUI;
    [SerializeField] private TMP_Text speedBoostCountUI;


    public bool deliverableItemHeld;
    [SerializeField] private GameObject deliveryItemObject;

    public BaseManager baseManager;
    public WaveManager waveManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            health--;
            UpdateHPUI();
        }
    }

    public void ResetHP()
    {
        health = maxHealth;
        UpdateHPUI();
        if (health <= 0) baseManager.ResetPlayer();
    }

    private void UpdateHPUI()
    {
        healthUI.maxValue = maxHealth;
        healthUI.value = health;
    }

    public void UpdateSpeedBoostUI(int count)
    {
        speedBoostCountUI.text = count.ToString();
    }

    public void DeliveryItemVisibility(bool pickup)
    {
        if (pickup == true)
        {
            Debug.Log("pickup item" + deliverableItemHeld);
            if (waveManager.nextWaveCanSpawn == true) waveManager.SpawnWave();
            if (deliverableItemHeld == true) return;
            Debug.Log("pickup 2");
            deliveryItemObject.SetActive(true);
            deliverableItemHeld = true;
        }
        else
        {
            if (deliverableItemHeld == false) return;
            deliveryItemObject.SetActive(false);
            deliverableItemHeld = false;
        }
    }
}