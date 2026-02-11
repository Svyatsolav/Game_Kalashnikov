using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_Lvl1 : MonoBehaviour
{
    public Animator anim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetTrigger("Trigger");
        }
    }
}
