using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricButton_Lvl1 : MonoBehaviour
{
    public static bool isOn;
    private bool can;
    public Animator interText;
    public Animator helpText;
    public GameObject textTrigger;
    public static bool isGenOn;
    public SpriteRenderer _sprite;
    public Sprite activeSprite;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) 
        {
            if(isGenOn == false) helpText.SetBool("isOn", true);
            can = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(isGenOn == false) helpText.SetBool("isOn", false);
            can = false;
        }
    }
    void Update()
    {
        if(isOn == false)
        {
            if(can == true)
            {
                if(isGenOn == true)
                {
                    interText.SetBool("isOn", true);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        isOn = true;
                        _sprite.sprite = activeSprite;
                        textTrigger.SetActive(true);
                    }
                }
            }
            else interText.SetBool("isOn", false);
        }
        else interText.SetBool("isOn", false);
    }
}
