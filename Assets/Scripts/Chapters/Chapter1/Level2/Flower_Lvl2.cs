using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flower_Lvl2 : MonoBehaviour
{
    [SerializeField] Animator wall;
    [SerializeField] Animator anim;
    [SerializeField] Slider hpBar;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Vector3 offset;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    void Update()
    {
        if(health <= 0)
        {
            anim.SetTrigger("death");
            hpBar.gameObject.SetActive(false);
            wall.SetTrigger("open");
        }
        else hpBar.gameObject.SetActive(health < maxHealth);
        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        hpBar.value = health;
        hpBar.maxValue = maxHealth;
    }
}
