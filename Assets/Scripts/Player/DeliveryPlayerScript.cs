using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPlayerScript : MonoBehaviour
{
    public bool deliverableItemHeld;
    [SerializeField] private GameObject deliveryItemObject;

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