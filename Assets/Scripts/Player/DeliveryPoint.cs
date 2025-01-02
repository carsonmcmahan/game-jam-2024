using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DeliveryPoint : MonoBehaviour
{
    public bool truckDestroyed;
    public WaveManager waveManager;
    public int deliveryPointIndex;
    public AudioClip deliverySound;

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
            waveManager.truckMadeDelivery = true;
            truck.Destroyed();
        }
    }

    private void DeliveryMade()
    {
        waveManager.delivered[deliveryPointIndex] = true; 
        gameObject.GetComponent<Collider>().enabled = false;
        AudioSource.PlayClipAtPoint(deliverySound, transform.position, 1f); 
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject.GetComponent<DeliveryPoint>(), .1f);
    }
}