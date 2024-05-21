using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    private Rigidbody2D rb;

    private Vector2 movementVector;

    public Transform turretParent;

    public float maxSpeed;
    public float rotationSpeed;
    public float acceleration;
    public float turretSpeed;

    [SerializeField] private float deacceleration;
    [SerializeField] private float reverseRotationThreshold;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentForwardDirection;

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

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        var turretDirection = (Vector3)pointerPosition - transform.position;

        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;

        var rotationStep = turretSpeed * Time.deltaTime;

        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle), rotationStep);
    }

    public void HandleShoot()
    {
        Debug.Log("FIRE");
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += acceleration * currentForwardDirection * Time.deltaTime;
        }
        else
        {
            if (currentSpeed < -0.1)
            {
                currentSpeed += deacceleration * Time.deltaTime;
            }
            else if (currentSpeed > 0.1)
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

        if (currentSpeed < reverseRotationThreshold)
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, movementVector.x * rotationSpeed * Time.fixedDeltaTime));
        }
        else
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
