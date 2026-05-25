using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool isLever;
    bool can;
    public Animator anim;
    public Animator door;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(isLever == false) anim.SetBool("isOn", true); can = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(isLever == false) anim.SetBool("isOn", false); can = false;
        }
    }
    void Update()
    {
        if(isLever == false)
        {
            if(can == true)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    isLever = true;
                    door.SetBool("isOn", true);
                    anim.SetBool("isOn", false);
                }
            }
        }
    }
}
