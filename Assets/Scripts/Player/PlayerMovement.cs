using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Speed")]
    public float moveSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float chargeJumpMulti;
    public float chargeTime;
    public float jumpCoolDown;
    public float airMultiplier;

    private float defaultJumpForce;
    private float defaultChargeTime;

    [Header("Key Codes")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float groundDrag;
    public float playerHeight;
    public LayerMask groundLayerMask;
    bool isGrounded;

    [Header("Direction")]
    public Transform orientation;

    float horizonatalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    bool isChargingJump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        defaultJumpForce = jumpForce;
        defaultChargeTime = chargeTime;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayerMask);

        PlayerInput();
        SpeedControl();
        ChargeJump();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        } 
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizonatalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            isChargingJump = true;
        }

        if (Input.GetKeyUp(jumpKey) && isGrounded )
        {
            Jump();
            ResetJump();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizonatalInput;

        if(isGrounded)
        {
            rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
        }
        else
        {
            rb.AddForce(10f * moveSpeed * airMultiplier * moveDirection.normalized, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 moveInput = new Vector3(horizonatalInput, 0f, verticalInput);

        if (moveInput.magnitude > 0.1f)
        {
            Vector3 horizontalDirection = orientation.forward * verticalInput + orientation.right * horizonatalInput;
            Vector3 jumpDirection = (horizontalDirection.normalized + 1.5f * Vector3.up).normalized;
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
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
