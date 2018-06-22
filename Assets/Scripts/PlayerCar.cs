using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour {

    public bool alive { get { return hitsLeft > 0; } }
    public int hitsLeft = 3;
    public KeyCode lineUp = KeyCode.W;
    public KeyCode lineDown = KeyCode.S;

    public float[] pasy = new float[] {
        5.25f,
        3.1f,
        1f,
        -1.1f,
        -3.25f
    };

    public int currentPas = 2;

    public void Crashed()
    {
        hitsLeft = 0;
        FindObjectOfType<MovingRoad>().slowdown = true;
    }

    public void Update()
    {
                if (alive)
        {
            if (Input.GetKeyDown(lineUp))
            {
                currentPas--;
            }
            if (Input.GetKeyDown(lineDown))
            {
                currentPas++;
            }
        }

        if (currentPas <= 0)
        {
            currentPas = 0;
        }

        if (currentPas >= 4)
        {
            currentPas = 4;
        }
    }
}
