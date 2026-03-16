using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger1 : MonoBehaviour
{
    public Animator anim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) anim.SetBool("isOn", true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")) anim.SetBool("isOn", false); Destroy(gameObject);
    }
}
