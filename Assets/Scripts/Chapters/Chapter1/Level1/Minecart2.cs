using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart2 : MonoBehaviour
{
    public void MinecartMoves()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
