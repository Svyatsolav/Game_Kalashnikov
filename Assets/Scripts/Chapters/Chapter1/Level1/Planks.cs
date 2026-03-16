using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planks : MonoBehaviour
{
    public GameObject planksItem;
    public Transform Player;
    public GameObject bl;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            bl.GetComponent<BrokenLadder>().hasPlanks = true;
            Instantiate(planksItem, Player);
            Destroy(gameObject);
        }
    }
}
