using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenLadder : MonoBehaviour
{
    public float speed;
    bool isFixed;
    public bool hasPlanks;
    public Animator helpText2;
    public Animator helpText;
    public Animator interText;
    public Transform player;
    public SpriteRenderer _sprite1;
    public SpriteRenderer _sprite2;
    public Sprite fixedSprite;
    bool can = false;
    bool inLadder = false;

    void OnTriggerStay2D(Collider2D other)
    {
        inLadder = true;

        if(hasPlanks == true)
        {
            interText.SetBool("isOn", true);
            can = true;
        }
        else helpText.SetBool("isOn", true);

        if(isFixed == true)
        {
            helpText.SetBool("isOn", false);
            interText.SetBool("isOn", false);
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            helpText2.SetBool("isOn", true);
            if(other.CompareTag("Player"))
            {
                if(Input.GetKey(KeyCode.W))
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                }
                else if(Input.GetKey(KeyCode.S))
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                }
                else
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        inLadder = false;
        helpText.SetBool("isOn", false);
        interText.SetBool("isOn", false);
        helpText2.SetBool("isOn", false);
        other.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
    void Update()
    {
        if(inLadder == true)
        {
            if(Input.GetKeyDown(KeyCode.E) && can == true)
            {
                isFixed = true;
                _sprite1.sprite = fixedSprite;
                _sprite2.sprite = fixedSprite;
                Transform plankTr = player.Find("Plank(Clone)");
                GameObject plank = plankTr.gameObject;
                Destroy(plank);
                can = false;
            }
        }
    }
}
