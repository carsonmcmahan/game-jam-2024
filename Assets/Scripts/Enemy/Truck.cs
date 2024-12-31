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
    }

    private void AddBurningShader()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        List<Material> materials = new List<Material>();
        Material currentMaterial = mesh.material;
        materials.Add(currentMaterial);
        materials.Add(burningShader);
        mesh.materials = new Material[materials.Count];
        for (int i = 0; i < materials.Count; i++)
        {
            mesh.materials[i] = materials[i];
        }
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