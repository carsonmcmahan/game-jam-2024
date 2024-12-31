using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Objects")]
    public GameObject projectileOne;
    public float projectileOneForce;
    public GameObject projectileTwo;
    public float projectileTwoForce;

    [Header("Spawn Position")]
    public Transform spawnPoint;
    [SerializeField] private Transform beamSpawnPosition;

    [Header("Cool Downs")]
    public float primaryFireCoolDown;
    public float altFireCoolDown;

    bool canFirePrimary;
    bool canFireAlt;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        canFireAlt = true;
        canFirePrimary = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canFirePrimary)
        {
            SpawnProjectile(projectileOne, projectileOneForce);
            canFirePrimary = false;
            Invoke(nameof(ResetPrimaryFire), primaryFireCoolDown);
        }

        if(Input.GetMouseButtonDown(1) && canFireAlt)
        {
            Destroy(Instantiate(projectileTwo, beamSpawnPosition), 1.5f);
            
            canFireAlt = false;
            Invoke(nameof(RestAltFire), altFireCoolDown);
        }
    }

    // spawn the object and add force to its rigid body
    private void SpawnProjectile(GameObject gameObject, float projectileForce)
    {
        GameObject projectile = Instantiate(gameObject, spawnPoint.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = mainCamera.transform.forward * projectileForce + transform.up;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    // calculates the direction based on the cameras position
    //private Vector3 CalculateDirection()
    //{
    //    Vector3 forceDirection = mainCamera.transform.forward;
    //    if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, 500f))
    //    {
    //        forceDirection = (hit.point - spawnPoint.position).normalized;
    //    }

    //    return forceDirection;
    //}

    private void ResetPrimaryFire()
    {
        canFirePrimary = true;
    }

    private void RestAltFire()
    {
        canFireAlt = true;
    }
}
