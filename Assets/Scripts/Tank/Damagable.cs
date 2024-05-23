using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField]
    private int health;
    [SerializeField] private GameObject xpOrb;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            OnHealthChange?.Invoke((float)Health / maxHealth);
        }
    }

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit, OnHeal;

    private void Start()
    {
        Health = maxHealth;
    }

    internal void Hit(int damagePoints)
    {
        Health -= damagePoints;
        if (Health <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }

    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        OnHeal?.Invoke();
    }

    public void Death()
    {
        //Dropping XpOrbs
        if (xpOrb != null)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject orb = Instantiate(xpOrb, transform.position + new Vector3(Random.Range( -0.6f, 0.6f), Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f)), Quaternion.identity);
                orb.GetComponent<XpOrb>().SetXpDrop(Random.Range(1,3));
            }
        }
        Destroy(gameObject);
    }
}