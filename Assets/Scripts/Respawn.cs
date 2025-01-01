using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            other.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            other.gameObject.SetActive(true);
        }
    }
}
