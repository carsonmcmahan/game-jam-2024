using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    public bool truckDestroyed;
    public WaveManager waveManager;
    public int deliveryPointIndex;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + " entered collider");
        if (other.CompareTag("Player") && truckDestroyed == true)
        {
            Player player = other.GetComponent<Player>();
            player.DeliveryItemVisibility(false);
            DeliveryMade();
        }
        if (other.CompareTag("Truck"))
        {
            Truck truck = other.GetComponent<Truck>();
            DeliveryMade();
            truck.Destroyed();
        }
    }

    private void DeliveryMade()
    {
        waveManager.delivered[deliveryPointIndex] = true; 
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject.GetComponent<DeliveryPoint>(), .1f);
    }
}