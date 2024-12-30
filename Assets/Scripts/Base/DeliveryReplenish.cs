using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryReplenish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Player player = other.GetComponent<Player>();
        player.DeliveryItemVisibility(true);
    }
}