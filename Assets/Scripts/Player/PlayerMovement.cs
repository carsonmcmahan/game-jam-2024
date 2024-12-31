using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Speed")]
    public float moveSpeed;

    [Header("Jumping")]
    [SerializeField] private GameObject jumpVFXPrefab;
    private GameObject jumpVFX;

    public float jumpForce;
    public float chargeJumpMulti;
    public float chargeTime;
    public float jumpCoolDown;
    public float airMultiplier;

    private float defaultJumpForce;
    private float defaultChargeTime;
    private float verticalVelocity; // Tracks vertical movement for jumping

    [Header("Key Codes")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float groundDrag;
    public float playerHeight;
    public LayerMask groundLayerMask;
    bool isGrounded;

    [Header("Direction")]
    public Transform orientation;

    CharacterController characterController;

    bool isChargingJump = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        defaultJumpForce = jumpForce;
        defaultChargeTime = chargeTime;
    }

    private void Update()
    {
        isGrounded = GetComponent<ThirdPersonController>().Grounded;

        PlayerInput();
        ChargeJump();

        ApplyMovement();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            isChargingJump = true;
            jumpVFX = Instantiate(jumpVFXPrefab, gameObject.transform);
        }

        if (Input.GetKeyUp(jumpKey) && isGrounded)
        {
            Destroy(jumpVFX);
            Jump();
            ResetJump();
        }
    }

    private void ApplyMovement()
    {
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        verticalVelocity = jumpForce;
    }

    private void ChargeJump()
    {
        if (!isChargingJump) return;

        chargeTime -= Time.deltaTime;
        jumpForce += chargeJumpMulti * Time.deltaTime;

        if (chargeTime <= 0)
        {
            isChargingJump = false;
        }
    }

    private void ResetJump()
    {
        isChargingJump = false;
        jumpForce = defaultJumpForce;
        chargeTime = defaultChargeTime;
    }
}