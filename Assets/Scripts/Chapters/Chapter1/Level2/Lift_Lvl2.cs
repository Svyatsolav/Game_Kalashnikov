using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift_Lvl2 : MonoBehaviour
{
    public Animator interText;
    public Animator anim;
    public GameObject wall;
    bool can;
    bool isOn;
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && isOn == false)
        {
            interText.SetBool("isOn", true);
            can = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            interText.SetBool("isOn", false);
            can = false;
        }
    }
    void Update()
    {
        if(isOn == false)
        {
            if(can == true)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    isOn = true;
                    interText.SetBool("isOn", false);
                    anim.SetTrigger("on");
                    wall.SetActive(true);
                }
            }
        }
    }
}
