using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedReplenish : MonoBehaviour
{
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        ThirdPersonController player = other.GetComponent<ThirdPersonController>();
        player.speedBoostCount = 5;
        player.GetComponent<Player>().UpdateSpeedBoostUI((int)player.speedBoostCount);
        AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1f);
    }
}