using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates_Lvl1 : MonoBehaviour
{
    public Animator anim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) anim.SetBool("isOn", true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("isOn", false);
            if(ElectricButton_Lvl1.isOn == true) Destroy(gameObject);
        }
    }
}
