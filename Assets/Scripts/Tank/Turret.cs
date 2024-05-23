using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class Turret : MonoBehaviour
{
    public List<Transform> turretBarrels;
    [SerializeField] private BulletController bulletPrefab;
    public float reloadDelay;

    public int damage;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private IObjectPool<BulletController> bulletPool;

    [SerializeField] private bool collectionCheck = true;

    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxCapacity = 100;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();

        bulletPool = new ObjectPool<BulletController>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxCapacity);

        damage = bulletPrefab.damage;
    }

    private void OnDestroyPooledObject(BulletController pooledBullet)
    {
        Destroy(pooledBullet.gameObject);
    }

    private void OnReleaseToPool(BulletController pooledBullet)
    {
        pooledBullet.gameObject.SetActive(false);
    }

    private void OnGetFromPool(BulletController pooledBullet)
    {
        pooledBullet.gameObject.SetActive(true);
    }

    private BulletController CreateProjectile()
    {
        BulletController bullet = Instantiate(bulletPrefab);
        bullet.BulletPool = bulletPool;
        return bullet;
    }

    private void Update()
    {
        if(!canShoot)
        {
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = reloadDelay;

            foreach (var varbarrel in turretBarrels)
            {
                BulletController bullet = bulletPool.Get();
                bullet.transform.position = varbarrel.position;
                bullet.transform.localRotation = varbarrel.rotation;
                bullet.damage = damage;
                bullet.GetComponent<BulletController>().Initialize();
                foreach (var collider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
        }
    }
}