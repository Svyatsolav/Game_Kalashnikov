using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes_Lvl2 : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(1);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
