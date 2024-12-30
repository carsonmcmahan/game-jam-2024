using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private GameObject waveManager;
    [SerializeField] private GameObject playerSpawn;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health <= 0) ResetPlayer();
    }

    private void ResetPlayer()
    {
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
        player.ResetHP();
    }
}