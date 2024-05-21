using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();

    void Update()
    {
        GetMovement();
    }

    private void GetMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime, Input.GetAxisRaw("Vertical") * Time.deltaTime);
        OnMove?.Invoke(movementVector.normalized);
    }
}