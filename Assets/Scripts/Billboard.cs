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

    private void Awake()
    {
        origanalRotation = transform.rotation.eulerAngles;
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

        // modify the rotation in Euler space to lock certain dimensions
        Vector3 rotation = transform.rotation.eulerAngles;
        if (lockX) rotation.x = origanalRotation.x;
        if (lockY) rotation.y = origanalRotation.y;
        if (lockZ) rotation.z = origanalRotation.z;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
