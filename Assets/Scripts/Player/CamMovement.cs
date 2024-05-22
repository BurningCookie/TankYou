using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float moveSpeed = 10;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
