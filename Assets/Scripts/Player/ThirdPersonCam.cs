using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;

    [Header("Speed & Look At")]
    public float rotaionSpeed;
    public Transform combatLookAt;

    [Header("Camera Style")]
    [SerializeField] private CameraStyle currentStyle;

    private enum CameraStyle
    {
        Basic, Combat,TopDown
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if(currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (viewDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotaionSpeed);
            }
        }
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirCombatLookAt.normalized;

            playerObj.forward = dirCombatLookAt.normalized;
        }
    }
}
