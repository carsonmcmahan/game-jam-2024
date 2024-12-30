using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Truck : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float speed;
    [SerializeField] private float health;

    public Transform deliveryPoint;
    public WaveManager waveManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(deliveryPoint.position);
    }

    public void Destroyed()
    {
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        deliveryPoint.gameObject.GetComponent<DeliveryPoint>().truckDestroyed = true;
        waveManager.trucks.Remove(gameObject);
        Destroy(gameObject, 10f);
        //apply burnt shader material here
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack1"))
        {
            health--;
            if (health <= 0) Destroyed();
        }
    }
}