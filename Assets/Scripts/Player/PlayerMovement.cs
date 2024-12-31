using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jumping")]
    [SerializeField] private GameObject jumpVFXPrefab;
    private GameObject jumpVFX;

    public float jumpForce;
    public float chargeJumpMulti;
    public float chargeTime;

    private float defaultJumpForce;
    private float defaultChargeTime;
    private float verticalVelocity;

    [Header("Key Codes")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    bool isGrounded;

    [Header("Direction")]
    public Transform orientation;
    private StarterAssetsInputs starterAssetsInputs;
    private Vector2 moveDirection;
    private ThirdPersonController thirdPersonController;
    private float speed;

    CharacterController characterController;

    bool isChargingJump = false;
    bool canApplyMovement = false;
    private Vector3 jumpDirection; // Stores the jump direction

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        defaultJumpForce = jumpForce;
        defaultChargeTime = chargeTime;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        isGrounded = GetComponent<ThirdPersonController>().Grounded;
        moveDirection = starterAssetsInputs.move;
        speed = thirdPersonController.MoveSpeed;

        PlayerInput();
        ChargeJump();

        if (canApplyMovement)
        {
            ApplyMovement();
        }
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

            // Calculate jump direction using speed
            Vector3 forward = orientation.forward * moveDirection.y;
            Vector3 right = orientation.right * moveDirection.x;
            jumpDirection = (forward + right).normalized * speed * 0.5f; // Scale horizontal influence by speed

            Jump();
            ResetJump();
            canApplyMovement = true; // Enable movement after jump key is released
        }
    }

    private void ApplyMovement()
    {
        // Apply gravity to vertical velocity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // Combine vertical and horizontal movement
        Vector3 movement = (jumpDirection * Time.deltaTime) // Use jumpDirection scaled by speed
                         + (Vector3.up * verticalVelocity * Time.deltaTime);

        // Move the character
        characterController.Move(movement);

        // Reset `canApplyMovement` if grounded
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Ensure grounded state
            canApplyMovement = false; // Stop applying movement until the next jump
        }
    }

    private void Jump()
    {
        verticalVelocity = jumpForce; // Maintain vertical strength
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