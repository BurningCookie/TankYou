using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int damage;
    public float maxDistance;

    private Vector2 startPosition;
    private float conqueredDistance = 0;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        startPosition = transform.position;
        rb.velocity = transform.up * speed;
    }

    private void Update()
    {
        conqueredDistance = Vector2.Distance(transform.position, startPosition);
        if (conqueredDistance > maxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided" + collision.name);
        DisableObject();
    }
}
