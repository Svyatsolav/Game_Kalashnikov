using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    [SerializeField] LogData logItem;
    [SerializeField] Animator anim;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController.isTntPickedUp = true;
            anim.SetBool("isOn", true);
            LogScript.instance.AddItem(logItem);
            Destroy(gameObject);
        }
    }
}
