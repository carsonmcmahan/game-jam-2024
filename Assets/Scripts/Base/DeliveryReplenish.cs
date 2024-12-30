using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryReplenish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        player.deliverableItemHeld = true;
        player.DeliveryItemVisibility();
    }
}