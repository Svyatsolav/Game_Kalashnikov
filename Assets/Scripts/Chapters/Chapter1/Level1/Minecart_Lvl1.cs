using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart_Lvl1 : MonoBehaviour
{
    public static bool isOn;
    private bool can;
    public Rigidbody2D minecart;
    public Animator interText;
    public Animator anim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) can = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")) can = false;
    }
    void Update()
    {
        if(isOn == false)
        {
            if(can == true)
            {
                interText.SetBool("isOn", true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    isOn = true;
                    anim.SetTrigger("isOn");
                }
            }
            else interText.SetBool("isOn", false);
        }
        else interText.SetBool("isOn", false);
    }
}
