using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;

    [Header("Lock Rotation")]
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;

    private Vector3 origanalRotation;
    private enum BillboardType { LookAtCamera, CameraForward }

    private Transform playerTransform;

    Vector3 maxScale = new(25f, 25f, 25f);
    Vector3 medianScale = new(20f, 20f, 20f);
    Vector3 minScale = new(15f, 15f, 15f);

    private void Awake()
    {
        origanalRotation = transform.rotation.eulerAngles;
        playerTransform = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        ScaleBillboard();
    }

    private void LateUpdate()
    {
        switch(billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(Camera.main.transform.position, Vector3.up); 
                break;
            case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            default:
            break;
        }

        LockRotation();
    }

    // modify the rotation in Euler space to lock certain dimensions
    private void LockRotation()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        if (lockX) rotation.x = origanalRotation.x;
        if (lockY) rotation.y = origanalRotation.y;
        if (lockZ) rotation.z = origanalRotation.z;
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void ScaleBillboard()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance >= 200f)
        {
            transform.localScale = maxScale;
            transform.localPosition = new Vector3(0, 25f, 0);
        }
        else if(distance >= 100 )
        {
            transform.localScale = medianScale;
            transform.localPosition = new Vector3(0, 20f, 0);
        }
        else if (distance >= 50f)
        {
            transform.localScale = minScale;
            transform.localPosition = new Vector3(0, 15f, 0);
        }
        else if(distance < 50f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.localPosition = new Vector3(0, 5f, 0);
        }
    }
}
