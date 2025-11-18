using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 3.0f;
    private float originSpeed;
     
    [Header("Rotate")]
    [SerializeField] private float DPI = 600f;
    [SerializeField] private Transform cameraPoint;
    private float cameraPitch = 0f;

    [Header("GetItem")]
    [SerializeField] public float magnetRange = 3f;
    private float originRange;

    [Header("Score")]
    public int score = 0;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isPaused = false;
    [Header("Animator")]
    [SerializeField] Animator animator;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        originSpeed = moveSpeed;
        originRange = magnetRange;
    }

    void Update()
    {
        HandleMove();
        HandleJump();
        Rotate();
        UpdateAnimation();
        Magnet();
        Pause();
    }
    private void UpdateAnimation()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        float inputSpeed = new Vector2(h, v).magnitude; 
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

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void GetSpeedItem(float addSpeed, float speedTime)
    {
        StopCoroutine("SpeedReset");

        moveSpeed *= addSpeed;

        StartCoroutine(SpeedReset(speedTime));
    }
    private IEnumerator SpeedReset(float speedTime)
    {
        yield return new WaitForSeconds(speedTime);

        moveSpeed = originSpeed;
    }

    public void GetMagnetItem(float addRange, float magnetTime)
    {
        StopCoroutine("MagnetReset");

        magnetRange *= addRange;

        StartCoroutine(MagnetReset(magnetTime));
    }
    private IEnumerator MagnetReset(float magnetTime)
    {
        yield return new WaitForSeconds(magnetTime);

        magnetRange = originRange;
    }

    private void Magnet()
    {
        

        Collider[] hits = Physics.OverlapSphere(transform.position, magnetRange);

        foreach(var hit in hits)
        {
            if(hit.CompareTag("Item"))
            {
                hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, 10f * Time.deltaTime);
            }
        }
    }

    private void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                UIObserve.Instance.ShowPause();
                isPaused = true;
            }
            else
            {
                UIObserve.Instance.HidePause();
                isPaused = false;    
            }
        }
    }

   
}
