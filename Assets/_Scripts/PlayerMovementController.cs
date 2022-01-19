using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody rb;
    private float distanceToFeet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        distanceToFeet = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        movementInput *= movementSpeed * Time.deltaTime;
        transform.Translate(new Vector3(movementInput.x,0f,movementInput.y));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            }
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToFeet + 0.1f);
    }
}
