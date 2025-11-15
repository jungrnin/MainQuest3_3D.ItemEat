using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;

    [Header("Rotate")]
    [SerializeField] private float DPI = 600f;
    [SerializeField] private Transform cameraPoint;
    private float cameraPitch = 0f;

    private Rigidbody rb;
    private bool isGrounded = true;
    [SerializeField] Animator animator;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleMove();
        HandleJump();
        Rotate();
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        float inputSpeed = new Vector2(h, v).magnitude; // 0~1
        animator.SetFloat("Speed", inputSpeed);

        animator.SetBool("IsGround", isGrounded);
    }
    private void HandleMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0.0f, v);

        if (dir.magnitude > 1.0f)
        {
            dir = dir.normalized;
        }
        Move(dir);
    }

    private void Move(Vector3 direction)
    {
        Vector3 move = transform.TransformDirection(direction);   

        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }
    private void Jump()
    {
        isGrounded = false;

        Vector3 jumpVelocity = rb.velocity;
        jumpVelocity.y = jumpForce;
        rb.velocity = jumpVelocity;
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * DPI * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * DPI * Time.deltaTime;

        transform.Rotate(0f, mouseX, 0f);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -20f, 30f);

        cameraPoint.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
