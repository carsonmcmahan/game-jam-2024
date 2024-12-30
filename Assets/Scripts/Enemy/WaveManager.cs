using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int waveCount;
    [SerializeField] private List<GameObject> deliverySpawnPointPairs = new List<GameObject>();
    [SerializeField] private GameObject truckPrefab;

    public List<GameObject> trucks = new List<GameObject>();
    public List<bool> delivered = new List<bool>();

    public bool nextWaveCanSpawn;

    private void Start()
    {
        waveCount = 1;
        nextWaveCanSpawn = true;
    }

    private void Update()
    {
        if (delivered.Count > 0 && delivered.TrueForAll(status => status))
        {
            nextWaveCanSpawn = true;
        }
    }

    public void SpawnWave()
    {
        Debug.Log("spawning wave");
        nextWaveCanSpawn = false;
        List<GameObject> availablePairs = new List<GameObject>(deliverySpawnPointPairs);
        trucks = new List<GameObject>();
        delivered = new List<bool>();

        for (int i = 0; i < waveCount; i++)
        {

            if (availablePairs.Count == 0)
            {
                break;
            }

            // Select a random pair from the available list
            int randomPairIndex = Random.Range(0, availablePairs.Count);
            GameObject selectedPair = availablePairs[randomPairIndex];

            // Remove the selected pair from the available list to prevent duplicates
            availablePairs.RemoveAt(randomPairIndex);

            // Get the two child objects (spawn and delivery points)
            Transform pointA = selectedPair.transform.GetChild(0);
            Transform pointB = selectedPair.transform.GetChild(1);

            // Randomly assign spawn and delivery points
            Transform spawnPoint = Random.Range(0, 2) == 0 ? pointA : pointB;
            Transform deliveryPoint = spawnPoint == pointA ? pointB : pointA;

            // add components
            DeliveryPoint dPScript = deliveryPoint.AddComponent<DeliveryPoint>();
            deliveryPoint.GetComponent<SphereCollider>().enabled = true;
            dPScript.waveManager = this;
            dPScript.deliveryPointIndex = i;
            delivered.Add(false);

            // Spawn the enemy at the spawn point
            GameObject truck = Instantiate(truckPrefab, spawnPoint.position, Quaternion.identity);
            trucks.Add(truck);
            truck.GetComponent<Truck>().waveManager = this;

            // Assign the delivery point to the enemy (if it has a script to handle this)
            Truck truckScript = truck.GetComponent<Truck>();
            if (truckScript != null)
            {
                truckScript.deliveryPoint = deliveryPoint;
            }
        }
        waveCount++;
    }
}