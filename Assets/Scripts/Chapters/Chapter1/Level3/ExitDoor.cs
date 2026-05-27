using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Animator animText;
    bool can;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            can = true;
            animText.SetBool("isOn", true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            can = false;
            animText.SetBool("isOn", false);
        }
    }
    void Update()
    {
        if(PlayerController.isTntPickedUp == true)
        {
            if(can == true)
            {
                if(Input.GetKeyDown(KeyCode.R))
                {
                    PlayerController.isTntPickedUp = false;
                    can = false;
                    anim.SetBool("isOn", true);
                }
            }
        }
    }
}
