using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryReplenish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DeliveryPlayerScript player = other.GetComponent<DeliveryPlayerScript>();
        player.deliverableItemHeld = true;
        player.DeliveryItemVisibility();
    }
}