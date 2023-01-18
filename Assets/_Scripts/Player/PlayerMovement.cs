using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    // Public Variables
    public static Action IdleEvent, MoveRightEvent, MoveLeftEvent, JumpEvent, LandEvent;

    // Private Variables
    [SerializeField] private float movementSpeed = 8;
    [SerializeField] private float jumpForce = 800;
    [SerializeField] private float gravityMultiplier = 4;

    private Rigidbody _rb;
    private int _jumpCount;

    #endregion Variables

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameStates.Lose)
        {
            return;
        }
        
        Move();
        Jump();

        LimitPlayer();
    }

    private void FixedUpdate()
    {
        if (IsFalling())
        {
            _rb.AddForce(Physics.gravity * (gravityMultiplier * _rb.mass));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _jumpCount = 0;

            LandEvent?.Invoke();
        }
    }

    private void OnDestroy()
    {
        IdleEvent = null;
        MoveRightEvent = null;
        MoveLeftEvent = null;
        JumpEvent = null;
        LandEvent = null;
    }

    private void Move()
    {
        var horInput = Input.GetAxisRaw("Horizontal");

        if (horInput > 0)
        {
            MoveRightEvent?.Invoke();
        }
        else if (horInput < 0)
        {
            MoveLeftEvent?.Invoke();
        }
        else
        {
            IdleEvent?.Invoke();
        }
        
        transform.position += Vector3.right * (horInput * (Time.deltaTime * movementSpeed));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < 2)
        {
            _rb.velocity = Vector3.zero;
            
            _rb.AddForce(Vector3.up * jumpForce);
            _jumpCount++;

            JumpEvent?.Invoke();
        }
    }

    private bool IsFalling()
    {
        return !Physics.Raycast(transform.position, Vector3.down, 1f);
    }

    private void LimitPlayer()
    {
        var pos = transform.position;
        
        if (transform.position.x <= -12)
        {
            pos.x = -12;
        }
        if (transform.position.y >= 12)
        {
            pos.y = 12;
        }
        if (transform.position.x >= 12)
        {
            pos.x = 12;
        }
        
        transform.position = pos;
    }
}