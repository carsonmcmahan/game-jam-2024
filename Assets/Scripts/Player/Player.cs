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
    }

    private void UpdateHPUI()
    {
        healthUI.value = health;
        healthUI.maxValue = maxHealth;
    }

    public void UpdateSpeedBoostUI(int count)
    {
        speedBoostCountUI.text = count.ToString();
    }

    public void DeliveryItemVisibility()
    {
        if (deliverableItemHeld == true)
        {
            deliveryItemObject.SetActive(true);
        }
        else
        {
            deliveryItemObject.SetActive(false);
        }
    }
}