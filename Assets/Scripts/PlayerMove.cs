using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Rigidbody2D rb;

    [SerializeField] float mvSpeed = 10;
    [SerializeField] float rtSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            //rb.velocity = new Vector2
        }

        if (Input.GetKey("s"))
        {
            rb.AddRelativeForce(Vector2.right * -mvSpeed);
        }

        if (Input.GetKey("a"))
        {
            rb.AddTorque(rtSpeed);
        }

        if (Input.GetKey("d"))
        {
            rb.AddTorque(-rtSpeed);
        }

        if (Input.GetKey("space"))
        {
            Debug.Log("FIRE");
        }
    }
}
