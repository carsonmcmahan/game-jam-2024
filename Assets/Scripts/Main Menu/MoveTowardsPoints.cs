using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPoints : MonoBehaviour
{
    private float moveSpeed;

    [Header("Points")]
    [SerializeField] private Transform pointOne;
    [SerializeField] private Transform pointTwo;

    private void Awake()
    {
        moveSpeed = Mathf.Round(Random.Range(10f, 40f));
    }

    private void Update()
    {
        float speed = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pointTwo.position, speed);

        if(Vector3.Distance(transform.position, pointTwo.position) < 0.001f)
        {
            transform.position = pointOne.position;
            moveSpeed = Mathf.Round(Random.Range(10f, 40f));
        }
    }
}
