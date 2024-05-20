using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gametest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            ScoreManager.AddXp(1);
        }
    }
}
