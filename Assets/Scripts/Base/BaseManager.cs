using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject playerSpawn;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player.baseManager = this;
        player.waveManager = waveManager;
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
        player.ResetHP();
    }
}