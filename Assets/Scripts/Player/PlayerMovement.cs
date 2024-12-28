using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Speed")]
    public float moveSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplyer;
    bool readyToJump;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Start()
    {
        readyToJump = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayerMask);

        MyInput();
        SpeedControl();

        if(isGrounded)
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

    private void MyInput()
    {
        horizonatalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
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
            rb.AddForce(10f * moveSpeed * airMultiplyer * moveDirection.normalized, ForceMode.Force);
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

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
