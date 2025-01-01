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
        // Calculate the distance between the object and the player
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Base scale factor
        float baseScale = 1f;

        // Scale dynamically based on distance, clamping the scale to avoid extreme values
        float scaleFactor = Mathf.Clamp(distance / 10f, 1f, 25f); // Adjust the 5f max value as needed

        // Apply the calculated scale
        transform.localScale = new Vector3(baseScale * scaleFactor, baseScale * scaleFactor, baseScale * scaleFactor);

        // Optionally adjust position if the object needs to remain at a fixed height
        if (distance < 50f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 5f, transform.localPosition.z);
        }
    }
}
