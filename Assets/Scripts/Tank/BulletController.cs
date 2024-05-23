using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int damage;
    public float maxDistance;

    private Vector2 startPosition;
    private float conqueredDistance = 0;
    private Rigidbody2D rb;

    private IObjectPool<BulletController> bulletPool;

    public IObjectPool<BulletController> BulletPool { set => bulletPool = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        startPosition = transform.position;
        rb.velocity = transform.up * speed;
    }

    public void Deactivate()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        bulletPool.Release(this);
    }

    private void Update()
    {
        conqueredDistance = Vector2.Distance(transform.position, startPosition);
        if (conqueredDistance > maxDistance)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collider: " + collision.name);

        var damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(damage);
        }

        Deactivate();
    }
}
