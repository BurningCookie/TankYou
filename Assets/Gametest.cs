using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gametest : MonoBehaviour
{

    private void Start()
    {
        ScoreManager.levelup.AddListener(SayHi);
    }
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            ScoreManager.AddXp(7);
        }
    }
    void SayHi()
    {
        Debug.Log("hi");
    }
}
