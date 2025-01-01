using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Truck : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private Material burningShader;

    public Transform deliveryPoint;
    public WaveManager waveManager;
    public GameObject explosionPrefab;
    public GameObject explosionPoints;

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
        AddBurningShader();
        SpawnExplosions();
    }

    private void AddBurningShader()
    {
        // Get the MeshRenderer component from the child object
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        if (mesh == null)
        {
            Debug.LogError("MeshRenderer component is missing on the child object.");
            return;
        }

        // Retrieve the shared materials to avoid creating material instances
        Material[] currentMaterials = mesh.sharedMaterials;

        // Create a new array with one extra slot for the burning shader
        Material[] newMaterials = new Material[currentMaterials.Length + 1];

        // Copy the existing materials to the new array
        for (int i = 0; i < currentMaterials.Length; i++)
        {
            newMaterials[i] = currentMaterials[i];
        }

        // Add the burning shader material to the new array
        newMaterials[newMaterials.Length - 1] = burningShader;

        // Apply the updated materials array to the MeshRenderer
        mesh.materials = newMaterials; // This applies the new material array
    }

    private void SpawnExplosions()
    {
        foreach (Transform t in explosionPoints.transform)
        {
            Instantiate(explosionPrefab, t);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack2"))
        {
            health--;
            if (health <= 0) Destroyed();
        }
    }
}