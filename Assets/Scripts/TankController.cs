using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    private Rigidbody2D rb;

    private Vector2 movementVector;

    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deacceleration;
    [SerializeField] float currentSpeed;
    [SerializeField] float currentForwardDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void HandleMove(Vector2 movementVector)
    {
        this.movementVector = movementVector;

        CalculateSpeed(movementVector);

        if (movementVector.y > 0)
        {
            currentForwardDirection = 1;
        }
        else if (movementVector.y < 0)
        {
            currentForwardDirection = -1;
        }
        else
        {
            currentForwardDirection = 0;
        }
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += acceleration * currentForwardDirection * Time.deltaTime;
        }
        else
        {
            if (currentSpeed < 0)
            {
                currentSpeed += deacceleration * Time.deltaTime;
            }
            else if (currentSpeed > 0)
            {
                currentSpeed -= deacceleration * Time.deltaTime;
            }

            if (Mathf.Abs(currentSpeed) < 0.1)
            {
                currentSpeed = 0;
            }
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector2)transform.up  * currentSpeed * Time.fixedDeltaTime;

        if (currentSpeed < -10)
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, movementVector.x * rotationSpeed * Time.fixedDeltaTime));
        }
        else
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
