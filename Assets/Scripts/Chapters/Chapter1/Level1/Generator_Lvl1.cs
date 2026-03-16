using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator_Lvl1 : MonoBehaviour
{
    public static bool isOn;
    private bool can;
    public SpriteRenderer _sprite;
    public Sprite genOn;
    public Animator interText;
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
                    _sprite.sprite = genOn;
                    ElectricButton_Lvl1.isGenOn = true;
                }
            }
            else interText.SetBool("isOn", false);
        }
        else interText.SetBool("isOn", false);
    }
}
