using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float destroyDistance;
    Vector3 initalPositon;

    private void Start()
    {
        initalPositon = transform.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(initalPositon, transform.position);

        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
