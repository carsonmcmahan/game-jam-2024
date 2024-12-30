using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    public bool truckDestroyed;
    public WaveManager waveManager;
    public int deliveryPointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && truckDestroyed == true)
        {
            Player player = GetComponent<Player>();
            player.deliverableItemHeld = false;
            DeliveryMade();
        }
        if (other.CompareTag("Truck"))
        {
            Truck truck = GetComponent<Truck>();
            DeliveryMade();
            Destroy(other);
        }
    }

    private void DeliveryMade()
    {
        waveManager.delivered[deliveryPointIndex] = true;
        Destroy(gameObject.GetComponent<Collider>());
        Destroy(gameObject.GetComponent<DeliveryPoint>(), .001f);
    }
}