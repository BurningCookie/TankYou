using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    private int xpDrop = 1;
    private float speed = 4;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetXpDrop(int amount)
    {
        xpDrop = amount;
        transform.localScale = new Vector2(0.1f + 0.1f * xpDrop, 0.1f + 0.1f * xpDrop);
    }
    private void Update()
    {
        transform.Translate((player.transform.position - transform.position).normalized * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ScoreManager.AddXp(xpDrop);
            Destroy(gameObject);
        }
    }
}
